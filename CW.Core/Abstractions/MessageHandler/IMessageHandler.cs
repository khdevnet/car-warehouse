using CW.Core.Abstractions.MessageHandler.Dto;
using System.Threading.Tasks;

namespace CW.Core.Abstractions.MessageHandler
{
    public interface IMessageHandler<TMessage> where TMessage: IMessage
    {
        Task Handle(TMessage message);
    }
}
