using System;
using System.IO;
using Autofac;
using gifty.Shared.CQRS.Handlers;
using gifty.Shared.IoC;
using Microsoft.AspNetCore.Hosting;
using RawRabbit;
using RawRabbit.vNext;
using RawRabbit.Configuration;
using RawRabbit.DependencyInjection.Autofac;

namespace gifty.Shared.Builders
{
    public class ServiceBuilder : IServiceBuilder, IAutofacServiceBuilder, IRabbitMqServiceBuilder
    {
        private IWebHostBuilder _webHostBuilder;

        private IWebHost _webHost;
        private ContainerBuilder _containerBuilder;
        private ICustomDependencyResolver _customDependencyResolver;
        private IBusClient _busClient;
        private string _queueName;

        public ServiceBuilder(IWebHostBuilder webHostBuilder)
        {
            _webHostBuilder = webHostBuilder;
        }

        public static IServiceBuilder CreateDefault<TStartup>() where TStartup : class
        {
            var webHostBuilder  = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<TStartup>();

            return new ServiceBuilder(webHostBuilder);
        }

        IAutofacServiceBuilder IServiceBuilder.WithPort(int port)
        {
            _webHostBuilder.UseUrls($"http://*:{port}");
            return this;
        }

        IRabbitMqServiceBuilder IAutofacServiceBuilder.WithAutofac(Func<ContainerBuilder,ContainerBuilder> registrations)
        {
            _containerBuilder = registrations(new ContainerBuilder());
            return this;
        }

        IRabbitMqServiceBuilder IRabbitMqServiceBuilder.WithRabbitMq(string queueName, string username, string password, int port)
        {
            _containerBuilder.RegisterRawRabbit(new RawRabbitConfiguration
            {
                Username = username,
                Password = username,
                Port = port,
                VirtualHost = "/", 
                Queue = new GeneralQueueConfiguration()
                {
                    Durable = true,
                },
                RequestTimeout = new TimeSpan(0, 10, 0)
            });

            _customDependencyResolver = new CustomDependencyResolver(_containerBuilder.Build());

            _busClient = BusClientFactory.CreateDefault();
            _queueName = queueName;

            return this;
        }

        IRabbitMqServiceBuilder IRabbitMqServiceBuilder.SubscribeToCommand<TCommand>()
        {
            _busClient.SubscribeAsync<TCommand>(async (command, context) => 
            {
                var commandHandler = _customDependencyResolver.Resolve<ICommandHandler<TCommand>>();
                await commandHandler.HandleAsync(command);
            }, cfg => cfg.WithQueue(q => q.WithName(_queueName)));

            return this;
        }

        IRabbitMqServiceBuilder IRabbitMqServiceBuilder.SubscribeToEvent<TEvent>()
        {
            _busClient.SubscribeAsync<TEvent>(async (@event, context) => 
            {
                var commandHandler = _customDependencyResolver.Resolve<IEventHandler<TEvent>>();
                await commandHandler.HandleAsync(@event);
            }, cfg => cfg.WithQueue(q => q.WithName(_queueName)));

            return this;
        }

        IServiceBuilder IRabbitMqServiceBuilder.Build()
        {
            _webHost = _webHostBuilder.Build();
            return this;
        }

        void IServiceBuilder.Run()
        {
            _webHost.Run();
        }
    }
}