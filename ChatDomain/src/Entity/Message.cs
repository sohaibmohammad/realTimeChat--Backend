using System.ComponentModel.DataAnnotations;

namespace Chat.Domain.src.Entity
{
	public class Message:SharedEntity
	{
	 

		public Guid ConversationId { get; set; }
		public Conversation Conversation { get; set; } = null!;

		public Guid SenderId { get; set; }
		public User Sender { get; set; } = null!;

		[Required]
		public string MessageText { get; set; } = string.Empty;

		public string? MediaUrl { get; set; } // لو بعت صورة أو ملف في الشات

		[Required]
		public MessageStatus Status { get; set; } = MessageStatus.Sent;

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
