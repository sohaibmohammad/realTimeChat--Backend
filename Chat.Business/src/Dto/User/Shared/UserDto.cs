using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Dto.User.Shared
{
	public record UserDto(
		Guid Id,
		string UserName,
 		string Email,
		
		bool Active,
 		string? ProfileImageUrl
 	);
}
