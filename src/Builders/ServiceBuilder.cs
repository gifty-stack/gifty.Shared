using System;
using System.IO;
using Autofac;
using gifty.Shared.IoC;
using Microsoft.AspNetCore.Hosting;
using gifty.Shared.Extensions;
using gifty.Shared.ServiceBus;

namespace gifty.Shared.Builders
{
    public sealed class ServiceBuilder : IServiceBuilder, IAutofacServiceBuilder, IRabbitMqServiceBuilder
    {
        private IWebHostBuilder _webHostBuilder;

        private IWebHost _webHost;
        private ContainerBuilder _containerBuilder;
        private ICustomDependencyResolver _customDependencyResolver;
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
            _webHostBuilder.UseUrls($"http://*:{port}");
            return this;
        }

        IRabbitMqServiceBuilder IAutofacServiceBuilder.WithAutofac(Func<ContainerBuilder,ContainerBuilder> registrations)
        {
            _containerBuilder = registrations(new ContainerBuilder());
            return this;
        }

        IRabbitMqServiceBuilder IAutofacServiceBuilder.WithoutAutofac()
        {
            return this;
        }

        IRabbitMqServiceBuilder IRabbitMqServiceBuilder.WithRabbitMq(string queueName, string username, string password, int port)
        {
            _containerBuilder.RegisterRawRabbitWithAutofac(queueName, username, password, port);
            _containerBuilder.RegisterType<gifty.Shared.ServiceBus.ServiceBus>().As<IServiceBus>();
            var container = _containerBuilder.Build();

            var customDependencyBuilder = new ContainerBuilder();

            customDependencyBuilder.RegisterInstance<ICustomDependencyResolver>(new CustomDependencyResolver(container));
            customDependencyBuilder.Update(container);

            _serviceBus = container.Resolve<IServiceBus>();
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
            _webHost = _webHostBuilder.Build();
            return this;
        }

        void IServiceBuilder.Run()
        {
            _webHost.Run();
        }
    }
}