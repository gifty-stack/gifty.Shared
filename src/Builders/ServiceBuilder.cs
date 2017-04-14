using System.IO;
using Autofac;
using Microsoft.AspNetCore.Hosting;
using gifty.Shared.ServiceBus;
using gifty.Shared.Extensions;

namespace gifty.Shared.Builders
{
    public sealed class ServiceBuilder : IServiceBuilder, IAutofacServiceBuilder, IRabbitMqServiceBuilder
    {
        private IWebHostBuilder _webHostBuilder;

        private IWebHost _webHost;
        private ILifetimeScope _lifetimeScope;
        private IServiceBus _serviceBus;
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
            _webHost = _webHostBuilder
                .UseUrls($"http://*:{port}")
                .Build();

            return this;
        }

        IRabbitMqServiceBuilder IAutofacServiceBuilder.WithAutofac(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
            return this;
        }

        IRabbitMqServiceBuilder IRabbitMqServiceBuilder.WithRabbitMq(string queueName, string username, string password, int port)
        {
            _lifetimeScope.RegisterRawRabbit(queueName, username, password, port);
            _serviceBus = _lifetimeScope.Resolve<IServiceBus>();
            _queueName = queueName;

            return this;
        }

        IRabbitMqServiceBuilder IRabbitMqServiceBuilder.SubscribeToCommand<TCommand>()
        {
            _serviceBus.SubscribeToCommand<TCommand>(_queueName);
            return this;
        }

        IRabbitMqServiceBuilder IRabbitMqServiceBuilder.SubscribeToEvent<TEvent>()
        {
            _serviceBus.SubscribeToEvent<TEvent>(_queueName);
            return this;
        }

        IServiceBuilder IRabbitMqServiceBuilder.Build()
        {
            return this;
        }

        void IServiceBuilder.Run()
        {
            _webHost.Run();
        }
    }
}