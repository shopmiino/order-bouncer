using System;
using System.Text.Json.Serialization;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;
using OrderBouncer.GoogleDrive.Interfaces;
using OrderBouncer.GoogleDrive.Interfaces.Architectors;
using OrderBouncer.GoogleDrive.Interfaces.Helpers;
using OrderBouncer.GoogleDrive.Interfaces.UseCases;

namespace OrderBouncer.GoogleDrive.Architectors;

public class GoogleDriveArchitector : IGoogleDriveArchitector
{
    private readonly IArchitectorHelperService _helper;
    private readonly IGoogleDriveRepository _repository;
    public GoogleDriveArchitector(IArchitectorHelperService helper, IGoogleDriveRepository repository)
    {  
        _helper = helper;
        _repository = repository;
    }

    //TODO: I made it more clever but now it is so much complex ://
    public async Task ExecuteAsync(OrderDto dto, ICollection<FolderNamesEnum> folders, CancellationToken cancellationToken)
    {
        string generalFolderId = await _repository.CreateFolder(dto.ShopifyOrderID); //1007

        foreach(FolderNamesEnum folder in folders){
            await _helper.Generate(dto.Products, folder, generalFolderId);
        }
    }
}
