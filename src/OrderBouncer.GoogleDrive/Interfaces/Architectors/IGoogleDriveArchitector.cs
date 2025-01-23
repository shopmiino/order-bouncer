using System;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.GoogleDrive.Interfaces.Architectors;

public interface IGoogleDriveArchitector
{
    public Task Execute(OrderDto dto, CancellationToken cancellationToken);
}
