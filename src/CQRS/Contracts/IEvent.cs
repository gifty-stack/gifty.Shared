using System;

namespace src.CQRS.Contracts
{
    public interface IEvent
    {
        Guid Id { get; }   
    }
}