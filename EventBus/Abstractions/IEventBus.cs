using EventBus.Events;
using System.Threading.Tasks;

namespace EventBus.Abstractions;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent @event);
}
