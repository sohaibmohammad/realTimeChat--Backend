using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Dto.User.VerifyCode
{
	public class RefreshTokenRequest
	{
		public string RefreshToken { get; set; } = string.Empty;
	}

}
