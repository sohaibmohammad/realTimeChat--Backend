using Chat.Domain.src.Abstraction;
using Chat.Domain.src.Entity;
using Chat.Infrastructure.src.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chat.Infrastructure.src.Repository
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		private readonly AppDbContext ApplicationDbContext;
		private readonly DbSet<User> _Users;
		private readonly ILogger<BaseRepository<User>> _logger;

		public UserRepository(AppDbContext applicatoinDbContext, ILogger<BaseRepository<User>> logger) : base(applicatoinDbContext, logger)
		{
			ApplicationDbContext = applicatoinDbContext;
			_logger = logger;
			_Users = ApplicationDbContext.Set<User>();

		}
 

		public async Task<bool> DeleteUser(int id)
		{
			var user = await _Users.FindAsync(id);
			if (user == null) { return false; }
			user.IsDeleted = true;
			return true;
		}

		public async Task<User?> GetUserByEmailAsync(string email)
		{
			return await _Users
				   .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
		}

		public async Task<User> UpdatePassword(string email, string PasswordHash)
		{
			var user = await _Users.FirstOrDefaultAsync(u => u.Email == email);
			if (user == null)
				return null;

			user.PasswordHash = PasswordHash;
			return user;
		}
	}
}
