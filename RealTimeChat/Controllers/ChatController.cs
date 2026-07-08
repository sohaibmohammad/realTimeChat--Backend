using Chat.Business.src.Abstraction;
using Chat.Business.src.Dto.Converstion;
using Chat.Business.src.Dto.Message.Create;
using Chat.Business.src.Dto.Message.Delete;
using Chat.Business.src.Dto.Message.Get;
 using Chat.Business.src.Messages.Commands;
using MediatR;
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
		private readonly IMediator _mediator;
		public ChatController(IChatService chatService, IMediator mediator)
		{
			_chatService = chatService;
			_mediator = mediator;
		}

		[HttpPost("send")]
		[Authorize]
		public async Task<IActionResult> SendMessage([FromBody] CreateMessageRequest request)
		{
			var user=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var sender = Guid.Parse(user);
			var command = new SendMessageCommand(request, sender);
			var result = await _mediator.Send(command);
			

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
			var command = new DeleteMessageCommand(request.MessageId, userId);
			var result = await _mediator.Send(command);
			return Ok(result);
		}
		[Authorize]
		[HttpGet("messages")]
		public async Task<IActionResult> GetMessages([FromQuery]GetMessageRequest request)
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
