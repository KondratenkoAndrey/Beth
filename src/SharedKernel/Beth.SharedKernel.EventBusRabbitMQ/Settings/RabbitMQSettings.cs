namespace Beth.SharedKernel.EventBusRabbitMQ.Settings;

public class RabbitMQSettings
{
    public string Host { get; set; }
    public ushort Port { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
}