using Chat.Business.src.Abstraction;
using Chat.Domain.src.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Messages.Commands
{
	public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, bool>
	{
		private readonly IMessageRepository _messageRepository;
		private readonly IChatNotifier _notifier;

		public DeleteMessageCommandHandler(IMessageRepository messageRepository, IChatNotifier notifier)
		{
			_messageRepository = messageRepository;
			_notifier = notifier;
		}
		public async Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
		{
			var userMessage=await _messageRepository.UserHasThisMessage(request.UserId, request.MessageId);
			if (!userMessage) return false;

			var message = await _messageRepository.GetByIdAsync(request.MessageId);
			if (message == null) return false;

			await _messageRepository.DeleteAsync(message);
			await _messageRepository.SaveChangesAsync();
			await _notifier.NotifyMessageDeleted(message.ConversationId, request.MessageId);
			return true;
		}
	}
}
