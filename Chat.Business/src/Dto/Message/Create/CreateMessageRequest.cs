using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Dto.Message.Create
{
	 public record CreateMessageRequest( Guid ConversationId, string Content);
	
	
}
