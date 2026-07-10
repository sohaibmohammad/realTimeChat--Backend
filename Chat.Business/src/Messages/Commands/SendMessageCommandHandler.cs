using Chat.Business.src.Abstraction;
using Chat.Business.src.Dto.Message.Create;
using Chat.Domain.src.Abstraction;
using Chat.Domain.src.Entity;
using Chat.Domain.src.Event;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Messages.Commands
{
	public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, MessageDto>
	{
		private readonly IMessageRepository _messageRepository;
		private readonly IChatNotifier _notifier;
		private readonly IMessageQueue _messageQueue;
		public SendMessageCommandHandler(IMessageQueue messageQueue, IMessageRepository messageRepository, IChatNotifier notifier)
		{
			_messageRepository = messageRepository;
			_notifier = notifier;
			_messageQueue = messageQueue;
		}
		public async Task<MessageDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
		{
			//var message = new Message
			//{
			//	id = Guid.NewGuid(),
			//	SenderId = request.SenderId,
			//	ConversationId = request.Request.ConversationId,
			//	MessageText = request.Request.Content,
			//	CreatedAt = DateTime.UtcNow,
			//	Status = MessageStatus.Sent
			//};

			//await _messageRepository.AddAsync(message);
			//await _messageRepository.SaveChangesAsync();

			//var messageDto = new MessageDto(
			//	message.id,
			//	message.SenderId,
			//	message.ConversationId,
			//	message.MessageText,
			//	message.CreatedAt,
			//	message.Status.ToString()
			//);

			//await _notifier.NotifyMessageReceived(message.ConversationId, messageDto);

			var messageEvent = new SendMessageEvent
			{
				Id = Guid.NewGuid(),
				SenderId = request.SenderId,
				ConversationId = request.Request.ConversationId,
				Content = request.Request.Content,
				CreatedAt = DateTime.UtcNow
			};

			await _messageQueue.EnqueueAsync(messageEvent);


			return new MessageDto(
				messageEvent.Id,
				messageEvent.SenderId,
				messageEvent.ConversationId,
				messageEvent.Content,
				messageEvent.CreatedAt,
				"Processing"
				);
		 }
	}
}
