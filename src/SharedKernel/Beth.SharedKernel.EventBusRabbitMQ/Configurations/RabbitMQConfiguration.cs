namespace Beth.SharedKernel.EventBus.RabbitMQ.Configurations;

public record RabbitMQConfiguration
{
    public string Url { get; init; }
    public ushort Port { get; init; }
    public string Host { get; init; }
    public string User { get; init; }
    public string Password { get; init; }
}