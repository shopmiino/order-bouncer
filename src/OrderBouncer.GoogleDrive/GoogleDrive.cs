using System;
using Microsoft.Extensions.DependencyInjection;

namespace OrderBouncer.GoogleDrive;

public static class GoogleDrive
{
    public static IServiceCollection AddGoogleDrive(this IServiceCollection services){
        return services;
    }
}
