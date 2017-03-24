using System;
using Autofac;
using src.IoC;

namespace src.Builders
{
    public interface IAutofacServiceBuilder
    {
         IRabbitMqServiceBuilder WithAutofac(Func<ContainerBuilder, ICustomDependencyResolver> registrations);
    }
}