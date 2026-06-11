using Chat.Business.src.Abstraction;
using Chat.Business.src.Dto.Converstion;
using Chat.Business.src.Dto.Message.Get;
using Chat.Domain.src.Abstraction;
using Chat.Domain.src.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Implementation
{
	public class ConversationService:IConversationService
	{
		private readonly IConversationRepository _conversationRepository;
		private readonly IMessageRepository _messageRepository;	
		private readonly IParticipantRepository _participantRepository;
		private readonly IUserRepository _userRepository;
		public ConversationService(IUserRepository userRepository,IMessageRepository messageRepository,IConversationRepository conversationRepository, IParticipantRepository participantRepository	)
		{
			_conversationRepository = conversationRepository;
			_participantRepository = participantRepository;
			_messageRepository = messageRepository;
			_userRepository = userRepository;
		}

		public async Task<Guid> CreateConversationAsync(ConversationCreate request)
		{
			if (request.participantIds.Count == 2)
			{
				var existingId = await _conversationRepository.GetConversationBetweenUsersAsync(request.participantIds[0], request.participantIds[1]);
				if (existingId != Guid.Empty)
				{
					return existingId; 
				}
			}
			var newConversation = new Conversation
			{
				id = Guid.NewGuid(),
				IsGroup = request.participantIds.Count > 2,
				GroupName = request.participantIds.Count > 2 ? request.groupName : "Direct Chat",
				CreatedAt = DateTime.UtcNow
			};
			await _conversationRepository.AddAsync(newConversation);
			foreach (var userId in request.participantIds)
			{
				var participant = new Participant
				{
					id = Guid.NewGuid(),
					ConversationId = newConversation.id,
					UserId = userId,
					JoinedAt = DateTime.UtcNow
				};

				await _participantRepository.AddAsync(participant);
			}
			await _conversationRepository.SaveChangesAsync();
			return newConversation.id;
		}

	    public async Task <List<ConversationGetAll>> GetAllChats(Guid userId)
		{
			var user= await _userRepository.GetByIdAsync(userId);
			if (user == null)
				throw new Exception("Please Login");

			var conversations = await _conversationRepository.GetConversationsForUserAsync(userId);
			var conversationsList = conversations
				.Select(x => new ConversationGetAll(
             	x.id,
		 		x.GroupName,
		 		x.Messages != null && x.Messages.Any()
		 		? x.Messages.OrderByDescending(c => c.CreatedAt).FirstOrDefault().MessageText // 👈 هون حط اسم حقل النص عندك (Content أو Text)
		 		: string.Empty 
		 	)).ToList();

			return conversationsList;

			
		}

	}
}
