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
	//[Authorize]
	public class ChatHub : Hub
	{
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

		public Task SendTypingStatus(string conversationId,bool isTyping)
		{
			return Clients.OthersInGroup(conversationId).SendAsync("ReceiveTypingStatus", Context.ConnectionId, isTyping);
		}
	}
}
