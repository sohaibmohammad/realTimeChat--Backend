using Chat.Domain.src.Entity;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.src.Database
{
	public class AppDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Conversation> Conversations { get; set; }
		public DbSet<Participant> Participants { get; set; }

		public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// 1. إعدادات علاقات كلاس الـ Message
			modelBuilder.Entity<Message>(entity =>
			{
				// ربط علاقة الرسالة بالمحادثة باستخدام الحقل المكتوب بـ PascalCase
				entity.HasOne(m => m.Conversation)
					  .WithMany(c => c.Messages) // تأكد إن عندك List<Message> Messages بكلاس Conversation
					  .HasForeignKey(m => m.ConversationId)
					  .OnDelete(DeleteBehavior.Restrict); // لمنع الـ Cascade المتعارض

				// ربط علاقة الرسالة بالمرسل
				entity.HasOne(m => m.Sender)
					  .WithMany() // إذا ما عندك لستة رسائل بكلاس الـ User
					  .HasForeignKey(m => m.SenderId)
					  .OnDelete(DeleteBehavior.Restrict);
			});

			// 2. إعدادات علاقات كلاس الـ Participant (إذا كان مسبب مشكلة سابقاً)
			modelBuilder.Entity<Participant>(entity =>
			{
				entity.HasOne(p => p.Conversation)
					  .WithMany(c => c.Participants) // تأكد إن عندك List<Participant> بكلاس Conversation
					  .HasForeignKey(p => p.ConversationId)
					  .OnDelete(DeleteBehavior.Cascade);
			});

		}
	}
}