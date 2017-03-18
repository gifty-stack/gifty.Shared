using System.Threading.Tasks;
using src.Interfaces.Contracts;

namespace src.Interfaces.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
         void Validate(TCommand command);
         Task HandleAsync(TCommand command);
    }
}