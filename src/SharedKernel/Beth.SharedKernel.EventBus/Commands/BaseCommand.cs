using System;

namespace Beth.SharedKernel.EventBus.Commands;

public record BaseCommand
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}