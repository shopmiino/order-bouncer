using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Interfaces.Services;

namespace OrderBouncer.GoogleDrive.Services;

public class GoogleDriveEngine : IGoogleDriveEngine
{
    public Task UploadOrder(OrderDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
