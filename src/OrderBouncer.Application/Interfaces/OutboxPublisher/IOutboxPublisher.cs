using System;
using OrderBouncer.Domain.Outbox;

namespace OrderBouncer.Application.Interfaces.OutboxPublisher;

public interface IOutboxPublisher
{
    public PublisherTargetSystem TargetSystem {get;}
    public Task PublishAsync(byte[] fileContent, CancellationToken cancellationToken);
}
