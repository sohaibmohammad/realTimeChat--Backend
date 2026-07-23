using Chat.Business.src.Abstraction;
using Chat.Business.src.Dto.Converstion;
using Chat.Business.src.Dto.Message.Get;
using Chat.Domain.src.Abstraction;
using Chat.Domain.src.Entity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
namespace Chat.Business.src.Implementation
{
	public class ConversationService:IConversationService
	{
		private readonly IConversationRepository _conversationRepository;
		private readonly IMessageRepository _messageRepository;	
		private readonly IParticipantRepository _participantRepository;
		private readonly IUserRepository _userRepository;
		private readonly IDistributedCache _cache;
		public ConversationService(IDistributedCache cache,IUserRepository userRepository,IMessageRepository messageRepository,IConversationRepository conversationRepository, IParticipantRepository participantRepository	)
		{
			_cache = cache; 
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

		public async Task<List<ConversationGetAll>> GetAllChats(Guid userId)
		{
			string cacheKey = $"conversations_{userId}";

			var cachedData = await _cache.GetStringAsync(cacheKey);

			if(!string.IsNullOrEmpty(cachedData))
			{
				var cachedConversations = System.Text.Json.JsonSerializer.Deserialize<List<ConversationGetAll>>(cachedData);
				return cachedConversations ?? new List<ConversationGetAll>();
			}

			var user = await _userRepository.GetByIdAsync(userId);

			if (user == null)
				throw new Exception("Please Login");

			var conversations = await _conversationRepository.GetConversationsForUserAsync(userId);

			var conversationsList = conversations.Select(x =>
			{
				var lastMessage = x.Messages?
					.OrderByDescending(m => m.CreatedAt)
					.FirstOrDefault();

				return new ConversationGetAll(
					x.id,

					x.IsGroup && x.GroupName != "Direct Chat"
						? x.GroupName
						: x.Participants
							.Where(p => p.UserId != userId)
							.Select(p => p.User?.UserName)
							.FirstOrDefault() ?? "Unknown User",

					lastMessage?.MessageText ?? string.Empty
				);
			}).ToList();
			var cacheOptions = new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
			};

			await _cache.SetStringAsync(cacheKey, System.Text.Json.JsonSerializer.Serialize(conversationsList), cacheOptions);

			return conversationsList;
		}

	}
}
