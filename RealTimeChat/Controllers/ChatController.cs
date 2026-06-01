using Chat.Business.src.Abstraction;
using Chat.Business.src.Dto.Message.Create;
using Microsoft.AspNetCore.Mvc;

namespace RealTimeChat.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ChatController:ControllerBase
	{
		private readonly IChatService _chatService;
		public ChatController(IChatService chatService)
		{
			_chatService = chatService;
		}

		[HttpPost("send")]
		public async Task<IActionResult> SendMessage([FromBody] CreateMessageRequest request)
		{
			var result = await _chatService.SendMessageAsync(request);
			return Ok(result);
		}
	}
}
