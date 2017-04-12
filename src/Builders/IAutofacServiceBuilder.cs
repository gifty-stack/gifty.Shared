using System;
using Autofac;

namespace gifty.Shared.Builders
{
    public interface IAutofacServiceBuilder
    {
         IRabbitMqServiceBuilder WithAutofac(Func<ContainerBuilder, ContainerBuilder> registrations);
         IRabbitMqServiceBuilder WithoutAutofac();
    }
}