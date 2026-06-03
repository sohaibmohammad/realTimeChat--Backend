using Chat.Business.src.Dto.User.Shared;
using Chat.Business.src.Dto.User.Update;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Abstraction
{
	public interface IUserService
	{

		Task<bool> UpdateUserAsync(int id, UpdateUserRequest updateUserRequest);
		//Task<PagedResponseDto<UserDto>> GetAllUserAsync(QueryOptions queryOptions);

		Task<UserDto?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);
		Task<bool> DeleteUserByIdAsync(int userId);

		Task<UserDto?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
		Task<string> UpdateUserProfilePictureAsync(IFormFile file, int userId);
	}
}
