using Chat.Domain.src.Entity;

namespace Chat.Domain.src.Abstraction
{
	public interface IConversationRepository : IBaseRepository<Conversation>
	{
		// دالة بتجيب كل المحادثات الخاصة بمستخدم معين مع تفاصيل الأعضاء والرسائل
		Task<IEnumerable<Conversation>> GetConversationsForUserAsync(Guid userId);
	}
}
