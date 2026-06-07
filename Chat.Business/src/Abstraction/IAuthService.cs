using Chat.Business.src.Dto.User.Create;
using Chat.Business.src.Dto.User.Get;
using Chat.Domain.src.Entity;

namespace Chat.Business.src.Abstraction
{
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
