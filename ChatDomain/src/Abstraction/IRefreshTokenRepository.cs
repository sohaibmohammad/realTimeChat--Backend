using Chat.Domain.src.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.src.Abstraction
{
	public interface IRefreshTokenRepository
	{
		Task<string> AddRefreshToken(UserRefreshToken userRef);
		Task DeleteExpiredTokensAsync();
		Task DeleteRefreshTokenAsync(string refreshToken);
		Task DeleteTokensByUserIdAsync(Guid userId);
		Task<UserRefreshToken?> GetByTokenAsync(string refreshToken);
		Task RevokeAsync(Guid userId);
		Task<bool> ValidateRefreshTokenAsync(Guid userId, string refreshToken);
	}
	 
}
