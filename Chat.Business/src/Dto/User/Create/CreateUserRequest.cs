using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Dto.User.Create
{
	public record CreateUserRequests(
		[Required] string UserName,
		[Required] string Email,
 		[Required, MinLength(4)] string Password
		  		)
	{
	}
}
