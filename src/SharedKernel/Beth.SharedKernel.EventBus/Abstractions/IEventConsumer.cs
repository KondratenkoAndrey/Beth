using Beth.SharedKernel.EventBus.Events;

namespace Beth.SharedKernel.EventBus.Abstractions;

public interface IEventConsumer<T> where T : IntegrationEvent
{
}