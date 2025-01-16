using System;
using OrderBouncer.Domain.Outbox;

namespace OrderBouncer.Application.Interfaces.OutboxPublisher;

public interface IOutboxPublisher
{
    public PublisherTargetSystem TargetSystem {get;}
    public Task PublishBytesAsync(byte[] fileContent, CancellationToken cancellationToken);
    public Task PublishPathAsync(string filePath, CancellationToken cancellationToken);
}
