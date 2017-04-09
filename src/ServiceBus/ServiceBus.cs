using gifty.Shared.CQRS.Contracts;
using gifty.Shared.CQRS.Handlers;
using gifty.Shared.IoC;
using RawRabbit;
using RawRabbit.vNext;

namespace gifty.Shared.ServiceBus
{
    internal sealed class ServiceBus : IServiceBus
    {
        private readonly IBusClient _busClient;
        private readonly ICustomDependencyResolver _customDependencyResolver;
        public ServiceBus(ICustomDependencyResolver customDependencyResolver)
        {
            _busClient = BusClientFactory.CreateDefault();
            _customDependencyResolver = customDependencyResolver;
        }
        public void SubscribeToCommand<TCommand>(string queueName) where TCommand : ICommand
        {
            _busClient.SubscribeAsync<TCommand>(async (command, context) => 
            {
                var commandHandler = _customDependencyResolver.Resolve<ICommandHandler<TCommand>>();
                await commandHandler.HandleAsync(command);
            }, cfg => cfg.WithQueue(q => q.WithName(queueName)));
        }

        public void SubscribeToEvent<TEvent>(string queueName) where TEvent : IEvent
        {
            _busClient.SubscribeAsync<TEvent>(async (@event, context) => 
            {
                var commandHandler = _customDependencyResolver.Resolve<IEventHandler<TEvent>>();
                await commandHandler.HandleAsync(@event);
            }, cfg => cfg.WithQueue(q => q.WithName(queueName)));
        }
    }
}