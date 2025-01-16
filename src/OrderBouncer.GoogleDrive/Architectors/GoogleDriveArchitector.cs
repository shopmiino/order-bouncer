using System;
using System.Text.Json.Serialization;
using OrderBouncer.GoogleDrive.Constants;
using OrderBouncer.GoogleDrive.Interfaces;
using OrderBouncer.GoogleDrive.Interfaces.Architectors;

namespace OrderBouncer.GoogleDrive.Architectors;

public class GoogleDriveArchitector : IGoogleDriveArchitector
{
    private readonly IGoogleDriveRepository _repository;
    public GoogleDriveArchitector(IGoogleDriveRepository repository)
    {  
        _repository = repository;
    }

    //TODO: I can do this in more clever way but it is unnecessary rn. maybe later...
    public async Task Execute(int orderId)
    {
        int accessoryCount = 2;
        int petCount = 3;
        int figureCount = 1;
        int figureAccessoryCount = 2;
        int keychainCount = 4;

        string generalFolderId = await _repository.CreateFolder(orderId.ToString());

        await GenerateFolders(FolderNamesEnum.Accessory, generalFolderId, accessoryCount);
        await GenerateFolders(FolderNamesEnum.Figure, generalFolderId, figureCount);
        await GenerateFolders(FolderNamesEnum.Keychain, generalFolderId, keychainCount);
        await GenerateFolders(FolderNamesEnum.Pet, generalFolderId, petCount);
       
    }

    private async Task<GoogleDriveArchitector> GenerateFolders(FolderNamesEnum namesEnum, string parentId, int count){
        string accessoryFolderId = await _repository.CreateFolder($"{FolderNames.Names[namesEnum]} ({count} Tane)", parentId);
        (List<string> accessoryItemFolderIds, List<string> accessoryItemImageFolderIds) accessoryIds = await GenerateSubFolders(accessoryFolderId, count);

        return this;
    }
    private async Task<(List<string> folderIds, List<string> imageFolderIds)> GenerateSubFolders(string parentFolderId, int itemCount){
        List<string> itemFolderIds = [];
        List<string> imageIds = [];

        for(int i = 0; i < itemCount; i++){
            string id = await _repository.CreateFolder((i + 1).ToString(), parentFolderId);

            string imageFolderId = await _repository.CreateFolder("Resimler", id);

            imageIds.Add(imageFolderId);
            itemFolderIds.Add(id);
        }

        return (itemFolderIds, imageIds);
    }

    /*

    private async Task 

    */
}
