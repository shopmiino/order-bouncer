using System;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.Interfaces.Context;
using OrderBouncer.Application.Interfaces.Executors;
using OrderBouncer.Application.Interfaces.Processors;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Processors;

public class CreateRequestProcessorService : ICreateRequestProcessorService
{
    private readonly IOutboxExecutor _outbox;
    private readonly IJobContext _jobContext;
    private readonly ILogger<CreateRequestProcessorService> _logger;

    public CreateRequestProcessorService(IOutboxExecutor outbox, IJobContext jobContext, ILogger<CreateRequestProcessorService> logger){
        _outbox = outbox;
        _jobContext = jobContext;
        _logger = logger;
    }
    public async Task ProcessAsync(Guid jobId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{0}'s ProcessAsync is started with jobId: {1}", nameof(CreateRequestProcessorService), jobId);

        (OrderDto orderDto, bool success) context = _jobContext.TryGetObject<OrderDto>(jobId, p => p.ObjType == typeof(OrderDto));
        _logger.LogDebug("OrderDto retrieved from jobContext with jobId: {0}. Success code is {1}", jobId, context.success);

        _logger.LogDebug("Executing outbox with retrieved OrderDto, jobId: {0}", jobId);
        await _outbox.ExecuteAsync(context.orderDto, cancellationToken);
    }
}
