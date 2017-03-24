using System;

namespace src.CQRS.Contracts
{
    public interface ICommand
    {
         Guid Id { get; }
    }
}