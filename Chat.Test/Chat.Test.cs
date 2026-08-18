using Chat.Business.src.Dto.Message.Create;
using Chat.Business.src.Hubs;
using Chat.Business.src.Implementation;
using Chat.Domain.src.Abstraction;
using Chat.Domain.src.Entity;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Test
{
	 

		public class ChatServiceTests
		{
			private readonly Mock<IMessageRepository> _messageRepoMock;
			private readonly Mock<IConversationRepository> _conversationRepoMock;
			private readonly Mock<IParticipantRepository> _participantRepoMock;
			private readonly Mock<IHubContext<ChatHub>> _hubContextMock;
			private readonly Mock<IHubClients> _hubClientsMock;
			private readonly Mock<IClientProxy> _clientProxyMock;

			private readonly ChatService _chatService;

			public ChatServiceTests()
			{
				_messageRepoMock = new Mock<IMessageRepository>();
				_conversationRepoMock = new Mock<IConversationRepository>();
				_participantRepoMock = new Mock<IParticipantRepository>();
				_hubContextMock = new Mock<IHubContext<ChatHub>>();
				_hubClientsMock = new Mock<IHubClients>();
				_clientProxyMock = new Mock<IClientProxy>();

				// إعداد SignalR Mock عشان ما يضرب استثناء وقت استدعاء الـ SendAsync
				_hubContextMock.Setup(h => h.Clients).Returns(_hubClientsMock.Object);
				_hubClientsMock.Setup(c => c.Group(It.IsAny<string>())).Returns(_clientProxyMock.Object);

				// إنشاء نسخة من الـ ChatService وتمرير الموكس كاملة
				_chatService = new ChatService(
					_participantRepoMock.Object,
					_conversationRepoMock.Object,
					_messageRepoMock.Object,
					_hubContextMock.Object
				);
			}

			[Fact]
		public async Task SendMessageAsync_ShouldReturnMessageDto_WhenSuccessfullyAdded()
		{
			var senderId = Guid.NewGuid();

			var request = new CreateMessageRequest
			(Guid.NewGuid(), "Hello, World!");

			var result =await _chatService.SendMessageAsync(senderId, request);
			result.Should().NotBeNull();
			result.MessageText.Should().Be(request.Content);
			result.SenderId.Should().Be(senderId);
			_messageRepoMock.Verify(m => m.AddAsync(It.IsAny<Message>(),default), Times.Once);
			_messageRepoMock.Verify(repo => repo.SaveChangesAsync(default), Times.Once);

		}
		[Fact]
		public async Task DeleteMessageAsync_ShouldReturnFalse_whenMessageIsNull()
		{
			var messageId = Guid.NewGuid();
			var userId = Guid.NewGuid();
			_messageRepoMock.Setup(m => m.GetByIdAsync(messageId, default)).ReturnsAsync((Message)null!);
			var result = await _chatService.DeleteMessageAsync(messageId, userId);

			result.Should().BeFalse();
			_messageRepoMock.Verify(m => m.DeleteAsync(It.IsAny<Message>()), Times.Never);
		}
	}
}
