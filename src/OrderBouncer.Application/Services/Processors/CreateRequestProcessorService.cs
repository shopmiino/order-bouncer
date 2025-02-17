using System;
using OrderBouncer.Application.Interfaces.Executors;
using OrderBouncer.Application.Interfaces.Processors;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Processors;

public class CreateRequestProcessorService : ICreateRequestProcessorService
{
    private readonly IOutboxExecutor _outbox;

    public CreateRequestProcessorService(IOutboxExecutor outbox){
        _outbox = outbox;
    }
    public async Task ProcessAsync(OrderDto orderDto, CancellationToken cancellationToken)
    {
        await _outbox.ExecuteAsync(orderDto, cancellationToken);
    }
}
