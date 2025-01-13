using System;
using OrderBouncer.Application.Interfaces.OutboxPublisher;
using OrderBouncer.Domain.Outbox;
using OrderBouncer.GoogleSheets.Interfaces;

namespace OrderBouncer.GoogleSheets.Services;

public class GoogleSheetsOutboxPublisher : IOutboxPublisher
{
    private readonly IGoogleSheetsRepository _repository;
    public PublisherTargetSystem TargetSystem => PublisherTargetSystem.GoogleSheets;

    public GoogleSheetsOutboxPublisher(IGoogleSheetsRepository repository){
        _repository = repository;
    }

    public Task PublishAsync(byte[] fileContent, CancellationToken cancellationToken)
    {
        _repository.AddRow();
    }
}
