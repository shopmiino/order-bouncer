using System;
using OrderBouncer.Application.Interfaces.Repositories;
using OrderBouncer.Domain.Outbox;

namespace OrderBouncer.Infrastructure.Repositories;

public class OutboxRepository : IOutboxRepository
{
    public Task AddAsync(OutboxMessage message)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OutboxMessage>> GetPendingMessagesAsync()
    {
        throw new NotImplementedException();
    }

    public Task MarkAsProcessedAsync(Guid messageId)
    {
        throw new NotImplementedException();
    }
}
