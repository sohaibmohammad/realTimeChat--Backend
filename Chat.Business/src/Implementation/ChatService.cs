using Chat.Business.src.Abstraction;
using Chat.Business.src.Dto.Message.Create;
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
		public ChatService(IParticipantRepository participantRepository ,IConversationRepository conversationRepository, IMessageRepository messageRepository, IHubContext<ChatHub> hubContext)
		{
			_messageRepository = messageRepository;
			_hubContext = hubContext;
			_conversationRepository = conversationRepository;
			_participantRepository = participantRepository;
		}
		public async Task<MessageDto> SendMessageAsync(CreateMessageRequest request)
		{
			Guid conversationId = await _conversationRepository.GetConversationBetweenUsersAsync(request.SenderId, request.receiverId);

			if(conversationId == Guid.Empty)
			{
				var newConversation = new Conversation
				{
					id = Guid.NewGuid(),
					IsGroup = false,
					GroupName = "Direct Chat",
					CreatedAt = DateTime.UtcNow
				};
				await _conversationRepository.AddAsync(newConversation);
				var senderParticipant = new Participant
				{
					id = Guid.NewGuid(),
					ConversationId = newConversation.id,
					UserId = request.SenderId,
					 JoinedAt = DateTime.UtcNow
				};
				var receiverParticipant = new Participant
				{
					id = Guid.NewGuid(),
					ConversationId = newConversation.id,
					UserId = request.receiverId,
					 JoinedAt = DateTime.UtcNow
				};
				await _participantRepository.AddAsync(senderParticipant);
				await _participantRepository.AddAsync(receiverParticipant);

				conversationId = newConversation.id;
			}



			var message = new Message
			{
				
				id = Guid.NewGuid(),
				SenderId = request.SenderId,
				ConversationId = conversationId,
				MessageText = request.Content,
				CreatedAt = DateTime.UtcNow,
				Status = MessageStatus.Sent
			};

		 await	_messageRepository.AddAsync(message);
			await _messageRepository.SaveChangesAsync();
			await _hubContext.Clients.Group(request.receiverId.ToString()).SendAsync("ReceiveMessage", new MessageDto(
				message.id,
				message.SenderId,
				message.ConversationId,
				message.MessageText,
				message.CreatedAt,
				message.Status.ToString()
			));
			return new MessageDto(
				message.id,
				message.SenderId,
				message.ConversationId,
				message.MessageText,
				message.CreatedAt,
				message.Status.ToString()
			);

			
		}
	}
}
