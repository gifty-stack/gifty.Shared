using System.Threading.Tasks;
using src.CQRS.Contracts;

namespace src.CQRS.Handlers
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}