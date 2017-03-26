using System;
using Autofac;
using gifty.Shared.IoC;

namespace gifty.Shared.Builders
{
    public interface IAutofacServiceBuilder
    {
         IRabbitMqServiceBuilder WithAutofac(Func<ContainerBuilder, ContainerBuilder> registrations);
    }
}