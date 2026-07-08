
using Chat.Domain.src.Entity;

namespace Chat.Domain.src.Abstraction
{
	public interface IChatNotifier
	{
		Task NotifyMessageDeleted(Guid conversationId, Guid messageId);
		Task NotifyMessageRead(Guid conversationId, Guid messageId, Guid readerId);
		Task NotifyMessageReceived(Guid conversationId, Message message);
		Task NotifyTypingStatus(Guid conversationId, string userId, bool isTyping);
	}
}
