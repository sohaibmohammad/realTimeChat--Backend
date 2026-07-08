using Chat.Domain.src.Abstraction;
using Chat.Domain.src.Entity;
using Chat.Infrastructure.src.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

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
		public async Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(Guid conversationId, Guid? cursor, int limit = 10)
		{
			var query = _context.Messages.Where(m => m.ConversationId == conversationId);

			if (cursor.HasValue)
			{
				var cursorMessage = await _context.Messages.FirstOrDefaultAsync(m => m.id == cursor.Value);

				if (cursorMessage != null)
				{
					query = query.Where(m => m.CreatedAt < cursorMessage.CreatedAt);
				}

			}
			return await query
		   .OrderByDescending(m => m.CreatedAt)
		   .Take(limit + 1)
		   .ToListAsync();
		}

		public async Task<bool> UserHasThisMessage(Guid userId, Guid messageId)
		{
			var message = await _context.Messages.FirstOrDefaultAsync(m => m.id == messageId && m.SenderId == userId);
			if (message == null)
				return false;
			var participant = await _context.Participants.FirstOrDefaultAsync(p => p.ConversationId == message.ConversationId && p.UserId == userId);
			return participant != null;
		}
	}
	}
