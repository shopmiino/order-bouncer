using System;
using Serilog;

namespace OrderBouncer.Web;

public static class SerilogExtension
{
    public static IHostBuilder ConfigureSerilog(this IHostBuilder host){
        host.UseSerilog((context, services, configuration) => {
            configuration.ReadFrom.Configuration(context.Configuration);
        });
        return host;
    }
}
