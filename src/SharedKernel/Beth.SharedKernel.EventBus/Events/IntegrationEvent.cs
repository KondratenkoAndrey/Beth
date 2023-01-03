using System;

namespace Beth.SharedKernel.EventBus.Events;

public record IntegrationEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}