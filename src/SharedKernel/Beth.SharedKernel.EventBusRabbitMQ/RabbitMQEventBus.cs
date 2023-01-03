using System.Threading.Tasks;
using Beth.SharedKernel.EventBus.Abstractions;
using Beth.SharedKernel.EventBus.Events;
using MassTransit;

namespace Beth.SharedKernel.EventBusRabbitMQ;

public class RabbitMQEventBus : IEventBus
{
    readonly IPublishEndpoint _publishEndpoint;

    public RabbitMQEventBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishAsync(IntegrationEvent @event)
    {
        await _publishEndpoint.Publish(@event);
    }
}