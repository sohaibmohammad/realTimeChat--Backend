namespace Chat.Domain.src.Entity
{
	public enum MessageStatus
	{
		Sent = 1,      // طلعت من المرسل
		Delivered = 2, // وصلت لجهاز المستقبل
		Read = 3       // المستقبل فتحها وقرأها
	}
}
