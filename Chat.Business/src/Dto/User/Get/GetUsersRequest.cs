namespace Chat.Business.src.Dto.User.Get
{
	public record GetUsersRequest(bool IsActiveOnly = true, int PageNumber = 1, int Limit = 50);

}
