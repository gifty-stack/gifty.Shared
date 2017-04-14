using System;
using Autofac;
using Nancy.Bootstrappers.Autofac;
using RawRabbit.Configuration;
using RawRabbit.DependencyInjection.Autofac;

namespace gifty.Shared.Extensions
{
    internal static class RawRabbitExtensions
    {
        internal static void RegisterRawRabbit(this ILifetimeScope container, string queueName, string username, string password, int port)
            =>
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
                });                
    }
}