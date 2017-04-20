using Autofac;
using gifty.Shared.ServiceBus;

namespace gifty.Shared.IoC
{
    internal class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<CustomDependencyResolver>().As<ICustomDependencyResolver>();  
            containerBuilder.RegisterType<gifty.Shared.ServiceBus.ServiceBus>().As<IServiceBus>();
        }
    }
}