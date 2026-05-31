using Chat.Domain.src.Abstraction;
using Chat.Domain.src.Entity;
using Chat.Infrastructure.src.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chat.Infrastructure.src.Repository
{
	public class MessageRepository : BaseRepository<Message>, IMessageRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<Message> _dbSet;
		private readonly ILogger<BaseRepository<Message>> _logger;
		public MessageRepository(AppDbContext context, ILogger<BaseRepository<Message>> logger) : base(context, logger)
		{
			_context = context;
			_dbSet = _context.Set<Message>();
			_logger = logger;
		}
		public async Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(Guid conversationId, int pageSize = 50)
		{
			return await _context.Messages
				.Where(m => m.ConversationId == conversationId)
				.OrderBy(m => m.CreatedAt)
				.Take(pageSize)
				.ToListAsync();
		}
	}
}
