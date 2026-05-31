using Chat.Domain.src.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.src.Database
{
	public class AppDbContext:DbContext
	{
		public	 DbSet<User> Users { get; set; }
		public	 DbSet<Message> Messages { get; set; }
		public	 DbSet<Conversation> Conversations { get; set; }
		public	 DbSet<Participant> Participants { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
