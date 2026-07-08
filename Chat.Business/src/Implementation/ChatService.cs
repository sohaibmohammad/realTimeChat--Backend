using Chat.Business.src.Abstraction;
using Chat.Business.src.Dto.Converstion;
using Chat.Business.src.Dto.Message.Create;
using Chat.Business.src.Dto.Message.Get;
using Chat.Business.src.Hubs;
using Chat.Domain.src.Abstraction;
using Chat.Domain.src.Entity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Implementation
{
	public class ChatService : IChatService
	{
		private readonly IMessageRepository _messageRepository;
		private readonly IConversationRepository _conversationRepository;
		private readonly IHubContext<ChatHub> _hubContext;
		private readonly IParticipantRepository _participantRepository;
		public ChatService(IParticipantRepository participantRepository, IConversationRepository conversationRepository, IMessageRepository messageRepository, IHubContext<ChatHub> hubContext)
		{
			_messageRepository = messageRepository;
			_hubContext = hubContext;
			_conversationRepository = conversationRepository;
			_participantRepository = participantRepository;
		}

		public async Task<bool> DeleteMessageAsync(Guid messageId, Guid userId)
		{


			var message = await _messageRepository.GetByIdAsync(messageId);
			if (message == null || message.SenderId != userId) {
				return false;
			}
			var delete= await _messageRepository.DeleteAsync(message);
			await _hubContext.Clients
	.Group(message.ConversationId.ToString())
	.SendAsync("MessageDeleted", messageId);
			await _messageRepository.SaveChangesAsync();

			return delete;
			
		}

		public Task<IEnumerable<MessageDto>> GetMessagesByConversationIdAsync(CoversationRequest request)
		{
			throw new ArgumentException();
		}

		public Task<IEnumerable<ConversationDto>> GetUserConversationsAsync(Guid userId)
		{
			throw new NotImplementedException();
		}

		public Task<bool> MarkAsDeliveredAsync(Guid messageId)
		{
			throw new NotImplementedException();
		}

		public Task<bool> MarkConversationAsReadAsync(Guid conversationId, Guid userId)
		{
			throw new NotImplementedException();
		}

		public async Task<MessageDto> SendMessageAsync(Guid sender,CreateMessageRequest request)
		{
			var message = new Message
			{
				id = Guid.NewGuid(),
				SenderId = sender,
				ConversationId = request.ConversationId,
				MessageText = request.Content,
				CreatedAt = DateTime.UtcNow,
				Status = MessageStatus.Sent
			};
			await _messageRepository.AddAsync(message);
			await _messageRepository.SaveChangesAsync();

			var messageDto = new MessageDto(
		message.id,
		message.SenderId,
		message.ConversationId,
		message.MessageText,
		message.CreatedAt,
		message.Status.ToString()
	);
			await _hubContext.Clients.Group(request.ConversationId.ToString()).SendAsync("ReceiveMessage", messageDto)

		;
			return messageDto;
		}

		public async Task<PagedMessagesResult> GetMessagesAsync(Guid currentUserId,GetMessageRequest request)
		{

			var messages=await _messageRepository.GetMessagesByConversationIdAsync(request.ConversationId,request.Cursor,request.Limit);

			var hasMore = messages.Count() > request.Limit ;

			if(hasMore)
				messages = messages.Take(request.Limit).ToList() ;
			var result = messages
		  .OrderBy(m => m.CreatedAt)
		  .Select(m => new GetMessage
		  {
			  Id = m.id,
			  SenderId = m.SenderId,
			  Text = m.MessageText,
			  CreatedAt = m.CreatedAt,

			  IsMine=m.SenderId == currentUserId,
		  })
		  .ToList();
			return new PagedMessagesResult
			{
				Messages = result,
				HasMore = hasMore
			};
		}
	}
}
