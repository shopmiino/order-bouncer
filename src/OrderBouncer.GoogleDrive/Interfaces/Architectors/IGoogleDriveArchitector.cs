using System;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.GoogleDrive.Interfaces.Architectors;

public interface IGoogleDriveArchitector
{
    public Task ExecuteAsync(OrderDto dto, CancellationToken cancellationToken);
}
