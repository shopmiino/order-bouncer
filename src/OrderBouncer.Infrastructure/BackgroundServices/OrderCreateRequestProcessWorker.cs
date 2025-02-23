using System;
using Hangfire;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Buffer;
using OrderBouncer.Application.Interfaces.Processors;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Infrastructure.BackgroundServices;

public class OrderCreateRequestProcessWorker : BackgroundService
{
    private readonly ICreateRequestBufferService _bufferService;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly ILogger<OrderCreateRequestProcessWorker> _logger;

    public OrderCreateRequestProcessWorker(ICreateRequestBufferService bufferService, IBackgroundJobClient backgroundJobClient, ILogger<OrderCreateRequestProcessWorker> logger){
        _bufferService = bufferService;
        _backgroundJobClient = backgroundJobClient;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (OrderCreatedShopifyRequestDto request in _bufferService.Reader.ReadAllAsync(stoppingToken)){
            if(stoppingToken.IsCancellationRequested) break;
            try{
                _logger.LogInformation("Background service for OrderDto processing is Enqueing");

                Guid jobId = Guid.NewGuid();

                string id = _backgroundJobClient.Enqueue<ICreateRequestConvertProcessorService>(service => service.ConvertAndStoreAsync(request, jobId, stoppingToken));

                _backgroundJobClient.ContinueJobWith<ICreateRequestProcessorService>(id, (service) => service.ProcessAsync(jobId, stoppingToken));

                _logger.LogDebug("Job is enqueued with id of {0}", jobId);
            } catch(Exception ex){
                _logger.LogError("Error occurred while trying to enqueue backgroundJob, message: {0}", ex.Message);
            }
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken){
        await base.StopAsync(cancellationToken);
    }
}
