using System;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.GoogleDrive.Interfaces.Services;

public interface IGoogleDriveEngine
{
    public Task UploadOrder(OrderDto dto, CancellationToken cancellationToken);
}
