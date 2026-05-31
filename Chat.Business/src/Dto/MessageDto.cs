using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Dto
{
	public record MessageDto(Guid Id, Guid SenderId, Guid ConversationId, string MessageText, DateTime CreatedAt,string Status);
	
}
