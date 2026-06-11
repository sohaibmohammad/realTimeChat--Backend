using Chat.Business.src.Abstraction;
using Chat.Business.src.Dto.Converstion;
using Chat.Business.src.Dto.Message.Create;
using Chat.Business.src.Dto.Message.Delete;
using Chat.Business.src.Dto.Message.Get;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RealTimeChat.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
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

		[Authorize]
		[HttpDelete]
		
		public async Task<IActionResult> DeleteMessage(DeleteMesageRequest request) {

			var userIdClaim= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (string.IsNullOrEmpty(userIdClaim))
			{
				return Unauthorized("المستخدم غير معرف أو التوكن غير صحيح.");
			}
			if (!Guid.TryParse(userIdClaim, out Guid userId))
			{
				return BadRequest("صيغة معرف المستخدم غير صحيحة.");
			}
			var result = await _chatService.DeleteMessageAsync(request.MessageId,userId);
			return Ok(result);
		}
		[Authorize]
		[HttpGet("/messages")]
		public async Task<IActionResult> GetMessages(GetMessageRequest request)
		{
			var userIdClaim=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (string.IsNullOrEmpty(userIdClaim))
				return Unauthorized();
			var userId=Guid.Parse(userIdClaim);

			var result = await _chatService.GetMessagesAsync(userId, request);
			return Ok(result);
		}
	
	}
}
