using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Chat.Domain.src.Entity
{
	public class User : SharedEntity
	{

		[Required]
		[MaxLength(50)]
		public required string UserName { get; set; }
 		public string? ProfileImagePublicId { get; set; }
		public required string Email { get; set; }
  		public bool Active { get; set; }
		public bool IsDeleted { get; set; }
 		public string? ProfileImageUrl { get; set; }
		= null;
		public string PasswordHash { get; set; } = null!;
		public string? CoverImageUrl { get; set; }

		public ICollection<UserRefreshToken> RefreshTokens { get; set; } = new List<UserRefreshToken>();
		public string? EmailVerificationCode { get; set; }
		public DateTime? VerificationCodeExpiry { get; set; }

		public string? ExperienceYears { get; set; }
		public bool IsEmailVerified { get; set; } = false;
 

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public ICollection<Participant> Participants { get; set; } = new List<Participant>();
		public ICollection<Message> Messages { get; set; } = new List<Message>();
	}
}
