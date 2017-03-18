using Autofac;

namespace src.IoC
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