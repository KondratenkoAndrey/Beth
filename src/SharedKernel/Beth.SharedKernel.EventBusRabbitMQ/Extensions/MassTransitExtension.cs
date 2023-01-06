using System;
using Beth.SharedKernel.EventBus.Abstractions;
using Beth.SharedKernel.EventBus.RabbitMQ.Configurations;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Beth.SharedKernel.EventBus.RabbitMQ.Extensions;

public static class MassTransitExtension
{
    public static IServiceCollection AddMassTransitEventBus(
        this IServiceCollection services,
        RabbitMQConfiguration rabbitMQConfiguration,
        Action<IBusRegistrationConfigurator>? busConfigurator = null)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(
                    rabbitMQConfiguration.Url,
                    rabbitMQConfiguration.Port,
                    rabbitMQConfiguration.Host,
                    configurator =>
                    {
                        configurator.Username(rabbitMQConfiguration.User);
                        configurator.Password(rabbitMQConfiguration.Password);
                    });
                cfg.ConfigureEndpoints(context);
            });
            busConfigurator?.Invoke(x);
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