namespace Chat.Business.src.Dto.User.VerifyCode
{
	public record UpdatePasswordRequest(string Email, string OldPassword, string NewPassword);

}
