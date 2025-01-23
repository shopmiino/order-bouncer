using System;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Interfaces.Executors;

public interface IOutboxExecutor
{
    public Task ExecuteBytesAsync(byte[] fileContent, CancellationToken cancellationToken);
    public Task ExecutePathAsync(string filePath, CancellationToken cancellationToken);
    public Task ExecuteAsync(OrderDto dto, CancellationToken cancellationToken);
}
