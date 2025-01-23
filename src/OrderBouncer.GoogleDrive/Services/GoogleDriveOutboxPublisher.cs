using System;
using OrderBouncer.Application.Interfaces.OutboxPublisher;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.Domain.Outbox;
using OrderBouncer.GoogleDrive.Interfaces;
using OrderBouncer.GoogleDrive.Interfaces.Architectors;
using OrderBouncer.GoogleDrive.Interfaces.Services;

namespace OrderBouncer.GoogleDrive.Services;

public class GoogleDriveOutboxPublisher : IOutboxPublisher
{
    private readonly IGoogleDriveRepository _repository;
    private readonly IGoogleDriveEngine _engine;

    public GoogleDriveOutboxPublisher(IGoogleDriveRepository repository, IGoogleDriveEngine engine){
        _repository = repository;
        _engine = engine;
    }
    public PublisherTargetSystem TargetSystem => PublisherTargetSystem.GoogleDrive;

    public async Task PublishAsync(OrderDto dto, CancellationToken cancellationToken)
    {
        await _engine.UploadOrder(dto, cancellationToken);
    }

    public Task PublishBytesAsync(byte[] fileContent, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task PublishPathAsync(string filePath, CancellationToken cancellationToken)
    {
        //string id = await _repository.CreateFolder("denemeic");
        //await _repository.UploadFile(filePath, id);
    }
}
