using Chat.Business.src.Abstraction;
using Chat.Business.src.Dto.Converstion;
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
		private readonly IParticipantRepository _participantRepository;

		public ConversationService(IConversationRepository conversationRepository, IParticipantRepository participantRepository	)
		{
			_conversationRepository = conversationRepository;
			_participantRepository = participantRepository;
		}

		public async Task<Guid> CreateConversationAsync(ConversationCreate request)
		{
			if (request.participantIds.Count == 2)
			{
				var existingId = await _conversationRepository.GetConversationBetweenUsersAsync(request.participantIds[0], request.participantIds[1]);
				if (existingId != Guid.Empty)
				{
					return existingId; // الغرفة موجودة أصلاً، برجع الـ ID تبعها فوراً بدون تكرار
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
	}
}
