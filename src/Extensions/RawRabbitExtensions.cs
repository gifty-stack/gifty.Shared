using System;
using Autofac;
using gifty.Shared.IoC;
using gifty.Shared.ServiceBus;
using Nancy.Bootstrappers.Autofac;
using RawRabbit.Configuration;
using RawRabbit.DependencyInjection.Autofac;

namespace gifty.Shared.Extensions
{
    internal static class RawRabbitExtensions
    {
        internal static void RegisterRawRabbit(this ILifetimeScope container, string queueName, string username, string password, int port)
        {
            container.Update(builder => 
            {
                builder.RegisterRawRabbit(new RawRabbitConfiguration
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

                builder.RegisterInstance<ICustomDependencyResolver>(new CustomDependencyResolver(container));
                builder.RegisterType<gifty.Shared.ServiceBus.ServiceBus>().As<IServiceBus>();   
            });

            Console.WriteLine($"TEST REGISTRATION: {container.Resolve<IServiceBus>().GetType()}");
        }        
    }
}