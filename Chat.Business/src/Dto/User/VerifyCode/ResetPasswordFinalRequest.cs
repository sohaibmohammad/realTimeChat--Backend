namespace Chat.Business.src.Dto.User.VerifyCode
{
	public record ResetPasswordFinalRequest(
	string Email,
	string Code,
	string NewPassword
);

}
