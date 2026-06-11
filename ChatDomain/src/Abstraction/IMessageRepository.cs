using Chat.Domain.src.Entity;

namespace Chat.Domain.src.Abstraction
{
	public interface IMessageRepository : IBaseRepository<Message>
	{
		// دالة بتجيب رسائل محادثة معينة مرتبة من الأقدم للأحدث (عشان تظهر صح بالشات)
		Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(Guid conversationId, Guid? cursor, int limit = 10);
	}
}
