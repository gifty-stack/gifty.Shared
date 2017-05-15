using Autofac;
using gifty.Shared.Neo4j;
using Nancy.Bootstrappers.Autofac;

namespace gifty.Shared.Extensions
{
    internal static class Neo4jExtensions
    {
        internal static void RegisterNeo4j(this ILifetimeScope lifetimeScope, string boltEndpoint, string login, string password)
        {
            lifetimeScope.Update(builder => 
            {
                builder.RegisterInstance(new Neo4jSettings 
                {
                    BoltEndpoint = boltEndpoint,
                    Login = login,
                    Password = password
                }).SingleInstance();

                builder.RegisterType<Neo4jSession>().As<INeo4jSession>();
                builder.RegisterType<Neo4jSessionProvider>().As<INeo4jSessionProvider>();
            });
        }
    }
}