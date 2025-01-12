using System;
using Microsoft.Extensions.DependencyInjection;
using OrderBouncer.Application.Interfaces.OutboxPublisher;
using OrderBouncer.Infrastructure.BackgroundServices;
using OrderBouncer.Infrastructure.OutboxPublishers;

namespace OrderBouncer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services){
        services.AddScoped<IOutboxPublisher, GoogleDrivePublisher>();
        services.AddScoped<IOutboxPublisher, MicrosoftExcelPublisher>();
        
        services.AddHostedService<OutboxProcessor>();

        return services;
    }
}
