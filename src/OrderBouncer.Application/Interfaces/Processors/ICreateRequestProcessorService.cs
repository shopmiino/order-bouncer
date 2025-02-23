using System;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Interfaces.Processors;

public interface ICreateRequestProcessorService
{
    public Task ProcessAsync(Guid jobId, CancellationToken cancellationToken);
}
