using System;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.Interfaces.Infrastructure.Services;
using OrderBouncer.Application.Interfaces.OutboxPublisher;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.Domain.Outbox;
using OrderBouncer.GoogleDrive.Interfaces;
using OrderBouncer.GoogleDrive.Interfaces.Architectors;
using OrderBouncer.GoogleDrive.Interfaces.Services;

namespace OrderBouncer.GoogleDrive.Services;

public class GoogleDriveOutboxPublisher : IOutboxPublisher
{
    private readonly IGoogleDriveEngine _engine;
    private readonly IFileCleanupService _cleanupService;
    private readonly ILogger<GoogleDriveOutboxPublisher> _logger;

    public GoogleDriveOutboxPublisher(IFileCleanupService cleanupService, IGoogleDriveEngine engine, ILogger<GoogleDriveOutboxPublisher> logger){
        _cleanupService = cleanupService;
        _engine = engine;
        _logger = logger;
    }
    public PublisherTargetSystem TargetSystem => PublisherTargetSystem.GoogleDrive;

    public async Task PublishAsync(OrderDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("PublishAsync is started for GoogleDrive with jobId {0}", dto.ScopeId);
        
        try{
            await _engine.UploadOrder(dto, cancellationToken);
        } catch (Exception ex) {
            _logger.LogError("An error occured while uploading order to GoogleDrive\nmessage: {0}\nstackTrace: {1}", ex.Message, ex.StackTrace);
        } finally {
            _cleanupService.Cleanup(dto.ScopeId);
        }
    }
}
