using Chat.Domain.src.Abstraction;
using Chat.Domain.src.Entity;
using Chat.Infrastructure.src.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.src.Repository
{
	public class RefreshTokenRepository(AppDbContext Dbcontext) : IRefreshTokenRepository
	{
		private readonly AppDbContext _dbcontext = Dbcontext;

		public async Task<string> AddRefreshToken(UserRefreshToken userRef)
		{

			await _dbcontext.UserRefreshTokens.AddAsync(userRef);
			await _dbcontext.SaveChangesAsync();
			return userRef.RefreshToken;
		}


		public async Task<UserRefreshToken?> GetByTokenAsync(string refreshToken)
		{
			var token = await _dbcontext.UserRefreshTokens.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

			return token;
		}
		public async Task DeleteRefreshTokenAsync(string refreshToken)
		{
			var token = await _dbcontext.UserRefreshTokens.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
			if (token != null)
			{
				_dbcontext.UserRefreshTokens.Remove(token);
				await _dbcontext.SaveChangesAsync();
			}
		}

		public async Task<bool> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
		{
			var token = await _dbcontext.UserRefreshTokens.FirstOrDefaultAsync(x => x.UserId == userId && x.RefreshToken == refreshToken);
			return token != null && token.ExpiryDate > DateTime.UtcNow && !token.IsRevoked;
		}

		public async Task DeleteExpiredTokensAsync()
		{
			await _dbcontext.UserRefreshTokens.Where(x => x.ExpiryDate <= DateTime.UtcNow).ExecuteDeleteAsync();

		}
		public async Task DeleteTokensByUserIdAsync(Guid userId)
		{
			await _dbcontext.UserRefreshTokens.Where(x => x.UserId == userId).ExecuteDeleteAsync();

		}

		public async Task RevokeAsync(Guid userId)
		{
			var activeTokens = await _dbcontext.UserRefreshTokens
			   .Where(x => x.UserId == userId && !x.IsRevoked)
			   .ToListAsync();

			if (activeTokens.Any())
			{
				foreach (var token in activeTokens)
				{
					token.IsRevoked = true;
				}

				_dbcontext.UserRefreshTokens.UpdateRange(activeTokens);
				await _dbcontext.SaveChangesAsync();
			}
		}
	}

	}
