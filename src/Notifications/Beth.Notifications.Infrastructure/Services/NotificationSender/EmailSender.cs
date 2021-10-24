﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Beth.Notifications.Application.Common.Interfaces;
using Beth.Notifications.Domain.Entities;

namespace Beth.Notifications.Infrastructure.Services.NotificationSender
{
    public class EmailSender : INotificationSender
    {
        public async Task SendNotificationAsync(Notification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Notification was sent via email with message \"{notification.Message}\"", "test");
            await Task.CompletedTask;
        }
    }
}