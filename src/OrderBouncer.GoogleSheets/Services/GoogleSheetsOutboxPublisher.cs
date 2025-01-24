using System;
using OrderBouncer.Application.Interfaces.OutboxPublisher;
using OrderBouncer.Domain.DTOs.Base;
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

    public async Task PublishAsync(OrderDto dto, CancellationToken cancellationToken)
    {
        await _repository.AddRow(["John Doe", "2025-01-10", "ABC123", "Black", "Light", "Male", "true", "Type1", "Type2", "Print1", "Print2", "Pending", "false", "true", "Hat", "Dog", "Urgent delivery", "2025-01-20"], "A1:Z1");
    }
}
