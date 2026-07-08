using MediatR;

namespace Chat.Business.src.Messages.Commands
{
	public record SendTypingStatusCommand(Guid ConversationId, Guid UserId, bool IsTyping) : IRequest;
}
