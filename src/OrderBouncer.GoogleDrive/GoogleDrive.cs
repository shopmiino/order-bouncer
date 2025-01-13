using System;
using Microsoft.Extensions.DependencyInjection;
using OrderBouncer.Application.Interfaces.OutboxPublisher;
using OrderBouncer.GoogleDrive.Services;

namespace OrderBouncer.GoogleDrive;

public static class GoogleDrive
{
    public static IServiceCollection AddGoogleDrive(this IServiceCollection services){
        services.AddScoped<IOutboxPublisher, GoogleDriveOutboxPublisher>();
        return services;
    }
}
