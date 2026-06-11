using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Dto.Message.Get
{
	public record GetMessageRequest(Guid ConversationId,
	Guid? Cursor,
	int Limit = 20);
	
	
}
