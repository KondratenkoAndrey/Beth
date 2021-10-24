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
            SendNotification("Test message");
            Console.ReadKey();
        }

        private static void SendNotification(string message)
        {
            var command = new SendNotificationCommand(message);
            INotificationSender sender = new Sender();
            var handler = new SendNotificationCommandHandler(sender);
            handler.Handle(command, CancellationToken.None);
        }
    }
}