using System;
using OrderBouncer.Domain.Outbox;

namespace OrderBouncer.Application.Interfaces.Repositories;

public interface IOutboxRepository
{
    public Task AddAsync(OutboxMessage message);
    public Task<IEnumerable<OutboxMessage>> GetPendingMessagesAsync();
    public Task MarkAsProcessedAsync(Guid messageId);
}
