using Chat.Business.src.Abstraction;
using Chat.Business.src.Dto.Message.Create;
using Chat.Business.src.Hubs;
using Chat.Business.src.Implementation;
using Chat.Domain.src.Abstraction;
using Chat.Domain.src.Entity;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
namespace Chat.Test
{
	public class ChatServiceTests
	{
		//	private readonly Mock<IMessageRepository> _messageRepositoryMock;
		//	private readonly ChatService _chatService;
		//			private readonly Mock<IHubContext<ChatHub>> _hubContextMock;


		//	public ChatServiceTests()
		//	{
		//		// 1. Arrange: ⁄„· Mock ··‹ Repository «·„Œ —ﬁ
		//		_messageRepositoryMock = new Mock<IMessageRepository>();

		//		// 2. Õﬁ‰ «·‹ Mock œ«Œ· «·‹ Service «·ÕﬁÌﬁÌ… «··Ì »œ‰« ‰›Õ’Â«
		//		_chatService = new ChatService(_messageRepositoryMock.Object, _hubContextMock.Object);
		//	}
		//	[Fact]
		//	public async Task SendMessageAsync_ShouldSaveMessageAndReturnCorrectDto()
		//	{
		//		var senderId = Guid.NewGuid();
		//		var receiverId = Guid.NewGuid();
		//		var content = "Hello, this is a real-time message!";

		//		// 1. ≈⁄œ«œ „ÌÀÊœ «·≈÷«›… „⁄  „—Ì— «·‹ CancellationToken «·«› —«÷Ì
		//		// 1. Õ· „‘ﬂ·… AddAsync: ‰Œ·ÌÂ Ì—Ã⁄ ‰›” «·‹ Message «··Ì œŒ·  ⁄·ÌÂ œÌ‰«„ÌﬂÌ«
		//		_messageRepositoryMock
		//			.Setup(repo => repo.AddAsync(It.IsAny<Message>(), It.IsAny<CancellationToken>()))
		//			.ReturnsAsync((Message msg, CancellationToken token) => msg);

		//		// 2. ≈⁄œ«œ „ÌÀÊœ «·Õ›Ÿ ( √ﬂœ „‰ ﬂ «» Â« Âﬂ–« ≈–« ﬂ«‰   —Ã⁄ Task ›«—€)
		//		_messageRepositoryMock
		//			.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
		//			.Returns(Task.CompletedTask);

		//		var result = await _chatService.SendMessageAsync(new CreateMessageRequest(senderId, receiverId, content));

		//		Assert.NotNull(result);
		//		Assert.NotEqual(Guid.Empty, result.Id); // «· √ﬂœ „‰  Ê·Ìœ Guid ÃœÌœ ··—”«·…
		//		Assert.Equal(senderId, result.SenderId);
		//		Assert.Equal(receiverId, result.ConversationId);
		//		Assert.Equal(content, result.MessageText);
		//		Assert.Equal("Sent", result.Status); // «· √ﬂœ „‰ «·Õ«·… «·√Ê·Ì… ··—”«·…

		//		// «· Õﬁﬁ «·Â‰œ”Ì: Â· «·‹ Service «” œ⁄  „ÌÀÊœ «·Õ›Ÿ »«·‹ Repository ›⁄·« Ê„—… Ê«Õœ…ø
		//		_messageRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Message>(), It.IsAny<CancellationToken>()), Times.Once);
		//		_messageRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
	//}
	}
}