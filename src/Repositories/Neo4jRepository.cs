using System;
using gifty.Shared.Builders;
using gifty.Shared.Domain;
using gifty.Shared.Neo4j;

namespace gifty.Shared.Repositories
{
    public abstract class Neo4jRepository<TDomainObject> : IRepository<TDomainObject> where TDomainObject: class, IDomainObject
    {
        protected INeo4jSessionProvider SessionProvider { get; }
        protected string NodeLabel { get; }
        protected string CypherVar => "x";

        protected Neo4jRepository(INeo4jSessionProvider sessionProvider, string nodeLabel)
        {
            this.SessionProvider = sessionProvider;
            this.NodeLabel = nodeLabel;
        }
        public virtual void Create(TDomainObject @object)
        { 
            var query = CypherQueryBuilder
                .Init()
                .Create("{0}", @object, CypherVar)
                .Return(CypherVar);

            using(var session = SessionProvider.Create())            
                session.Run(query);                    
        }

        public virtual void Update(TDomainObject @object)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(TDomainObject @object)
        {
            var query = CypherQueryBuilder
                .Init()
                .Match("{0}", new[] {@object}, new[] {CypherVar})
                .Delete(CypherVar);
            
            using(var session = SessionProvider.Create())
                session.Run(query);
        }
    }
}