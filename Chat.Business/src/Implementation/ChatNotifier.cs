using Chat.Business.src.Abstraction;
using Chat.Business.src.Dto.Message.Create;
using Chat.Business.src.Hubs;
using Chat.Domain.src.Abstraction;
using Chat.Domain.src.Entity;
using Microsoft.AspNetCore.SignalR;
 namespace Chat.Business.src.Implementation;
	public class ChatNotifier : IChatNotifier
{
	private readonly IHubContext<ChatHub> _hubContext;
	public ChatNotifier(IHubContext<ChatHub> hubContext) => _hubContext = hubContext;

	public async Task NotifyMessageReceived(Guid conversationId, Message message)
		=> await _hubContext.Clients.Group(conversationId.ToString()).SendAsync("ReceiveMessage", message);

	public async Task NotifyTypingStatus(Guid conversationId, string userId, bool isTyping)
	=> await _hubContext.Clients.Group(conversationId.ToString())
		.SendAsync("ReceiveTypingStatus", userId, isTyping);

	public async Task NotifyMessageRead(Guid conversationId, Guid messageId, Guid readerId)
	=> await _hubContext.Clients.Group(conversationId.ToString())
		.SendAsync("MessageRead", new { messageId, readerId });

	public async Task NotifyMessageDeleted(Guid conversationId, Guid messageId)
	=> await _hubContext.Clients.Group(conversationId.ToString())
		.SendAsync("MessageDeleted", messageId);
}