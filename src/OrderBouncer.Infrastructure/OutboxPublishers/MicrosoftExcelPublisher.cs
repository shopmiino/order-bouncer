using System;
using OrderBouncer.Application.Interfaces.OutboxPublisher;

namespace OrderBouncer.Infrastructure.OutboxPublishers;

public class MicrosoftExcelPublisher : IOutboxPublisher
{
    public Task PublishAsync(string fileName, byte[] fileContent)
    {
        throw new NotImplementedException();
    }
}
