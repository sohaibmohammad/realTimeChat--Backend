using Chat.Business.src.Abstraction;
using Chat.Business.src.Dto.Converstion;
using Microsoft.AspNetCore.Mvc;

namespace RealTimeChat.Controllers
{
	[ApiController]
	[Route("api/[controller]")]

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
	}
}
