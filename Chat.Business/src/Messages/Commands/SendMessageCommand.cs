using Chat.Business.src.Dto.Message.Create;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Messages.Commands
{
	public record SendMessageCommand(CreateMessageRequest Request, Guid SenderId) : IRequest<MessageDto>;
}
