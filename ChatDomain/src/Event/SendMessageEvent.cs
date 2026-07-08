using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.src.Event
{
	public class SendMessageEvent
	{
		public Guid Id { get; set; }
		public Guid SenderId { get; set; }
		public Guid ConversationId { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
