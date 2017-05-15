using System;
using Neo4j.Driver.V1;

namespace gifty.Shared.Neo4j
{
    public interface INeo4jSession : IDisposable
    {
         IStatementResult Run(string query);
    }
}