using Chat.Business.src.Abstraction;
using Chat.Domain.src.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Messages.Commands
{
	public class SendTypingStatusCommandHandler : IRequestHandler<SendTypingStatusCommand>
	{
		private readonly IChatNotifier _notifier;

		public SendTypingStatusCommandHandler(IChatNotifier notifier)
		{
			_notifier = notifier;
		}
		public async Task Handle(SendTypingStatusCommand request, CancellationToken cancellationToken)
		{
			await _notifier.NotifyTypingStatus(request.ConversationId, request.UserId.ToString(),request.IsTyping);
		}
	}
}
