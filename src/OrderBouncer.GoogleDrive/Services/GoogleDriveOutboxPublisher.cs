using System;
using OrderBouncer.Application.Interfaces.OutboxPublisher;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.Domain.Outbox;
using OrderBouncer.GoogleDrive.Interfaces;
using OrderBouncer.GoogleDrive.Interfaces.Architectors;

namespace OrderBouncer.GoogleDrive.Services;

public class GoogleDriveOutboxPublisher : IOutboxPublisher
{
    private readonly IGoogleDriveRepository _repository;
    private readonly IGoogleDriveArchitector _architector;

    public GoogleDriveOutboxPublisher(IGoogleDriveRepository repository, IGoogleDriveArchitector architector){
        _repository = repository;
        _architector = architector;
    }
    public PublisherTargetSystem TargetSystem => PublisherTargetSystem.GoogleDrive;

    public async Task PublishAsync(OrderDto dto, CancellationToken cancellationToken)
    {
        await _architector.Execute(dto, cancellationToken);
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
