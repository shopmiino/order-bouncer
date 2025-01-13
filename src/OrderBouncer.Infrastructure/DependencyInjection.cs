using System;
using Microsoft.Extensions.DependencyInjection;
using OrderBouncer.Infrastructure.BackgroundServices;

namespace OrderBouncer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services){
        services.AddHostedService<OutboxProcessor>();

        return services;
    }
}
