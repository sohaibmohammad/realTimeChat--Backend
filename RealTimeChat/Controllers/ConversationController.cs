using Chat.Business.src.Abstraction;
using Chat.Business.src.Dto.Converstion;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace RealTimeChat.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]

	public class ConversationController:ControllerBase
	{
		public readonly IConversationService _conversationService;
		public ConversationController(IConversationService conversationService)
		{
			_conversationService=conversationService;
		}

		[HttpPost("Conver")]
		
		public async Task<IActionResult> CreateConversation(ConversationCreate request)
		{
			var create = await _conversationService.CreateConversationAsync(request);

			return Ok(create);
		}
		[Authorize]
		[HttpGet("chats")]
		public async Task <IActionResult> GetAllConversation()
		{
			var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var userId=Guid.Parse(user);

			var chats = await _conversationService.GetAllChats(userId);
			return Ok(chats);
		}
	}
}
