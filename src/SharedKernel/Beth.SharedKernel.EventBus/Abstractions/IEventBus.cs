using System.Threading.Tasks;
using Beth.SharedKernel.EventBus.Commands;
using Beth.SharedKernel.EventBus.Events;

namespace Beth.SharedKernel.EventBus.Abstractions;

public interface IEventBus
{
    public Task PublishAsync<T>(T @event) where T : IntegrationEvent;
    public Task SendAsync<T>(T @event) where T : BaseCommand;
}