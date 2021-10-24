using System.Threading;
using System.Threading.Tasks;
using Beth.Notifications.Domain.Entities;

namespace Beth.Notifications.Application.Common.Interfaces
{
    public interface INotificationSender
    {
        Task SendNotificationAsync(Notification notification, CancellationToken cancellationToken);
    }
}