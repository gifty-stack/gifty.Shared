using System;

namespace gifty.Shared.Domain
{
    public interface IDomainObject
    {
         Guid Id { get; set; }
    }
}