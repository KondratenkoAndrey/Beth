using System;
using System.Threading;
using Beth.Notifications.Application.Common.Interfaces;
using Beth.Notifications.Application.Notifications.Commands;
using Beth.Notifications.Infrastructure.Services.NotificationSender;

namespace Beth.Notifications.ServiceConsole
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            SendNotification("Test sms message", new SmsSender());
            SendNotification("Test email message", new EmailSender());
            Console.ReadKey();
        }

        private static void SendNotification(string message, INotificationSender sender)
        {
            var command = new SendNotificationCommand(message);
            var handler = new SendNotificationCommandHandler(sender);
            handler.Handle(command, CancellationToken.None);
        }
    }
}