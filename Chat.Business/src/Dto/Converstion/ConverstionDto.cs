using Chat.Business.src.Dto.Message.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Dto.Converstion
{
	public class ConversationDto
	{
		// 1. المعرف الفريد للمحادثة عشان لما تكبس عليها تفتح الشات الصح
		public Guid Id { get; set; }

		// 2. معلومات الطرف الآخر اللي بتشات معه (الـ Receiver)
		public Guid OtherUserId { get; set; }
		public string OtherUserName { get; set; }
		public string OtherUserAvatar { get; set; } // رابط صورة البروفايل تبعه

		// 3. كبسولة "آخر رسالة" (Last Message) - أهم جزء بالـ Sidebar
		// بنستخدم الـ MessageDto اللي عندك عشان نجيب نص الرسالة ووقتها
		public MessageDto LastMessage { get; set; }

		// 4. عدّاد الرسائل غير المقروءة (Unread Messages Count)
		// عشان يظهر دائرة حمراء فيها رقم (مثلاً: 3 رسائل جديدة من أحمد)
		public int UnreadCount { get; set; }

		// 5. حالة الطرف الآخر (Real-time Online/Offline Status)
		// بتلزمك بالـ React عشان تحط نقطة خضراء أو رمادية جمب صورته
		public bool IsOnline { get; set; }
	}
}
