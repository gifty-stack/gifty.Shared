using gifty.Shared.IoC;

namespace gifty.Shared.Neo4j
{
    public class Neo4jSessionProvider : INeo4jSessionProvider
    {
        private readonly ICustomDependencyResolver _customDependencyResolver;

        public Neo4jSessionProvider(ICustomDependencyResolver customDependencyResolver)
        {
            _customDependencyResolver = customDependencyResolver;
        }

        public INeo4jSession Create()
            => _customDependencyResolver.Resolve<INeo4jSession>();
    }
}