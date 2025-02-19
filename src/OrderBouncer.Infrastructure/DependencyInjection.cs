using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderBouncer.Infrastructure.BackgroundServices;

namespace OrderBouncer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services){
        services.AddHostedService<OutboxProcessor>();

        return services;
    }

    public static IServiceCollection AddImageHttpClient(this IServiceCollection services, ILogger logger){
        services.AddHttpClient("ImageClient", client => {
            client.Timeout = TimeSpan.FromSeconds(10);
            client.DefaultRequestHeaders.ConnectionClose = false;
        })
        .ConfigurePrimaryHttpMessageHandler(()=>{
            return new HttpClientHandler{
                MaxConnectionsPerServer = 50,
                AutomaticDecompression = System.Net.DecompressionMethods.None
            };
        })
        .AddPolicyHandler(ImageClientPolicies.GetTimeoutPolicy())
        .AddPolicyHandler(ImageClientPolicies.GetRetryPolicy())
        .AddPolicyHandler(ImageClientPolicies.GetCircuitBreakerPolicy(logger));
        

        return services;
    }
}
