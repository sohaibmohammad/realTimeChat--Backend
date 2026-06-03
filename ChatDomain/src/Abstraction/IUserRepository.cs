using Chat.Domain.src.Entity;

namespace Chat.Domain.src.Abstraction
{
	public interface IUserRepository : IBaseRepository<User>
	{
		Task<User> GetUserByEmailAsync(string email);
 		Task<User> UpdatePassword(string email, string PasswordHash);
		Task<bool> DeleteUser(int id);

	}
}
