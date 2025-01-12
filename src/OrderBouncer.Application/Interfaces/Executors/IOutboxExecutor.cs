using System;

namespace OrderBouncer.Application.Interfaces.Executors;

public interface IOutboxExecutor
{
    public Task ExecuteAsync(byte[] fileContent, CancellationToken cancellationToken);
}
