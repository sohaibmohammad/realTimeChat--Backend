
using Chat.Domain.src.Abstraction;
using Chat.Domain.src.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections;
namespace Chat.Infrastructure.src.Workers
{
	public class MessageWorker : BackgroundService
	{
		private readonly IMessageQueue _queue;
		private readonly IServiceScopeFactory _scopeFactory;
		public MessageWorker(
	  IMessageQueue queue,
	  IServiceScopeFactory scopeFactory)
		{
			_queue = queue;
			_scopeFactory = scopeFactory;
		}

		protected async override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested) {
				var message =
			   await _queue.DequeueAsync(stoppingToken);


				using var scope =
					_scopeFactory.CreateScope();


				var repo =
					scope.ServiceProvider
					.GetRequiredService<IMessageRepository>();


				var notifier =
					scope.ServiceProvider
					.GetRequiredService<IChatNotifier>();


				var entity = new Message
				{
					id = message.Id,
					SenderId = message.SenderId,
					ConversationId = message.ConversationId,
					MessageText = message.Content,
					CreatedAt = message.CreatedAt,
					Status = MessageStatus.Sent
				};


				await repo.AddAsync(entity);
				await repo.SaveChangesAsync();

				 

				await notifier.NotifyMessageReceived(
					message.ConversationId,
					entity);
			}
		}
	}
}
