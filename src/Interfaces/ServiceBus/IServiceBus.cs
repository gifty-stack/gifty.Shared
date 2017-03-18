using System.Threading.Tasks;
using src.Interfaces.Contracts;

namespace src.Interfaces.ServiceBus
{
    public interface IServiceBus
    {
         Task PublishCommandAsync<TCommand>(TCommand command) where TCommand : ICommand;
         Task PublishEventAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}