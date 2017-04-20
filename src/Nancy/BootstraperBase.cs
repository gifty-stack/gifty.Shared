using Autofac;
using Autofac.Extensions.DependencyInjection;
using gifty.Shared.IoC;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;

namespace gifty.Shared.Nancy
{
    public abstract class BootstrapperBase : AutofacNancyBootstrapper
    {
        public static ILifetimeScope BootstraperLifetimeScope;
        private readonly IServiceCollection _services;

        public BootstrapperBase(IServiceCollection services)
        {
            _services = services;
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {         
            container.Update(builder => 
            {                
                builder.RegisterModule(new ServiceModule());
                builder.Populate(_services);
            });

            base.ConfigureApplicationContainer(container);

            BootstraperLifetimeScope = container;
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            pipelines.OnError += OnErrorHook.OnError;
        }
    }
}