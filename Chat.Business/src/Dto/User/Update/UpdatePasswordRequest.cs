using System.ComponentModel.DataAnnotations;

namespace Chat.Business.src.Dto.User.Update
{
	public record UpdatePasswordRequest(
[Required, MinLength(6)] string CurrentPassword,
[Required, MinLength(6)] string NewPassword
);
}
