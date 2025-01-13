using System;
using OrderBouncer.Application.Interfaces.OutboxPublisher;
using OrderBouncer.Domain.Outbox;

namespace OrderBouncer.GoogleDrive.Services;

public class GoogleDriveOutboxPublisher : IOutboxPublisher
{
    public PublisherTargetSystem TargetSystem => PublisherTargetSystem.GoogleDrive;

    public Task PublishAsync(byte[] fileContent, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
