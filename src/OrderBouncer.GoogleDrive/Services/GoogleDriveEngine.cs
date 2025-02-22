using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;
using OrderBouncer.GoogleDrive.Interfaces.Architectors;
using OrderBouncer.GoogleDrive.Interfaces.Services;

namespace OrderBouncer.GoogleDrive.Services;

public class GoogleDriveEngine : IGoogleDriveEngine
{
    private readonly IGoogleDriveArchitector _architector;

    public GoogleDriveEngine(IGoogleDriveArchitector architector){
        _architector = architector;
    }

    public async Task UploadOrder(OrderDto dto, CancellationToken cancellationToken)
    {
        ICollection<FolderNamesEnum> types = [FolderNamesEnum.Accessory, FolderNamesEnum.Figure, FolderNamesEnum.Keychain, FolderNamesEnum.Pet];
        try{
            await _architector.ExecuteAsync(dto,types,cancellationToken);
        } catch(Exception ex){
            throw;
        }
    }
}
