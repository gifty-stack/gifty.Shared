using Autofac;

namespace gifty.Shared.Builders
{
    public interface IAutofacServiceBuilder
    {
         INeo4jServiceBuilder WithAutofac(ILifetimeScope lifetimeScope);
    }
}