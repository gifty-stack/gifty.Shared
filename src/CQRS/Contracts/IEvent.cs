using System;

namespace gifty.Shared.CQRS.Contracts
{
    public interface IEvent
    {
        Guid Id { get; }   
    }
}