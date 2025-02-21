using System;
using Hangfire;
using Hangfire.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.Interfaces.HttpClients;
using OrderBouncer.Application.Interfaces.Infrastructure.Services;
using OrderBouncer.Infrastructure.BackgroundServices;
using OrderBouncer.Infrastructure.ExternalHttp;
using OrderBouncer.Infrastructure.Services;

namespace OrderBouncer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration){
        //services.AddHostedService<OutboxProcessor>();

        services.ConfigureHttpClientServices();

        services.ConfigureHangfire(configuration);

        services.AddScoped<IFileCleanupService, FileCleanupService>();

        return services;
    }

    public static IServiceCollection ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        string? connString = configuration["Hangfire:SQLiteStorage"];
        if (connString is null) throw new ArgumentNullException("SQLite connection string is null");

        services.AddHangfire(config => config.UseSQLiteStorage(connString));

        services.AddHangfireServer(options =>
        {
            options.WorkerCount = 1;
        });
        services.AddHostedService<OrderCreateRequestProcessWorker>();

        return services;
    }
    public static WebApplication ConfigureHangfireDashboard(this WebApplication app)
    {
        app.UseHangfireDashboard();

        return app;
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

    public static IServiceCollection ConfigureHttpClientServices(this IServiceCollection services){
        services.AddScoped<IImageFetcherService, ImageFetcherService>();
        services.AddScoped<IImageSaverService, ImageSaverService>();
        return services;
    }
}
