using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.src.Entity
{
	public class User : SharedEntity
	{

		[Required]
		[MaxLength(50)]
		public string Username { get; set; }

		[Required]
		[MaxLength(100)]
		public string Email { get; set; }
        [Required]
		[MinLength(6)]
         public string PasswordHash { get; set; }

		public string? AvatarUrl { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public ICollection<Participant> Participants { get; set; } = new List<Participant>();
		public ICollection<Message> Messages { get; set; } = new List<Message>();
	}
}
