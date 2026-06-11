using Chat.Domain.src.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Dto.Message.Get
{
	public class PagedMessagesResult
	{
		public  List<GetMessage> ?Messages { get; set; }
		public bool HasMore { get; set; }=false;

	}
}
