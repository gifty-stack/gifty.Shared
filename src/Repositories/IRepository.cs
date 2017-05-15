using System;
using gifty.Shared.Domain;

namespace gifty.Shared.Repositories
{
    public interface IRepository<TDomainObject> where TDomainObject : IDomainObject
    {
         void Create(TDomainObject @object);
         void Update(TDomainObject @object);
         void Delete(Guid id);                 
    }
}