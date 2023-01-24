using System.Threading.Tasks;
using Beth.SharedKernel.EventBus.Abstractions;
using Beth.SharedKernel.EventBus.Commands;
using Beth.SharedKernel.EventBus.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Beth.SharedKernel.EventBus.RabbitMQ;

public class RabbitMQEventBus : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<RabbitMQEventBus> _logger;

    public RabbitMQEventBus(IPublishEndpoint publishEndpoint, ILogger<RabbitMQEventBus> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task PublishAsync<T>(T @event) where T : IntegrationEvent
    {
        await _publishEndpoint.Publish(@event);
        _logger.LogInformation("{event}", @event);
    }

    public async Task SendAsync<T>(T @event) where T : BaseCommand
    {
        await _publishEndpoint.Publish(@event);
        _logger.LogInformation("{event}", @event);
    }
}