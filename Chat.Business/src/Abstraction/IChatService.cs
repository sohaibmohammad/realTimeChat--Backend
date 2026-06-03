using Chat.Business.src.Dto.Message.Create;
using Chat.Business.src.Dto.User.Create;
using Chat.Business.src.Dto.User.Get;
using Chat.Domain.src.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Abstraction
{
	public interface IChatService
	{
		Task<MessageDto> SendMessageAsync(CreateMessageRequest request);
	}
	public interface IAuthService
	{
		Task SendVerificationCodeAsync(string email);

		Task<AuthResultDto> AuthenticateUserAsync(UserCredentials userCredentials);

		Task<AuthResultDto> RefreshTokenAsync(string refreshToken);
		Task<User> VerifyEmailAsync(string email, string code);

		Task<bool> ForgotPasswordAsync(string email);

		Task<bool> VerifyResetCodeAsync(string email, string code);

		Task<User> ResetPasswordAsync(string email, string code, string newPassword);
		Task<CreateUserResponse> CreateUserAsync(CreateUserRequests requests);
	}
}
