using Chat.Business.src.Dto.Converstion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Abstraction
{
	public interface IConversationService
	{
		Task<Guid> CreateConversationAsync(ConversationCreate request);

	}
}
