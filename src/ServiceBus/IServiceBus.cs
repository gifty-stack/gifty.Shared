using gifty.Shared.CQRS.Contracts;

namespace gifty.Shared.ServiceBus
{
    public interface IServiceBus
    {         
         void SubscribeToCommand<TCommand>(string queueName) where TCommand : ICommand;
         void SubscribeToEvent<TEvent>(string queueName) where TEvent : IEvent;
    }
}