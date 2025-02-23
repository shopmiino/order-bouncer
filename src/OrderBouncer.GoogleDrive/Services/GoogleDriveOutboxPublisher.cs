using System;
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

    public GoogleDriveOutboxPublisher(IFileCleanupService cleanupService, IGoogleDriveEngine engine){
        _cleanupService = cleanupService;
        _engine = engine;
    }
    public PublisherTargetSystem TargetSystem => PublisherTargetSystem.GoogleDrive;

    public async Task PublishAsync(OrderDto dto, CancellationToken cancellationToken)
    {
        await _engine.UploadOrder(dto, cancellationToken);
        _cleanupService.Cleanup(dto.ScopeId);
    }
}
