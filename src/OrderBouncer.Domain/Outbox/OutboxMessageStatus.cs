using System;

namespace OrderBouncer.Domain.Outbox;

public enum OutboxMessageStatus
{
    Pending,
    Processed,
    Failed
}
