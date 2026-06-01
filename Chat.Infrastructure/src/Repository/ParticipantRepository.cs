using Chat.Domain.src.Abstraction;
using Chat.Domain.src.Entity;
using Chat.Infrastructure.src.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chat.Infrastructure.src.Repository
{
	public class ParticipantRepository : BaseRepository<Participant>, IParticipantRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<Participant> _dbSet;
		private readonly ILogger<BaseRepository<Participant>> _logger;
		public ParticipantRepository(AppDbContext context, ILogger<BaseRepository<Participant>> logger) : base(context, logger)
		{
			_context = context;
			_dbSet = _context.Set<Participant>();
			_logger = logger;
		}
	}
	}
