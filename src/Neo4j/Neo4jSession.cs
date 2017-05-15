using System;
using Neo4j.Driver.V1;

namespace gifty.Shared.Neo4j
{
    internal sealed class Neo4jSession : INeo4jSession
    {
        private readonly IDriver _driver;
        private readonly ISession _session;
        
        public Neo4jSession(Neo4jSettings settings)
        {
            _driver = GraphDatabase.Driver(settings.BoltEndpoint, AuthTokens.Basic(settings.Login, settings.Password), Config.DefaultConfig);
            _session = _driver.Session();
        }

        IStatementResult INeo4jSession.Run(string query)
            => _session.Run(query);

        void IDisposable.Dispose()
        {
            _session.Dispose();
            _driver.Dispose();
        }        
    }
}