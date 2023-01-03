using System;
using Beth.SharedKernel.EventBus.Abstractions;
using Beth.SharedKernel.EventBusRabbitMQ.Settings;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Beth.SharedKernel.EventBusRabbitMQ.Extensions;

public static class MassTransitExtension
{
    public static IServiceCollection AddMassTransitEventBus(this IServiceCollection services, RabbitMQSettings rabbitMqSettings)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqSettings.Host, rabbitMqSettings.Port, "/", h =>
                {
                    h.Username(rabbitMqSettings.User);
                    h.Password(rabbitMqSettings.Password);
                });
            });
        });
        
        services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                options.WaitUntilStarted = true;
                options.StartTimeout = TimeSpan.FromSeconds(10);
                options.StopTimeout = TimeSpan.FromSeconds(30);
            });

        services.AddScoped<IEventBus, RabbitMQEventBus>();

        return services;
    }
}