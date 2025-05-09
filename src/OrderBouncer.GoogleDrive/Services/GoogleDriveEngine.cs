using System;
using Microsoft.Extensions.Logging;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;
using OrderBouncer.GoogleDrive.Interfaces.Architectors;
using OrderBouncer.GoogleDrive.Interfaces.Services;

namespace OrderBouncer.GoogleDrive.Services;

public class GoogleDriveEngine : IGoogleDriveEngine
{
    private readonly IGoogleDriveArchitector _architector;
    private readonly ILogger<GoogleDriveEngine> _logger;

    public GoogleDriveEngine(IGoogleDriveArchitector architector, ILogger<GoogleDriveEngine> logger){
        _architector = architector;
        _logger = logger;
    }

    public async Task UploadOrder(OrderDto dto, CancellationToken cancellationToken)
    {
        List<FolderNamesEnum> types = [FolderNamesEnum.Accessory, FolderNamesEnum.Figure, FolderNamesEnum.Keychain, FolderNamesEnum.Pet];
        try{
            await _architector.ExecuteAsync(dto,types,cancellationToken);
        } catch(Exception ex){
            _logger.LogError(ex, "An error occured while trying to {0} in {1}", nameof(UploadOrder), nameof(GoogleDriveEngine));
        }
    }
}
