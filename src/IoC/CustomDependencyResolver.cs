using Autofac;

namespace gifty.Shared.IoC
{
    public class CustomDependencyResolver : ICustomDependencyResolver
    {
        private readonly IContainer _container;

        public CustomDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public TResolved Resolve<TResolved>()
            => _container.Resolve<TResolved>();
    }
}