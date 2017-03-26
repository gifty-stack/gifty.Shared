using System.Threading.Tasks;
using gifty.Shared.CQRS.Contracts;

namespace gifty.Shared.CQRS.Handlers
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}