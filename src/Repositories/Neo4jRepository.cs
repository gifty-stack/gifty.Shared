using System;
using gifty.Shared.Domain;

namespace gifty.Shared.Repositories
{
    public abstract class Neo4jRepository<TDomainObject> : IRepository<TDomainObject> where TDomainObject: IDomainObject
    {
        protected string NodeLabel { get; }
        protected Neo4jRepository(string nodeLabel)
        {
            this.NodeLabel = nodeLabel;
        }
        public virtual void Create(TDomainObject @object)
        {
            throw new NotImplementedException();
        }      

        public virtual void Update(TDomainObject @object)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}