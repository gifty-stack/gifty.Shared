using System;
using Autofac;
using RawRabbit.Configuration;
using RawRabbit.DependencyInjection.Autofac;

namespace gifty.Shared.Extensions
{
    internal static class RawRabbitExtensions
    {
        internal static void RegisterRawRabbitWithAutofac(this ContainerBuilder containerBuilder, string queueName, string username, string password, int port)
            => containerBuilder.RegisterRawRabbit(new RawRabbitConfiguration
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
    }
}