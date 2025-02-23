using System;
using OrderBouncer.Application.Interfaces.Context;
using OrderBouncer.Application.Interfaces.Executors;
using OrderBouncer.Application.Interfaces.Processors;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Processors;

public class CreateRequestProcessorService : ICreateRequestProcessorService
{
    private readonly IOutboxExecutor _outbox;
    private readonly IJobContext _jobContext;

    public CreateRequestProcessorService(IOutboxExecutor outbox, IJobContext jobContext){
        _outbox = outbox;
        _jobContext = jobContext;
    }
    public async Task ProcessAsync(Guid jobId, CancellationToken cancellationToken)
    {
        (OrderDto orderDto, bool success) context = _jobContext.TryGetObject<OrderDto>(jobId, p => p.ObjType == typeof(OrderDto));
        await _outbox.ExecuteAsync(context.orderDto, cancellationToken);
    }
}
