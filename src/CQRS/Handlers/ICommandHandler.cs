using System.Threading.Tasks;
using gifty.Shared.CQRS.Contracts;

namespace gifty.Shared.CQRS.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
         void Validate(TCommand command);
         Task HandleAsync(TCommand command);
    }
}