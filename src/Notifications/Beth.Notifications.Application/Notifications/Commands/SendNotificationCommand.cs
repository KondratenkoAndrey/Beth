using MediatR;

namespace Beth.Notifications.Application.Notifications.Commands
{
    public class SendNotificationCommand : IRequest
    {
        public string Message { get; }

        public SendNotificationCommand(string message)
        {
            Message = message;
        }
    }
}