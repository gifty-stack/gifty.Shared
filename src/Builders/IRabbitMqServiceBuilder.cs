
using gifty.Shared.CQRS.Contracts;

namespace gifty.Shared.Builders
{
    public interface IRabbitMqServiceBuilder
    {
         IRabbitMqServiceBuilder WithRabbitMq(string queueName, string username, string password, int port);
         IRabbitMqServiceBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand;

         IRabbitMqServiceBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent;

         IServiceBuilder Build();
    }
}