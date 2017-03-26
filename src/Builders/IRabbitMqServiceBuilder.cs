
using gifty.Shared.CQRS.Contracts;

namespace gifty.Shared.Builders
{
    public interface IRabbitMqServiceBuilder
    {
         IRabbitMqServiceBuilder WithRabbitMq(string busName);
         IRabbitMqServiceBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand;

         IRabbitMqServiceBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent;

         IServiceBuilder Build();
    }
}