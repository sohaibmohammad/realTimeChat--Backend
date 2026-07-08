 
using Chat.Domain.src.Abstraction;
using Chat.Domain.src.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Chat.Infrastructure.src.Messaging
{
	public class MessageQueue : IMessageQueue
	{
		private readonly Channel<SendMessageEvent> _channel;
		public MessageQueue()
		{
			_channel = Channel.CreateBounded<SendMessageEvent>(100);
		}

		public async Task<SendMessageEvent> DequeueAsync(CancellationToken cancellationToken)
		{
			return await _channel.Reader.ReadAsync(cancellationToken);
		}

		public async Task EnqueueAsync(SendMessageEvent message)
		{

			await _channel.Writer.WriteAsync(message);
		}
	}
}
