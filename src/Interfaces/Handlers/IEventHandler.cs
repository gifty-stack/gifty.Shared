using System.Threading.Tasks;
using src.Interfaces.Contracts;

namespace src.Interfaces.Handlers
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}