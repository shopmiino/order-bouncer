using System;
using OrderBouncer.Application.Interfaces.OutboxPublisher;
using OrderBouncer.Domain.Outbox;

namespace OrderBouncer.Infrastructure.OutboxPublishers;

public class MicrosoftExcelPublisher : IOutboxPublisher
{
    public PublisherTargetSystem TargetSystem => PublisherTargetSystem.MicrosoftExcel;

    public Task PublishAsync(byte[] fileContent, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
