using System.Threading.Tasks;
using src.CQRS.Contracts;

namespace src.CQRS.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
         void Validate(TCommand command);
         Task HandleAsync(TCommand command);
    }
}