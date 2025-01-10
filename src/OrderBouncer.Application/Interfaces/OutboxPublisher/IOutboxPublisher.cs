using System;

namespace OrderBouncer.Application.Interfaces.OutboxPublisher;

public interface IOutboxPublisher
{
    public Task PublishAsync(string fileName, byte[] fileContent);
}
