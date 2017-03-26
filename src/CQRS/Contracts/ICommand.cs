using System;

namespace gifty.Shared.CQRS.Contracts
{
    public interface ICommand
    {
         Guid Id { get; }
    }
}