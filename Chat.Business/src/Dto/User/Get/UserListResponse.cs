namespace Chat.Business.src.Dto.User.Get
{
	public record UserListResponse(
		Guid Id,
		string UserName,
 		string Email,
 		bool Active
	);

}
