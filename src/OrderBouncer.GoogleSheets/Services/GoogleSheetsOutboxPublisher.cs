using System;
using OrderBouncer.Application.Interfaces.OutboxPublisher;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.Domain.Outbox;
using OrderBouncer.GoogleSheets.Interfaces;

namespace OrderBouncer.GoogleSheets.Services;

public class GoogleSheetsOutboxPublisher : IOutboxPublisher
{
    private readonly IGoogleSheetsEngine _engine;
    public PublisherTargetSystem TargetSystem => PublisherTargetSystem.GoogleSheets;

    public GoogleSheetsOutboxPublisher(IGoogleSheetsEngine engine){
        _engine = engine;
    }

    public async Task PublishAsync(OrderDto dto, CancellationToken cancellationToken)
    {
        await _engine.UploadOrder(dto, cancellationToken);
    }
}
