using System;

namespace src.Interfaces.Contracts
{
    public interface ICommand
    {
         Guid Id { get; }
    }
}