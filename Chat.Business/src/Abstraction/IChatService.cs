using Chat.Business.src.Dto.Converstion;
using Chat.Business.src.Dto.Message.Create;
using Chat.Business.src.Dto.Message.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Abstraction
{
	public interface IChatService
	{
		Task<MessageDto> SendMessageAsync(CreateMessageRequest request);

		Task<IEnumerable<MessageDto>> GetMessagesByConversationIdAsync(CoversationRequest request);

		// 3. جلب قائمة كل المحادثات الأخيرة للمستخدم الحالي (عشان تظهر بالـ Sidebar في الـ React)
		Task<IEnumerable<ConversationDto>> GetUserConversationsAsync(Guid userId);

		// 4. تحديث حالة الرسالة إلى "وصلت" (Delivered)
		Task<bool> MarkAsDeliveredAsync(Guid messageId);

		// 5. تحديث حالة الرسائل إلى "قُرئت" (Read) لما المستخدم يفتح الشات
		Task<bool> MarkConversationAsReadAsync(Guid conversationId, Guid userId);

		// 6. مسح رسالة (حذف من الطرفين أو طرف واحد حسب البزنس)
		Task<bool> DeleteMessageAsync(Guid messageId, Guid userId);
		Task<PagedMessagesResult> GetMessagesAsync(Guid currentUserId, GetMessageRequest request);
	}
}
