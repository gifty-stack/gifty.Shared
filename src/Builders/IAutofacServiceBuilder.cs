using Autofac;

namespace gifty.Shared.Builders
{
    public interface IAutofacServiceBuilder
    {
         IRabbitMqServiceBuilder WithAutofac(ILifetimeScope lifetimeScope);
    }
}