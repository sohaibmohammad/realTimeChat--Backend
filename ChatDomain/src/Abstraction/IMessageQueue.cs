using Chat.Domain.src.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.src.Abstraction
{
	public interface IMessageQueue
	{
		Task EnqueueAsync(SendMessageEvent message);

		Task<SendMessageEvent> DequeueAsync(
			CancellationToken cancellationToken
			);
	}
}
