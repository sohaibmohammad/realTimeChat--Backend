using Chat.Domain.src.Abstraction;
using Chat.Domain.src.Entity;
using Chat.Infrastructure.src.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chat.Infrastructure.src.Repository
{
	public class ConversationRepository : BaseRepository<Conversation>, IConversationRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<Conversation> _dbSet;
		private readonly ILogger<BaseRepository<Conversation>> _logger;

		public ConversationRepository(AppDbContext context, ILogger<BaseRepository< Conversation>> logger):base(context,logger)
		{
			_context = context;
			_dbSet = _context.Set<Conversation>();
			_logger = logger;
		}
		public async Task<IEnumerable<Conversation>> GetConversationsForUserAsync(Guid userId)
		{
			return await _context.Conversations
		.Include(c => c.Participants).ThenInclude(c=>c.User)
			
		// شلنا الـ Include تبع الـ Messages من هون نهائياً لحماية الرام
		.Where(c => c.Participants.Any(p => p.UserId == userId))
		.OrderByDescending(c => c.CreatedAt)
		.ToListAsync();
		}
		public async Task<Guid> GetConversationBetweenUsersAsync(Guid senderId, Guid receiverId)
		{
			var conversationId = await _context.Participants
 				.Where(p => p.UserId == senderId || p.UserId == receiverId)

 				.GroupBy(p => p.ConversationId) 
				.Where(g => g.Count() == 2)

 				.Select(g => g.Key)

 				.FirstOrDefaultAsync();

			return conversationId;
		}
	}
}
