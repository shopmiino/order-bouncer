using System;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.Interfaces.OutboxPublisher;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.Domain.Outbox;
using OrderBouncer.GoogleSheets.Interfaces;

namespace OrderBouncer.GoogleSheets.Services;

public class GoogleSheetsOutboxPublisher : IOutboxPublisher
{
    private readonly IGoogleSheetsEngine _engine;
    public PublisherTargetSystem TargetSystem => PublisherTargetSystem.GoogleSheets;
    private readonly ILogger<GoogleSheetsOutboxPublisher> _logger;

    public GoogleSheetsOutboxPublisher(IGoogleSheetsEngine engine, ILogger<GoogleSheetsOutboxPublisher> logger){
        _engine = engine;
        _logger = logger;
    }

    public async Task PublishAsync(OrderDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("PublishAsync is start for GoogleSheets with jobId: {0}", dto.ScopeId);

        try{
            await _engine.UploadOrder(dto, cancellationToken);
        } catch (Exception ex) {
            _logger.LogError("An error occured while uploading order to GoogleSheets\nmessage: {0}\nstackTrace: {1}", ex.Message, ex.StackTrace);
        }
    }
}
