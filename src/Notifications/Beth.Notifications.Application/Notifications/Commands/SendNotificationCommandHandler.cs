using System.Threading;
using System.Threading.Tasks;
using Beth.Notifications.Application.Common.Interfaces;
using Beth.Notifications.Domain.Entities;
using MediatR;

namespace Beth.Notifications.Application.Notifications.Commands
{
    public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand>
    {
        private readonly INotificationSender _sender;

        public SendNotificationCommandHandler(INotificationSender sender)
        {
            _sender = sender;
        }
        
        public async Task<Unit> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = new Notification(request.Message);

            await _sender.SendNotificationAsync(notification, cancellationToken);
            
            return Unit.Value;
        }
    }
}