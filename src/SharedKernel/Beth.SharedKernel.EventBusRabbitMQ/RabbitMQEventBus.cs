using System.Threading.Tasks;
using Beth.SharedKernel.EventBus.Abstractions;
using Beth.SharedKernel.EventBus.Events;
using MassTransit;

namespace Beth.SharedKernel.EventBus.RabbitMQ;

public class RabbitMQEventBus : IEventBus
{
    readonly IPublishEndpoint _publishEndpoint;

    public RabbitMQEventBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishAsync<T>(T @event) where T : IntegrationEvent
    {
        await _publishEndpoint.Publish(@event);
    }
}