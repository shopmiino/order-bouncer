using System;
using Microsoft.Extensions.Hosting;

namespace OrderBouncer.Infrastructure.BackgroundServices;

public class OutboxProcessor : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}
