namespace Chat.Domain.src.Entity
{
	public class Conversation:SharedEntity
	{
	public string? GroupName { get; set; }
		public bool IsGroup { get; set; } = false;
		public DateTime CreatedAt { get; set; }=DateTime.UtcNow;

		public ICollection<Participant> Participants { get; set; } = new List<Participant>();
		public ICollection<Message> Messages { get; set; } = new List<Message>();
	}
}
