using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.Domain.Outbox;

namespace OrderBouncer.Application.Interfaces.OutboxPublisher;

public interface IOutboxPublisher
{
    public PublisherTargetSystem TargetSystem {get;}
    public Task PublishAsync(OrderDto dto, CancellationToken cancellationToken);
}
