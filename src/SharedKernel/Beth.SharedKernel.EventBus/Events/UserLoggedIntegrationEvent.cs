namespace Beth.SharedKernel.EventBus.Events;

public record UserLoggedIntegrationEvent(string MobilePhone) : IntegrationEvent
{
}