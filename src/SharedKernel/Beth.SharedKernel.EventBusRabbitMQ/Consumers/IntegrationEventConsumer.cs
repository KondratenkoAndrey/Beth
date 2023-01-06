using System.Threading.Tasks;
using Beth.SharedKernel.EventBus.Events;
using MassTransit;

namespace Beth.SharedKernel.EventBus.RabbitMQ.Consumers;

public abstract class IntegrationEventConsumer<T> : IConsumer<T> where T : IntegrationEvent
{
    public async Task Consume(ConsumeContext<T> context)
    {
        await HandleEventAsync(context.Message);
    }

    protected abstract Task HandleEventAsync(T integrationEvent);
}