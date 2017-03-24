using src.CQRS.Contracts;

namespace src.Builders
{
    public interface IRabbitMqServiceBuilder
    {
         IRabbitMqServiceBuilder WithRabbitMq(string busName);
         IRabbitMqServiceBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand;

         IRabbitMqServiceBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent;

         IServiceBuilder Build();
    }
}