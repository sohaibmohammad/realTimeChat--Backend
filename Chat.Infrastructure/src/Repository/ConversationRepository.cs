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
				.Include(c => c.Participants)
					.ThenInclude(p => p.User)
				.Include(c => c.Messages) // عشان نجيب آخر رسالة بالشات مثلاً
				.Where(c => c.Participants.Any(p => p.id == userId))
				.OrderByDescending(c => c.CreatedAt)
				.ToListAsync();
		}
	}
}
