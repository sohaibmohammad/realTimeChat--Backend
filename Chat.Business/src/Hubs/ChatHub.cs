using Chat.Business.src.Dto.Message.Create;
using Chat.Business.src.Messages.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chat.Business.src.Hubs
{
	[Authorize]
	public class ChatHub : Hub
	{

		private readonly IMediator _mediator; 
		public ChatHub(IMediator mediator)
		{
			_mediator = mediator;
			Console.WriteLine(">>> CHAT HUB INITIALIZED SUCCESSFULLY! <<<");
		}
		// ميثود يدخل فيها المستخدم لغرفة المحادثة الخاصة بالـ ConversationId
		public async Task JoinConversation(string conversationId)
		{
			// استخدام الميثود المباشرة الصحيحة
			await Groups.AddToGroupAsync(Context.ConnectionId, conversationId);
		}

		// ميثود للخروج من الغرفة عند إغلاق الشات
		public async Task LeaveConversation(string conversationId)
		{
			// استخدام الميثود المباشرة الصحيحة
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId);
		}

		public async Task SendTypingStatus(string conversationId, bool isTyping)
		{
			var userId = Guid.Parse(Context.UserIdentifier);
			var command = new SendTypingStatusCommand(Guid.Parse(conversationId), userId, isTyping);

			// الموزع (Mediator) سيأخذ الأمر ويوصله للـ Handler الذي سيتصل بالـ Notifier
			await _mediator.Send(command);
		}
		public async Task SendMessage(CreateMessageRequest request)
		{
			// نستخرج الـ UserId من الـ Token تلقائياً عبر الـ CustomUserIdProvider
			var userId = Guid.Parse(Context.UserIdentifier);

			// الـ Hub يمرر الطلب للـ Mediator فقط!
			await _mediator.Send(new SendMessageCommand(request, userId));
		}
		public async Task DeleteMessage(string msgId)
		{
			var userId = Guid.Parse(Context.UserIdentifier);

			await _mediator.Send(new DeleteMessageCommand(Guid.Parse(msgId), userId));
		}
	}
}
