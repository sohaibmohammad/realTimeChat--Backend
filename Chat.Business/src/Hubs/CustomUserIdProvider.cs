using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Chat.Business.src.Hubs
{
	public class CustomUserIdProvider : IUserIdProvider
	{
		public string? GetUserId(HubConnectionContext connection)
		{
			// يستخرج الـ UserId من الـ Token الذي أرسله الـ Frontend
			return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}
	}
}
