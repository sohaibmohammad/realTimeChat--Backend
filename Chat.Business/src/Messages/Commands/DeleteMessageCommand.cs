using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.src.Messages.Commands
{
	public record DeleteMessageCommand(Guid MessageId,Guid UserId) : IRequest<bool>;
	
}
