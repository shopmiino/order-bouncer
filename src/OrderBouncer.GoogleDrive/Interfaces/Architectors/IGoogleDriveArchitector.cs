using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;

namespace OrderBouncer.GoogleDrive.Interfaces.Architectors;

public interface IGoogleDriveArchitector
{
    public Task ExecuteAsync(OrderDto dto, ICollection<FolderNamesEnum> folders, CancellationToken cancellationToken);
}
    