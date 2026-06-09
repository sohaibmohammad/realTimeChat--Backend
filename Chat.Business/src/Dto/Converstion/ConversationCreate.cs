using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Dto.Converstion
{
	public record ConversationCreate(List<Guid> participantIds, string groupName);
	
}
