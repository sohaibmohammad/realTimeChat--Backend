using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.Domain.src.Entity
{
	[Table("UserRefreshToken")]
	public class UserRefreshToken
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public string RefreshToken { get; set; }
		public bool IsRevoked { get; set; }
		public DateTime ExpiryDate { get; set; }
		public User User { get; set; }
	}
}
