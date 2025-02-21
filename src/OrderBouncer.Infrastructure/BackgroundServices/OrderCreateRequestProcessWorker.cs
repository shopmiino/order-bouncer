using System;
using Hangfire;
using Microsoft.Extensions.Hosting;
using OrderBouncer.Application.Interfaces.Buffer;
using OrderBouncer.Application.Interfaces.Processors;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Infrastructure.BackgroundServices;

public class OrderCreateRequestProcessWorker : BackgroundService
{
    private readonly ICreateRequestBufferService _bufferService;
    private readonly IBackgroundJobClient _backgroundJobClient;

    public OrderCreateRequestProcessWorker(ICreateRequestBufferService bufferService, IBackgroundJobClient backgroundJobClient){
        _bufferService = bufferService;
        _backgroundJobClient = backgroundJobClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (OrderDto request in _bufferService.Reader.ReadAllAsync(stoppingToken)){
            if(stoppingToken.IsCancellationRequested) break;

            _backgroundJobClient.Enqueue<ICreateRequestProcessorService>(service => service.ProcessAsync(request, stoppingToken));
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken){
        await base.StopAsync(cancellationToken);
    }
}
