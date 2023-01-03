using Beth.SharedKernel.EventBus.Events;

namespace Beth.Identity.Api.IntegrationEvents;

public record UserLoggedIntegrationEvent(string MobilePhone) : IntegrationEvent
{
}