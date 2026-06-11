using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Dto.Message.Get
{
	public class GetMessage
	{
		public Guid Id { get; set; }
		public Guid SenderId { get; set; }
		public string Text { get; set; }
		public DateTime CreatedAt { get; set; }

		public bool IsMine { get; set; }
	}
}
