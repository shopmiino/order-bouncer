using System;
using System.Text.Json.Serialization;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;
using OrderBouncer.GoogleDrive.DTOs;
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
    public async Task Execute(OrderDto dto, CancellationToken cancellationToken)
    {
        string generalFolderId = await _repository.CreateFolder(dto.ShopifyOrderID);

        foreach(var product in dto.Products){
            await GenerateGeneric<PetDto>(product.Pets, generalFolderId);
            foreach(PetDto ent in product.Pets){
                GoogleDriveEntityDto entityDto = new GoogleDriveEntityDto(ent.ImagePaths, FolderNames.TypeNames[ent.GetType()], generalFolderId, ent.Note);
                await GenerateFolders(entityDto);
            }
        }

/*
        await GenerateFolders(FolderNamesEnum.Accessory, generalFolderId, accessoryCount);
        await GenerateFolders(FolderNamesEnum.Figure, generalFolderId, figureCount);
        await GenerateFolders(FolderNamesEnum.Keychain, generalFolderId, keychainCount);
        await GenerateFolders(FolderNamesEnum.Pet, generalFolderId, petCount);
*/     
    }

    private async Task GenerateGeneric<T>(ICollection<T>? coll, string? parentId = null){
        if(coll is null){
            return;
        }

        FolderNamesEnum name = FolderNames.TypeNames[typeof(T)];
        int count = GetCount(coll);
        string folderName = GenerateFolderName(name, count);  //e.g. Evcil Hayvanlar (2 Tane)
        string entityFolderId = await _repository.CreateFolder(folderName, parentId);

        ICollection<string> itemFoldersIds = await GenerateItemFolders<T>(coll, entityFolderId); //Folder names follows 1, 2, 3, 4... 
        ICollection<string> itemSubFolderIds = await GenerateSubItemFolders<T>(coll, itemFoldersIds);
    }
    private async Task GenerateFolders(GoogleDriveEntityDto dto){
        int count = GetCount(dto.ImagePaths);
        
        string folderName = GenerateFolderName(dto.FolderName, count); 

        string accessoryFolderId = await _repository.CreateFolder(folderName, dto.ParentFolderId);
        GoogleDriveFolderDto folderDto = await GenerateSubFolders(accessoryFolderId, count);
        
    }

    private async Task<ICollection<string>> GenerateItemFolders<T>(FolderNamesEnum name, ICollection<T> collection, string parentId, ICollection<string>? parentIds = null){
        ICollection<string> itemFolderIds = [];

        for(int i = 0; i < collection.Count; i++){
            string itemFolderName = (i + 1).ToString();
            string itemFolderId = await _repository.CreateFolder(itemFolderName, parentId);

            itemFolderIds.Add(itemFolderId);
        }
        return itemFolderIds;
    }

    //TODO create two methods, manyForOne operates many subFolders for one parent, oneForMany operates one subfolder for many parents
    private async Task<ICollection<string>> GenerateSubFolders<T>(FolderNamesEnum name, ICollection<T> collection, ICollection<string> parentIds){
        ICollection<string> itemSubFolderIds = [];

        if(parentIds.Count > 1 && name != FolderNamesEnum.Id){
            throw new InvalidOperationException();
        }

        int count = name == FolderNamesEnum.Id ? collection.Count : parentIds.Count;

        Func<int, string> chosenNamingMethod = NamingMethod(name);

        for(int i = 0; i < count; i++){
            string folderName = chosenNamingMethod(i);
            string itemFolderId = await _repository.CreateFolder(folderName, parentIds.)
        }   
    }

    private async Task ManyForOne<T>(FolderNamesEnum name, ICollection<T> collection, string parentId) where T : BaseDto{
        ICollection<string> folderIds = [];

        Func<int, string> naming = NamingMethod(name);

        for(int i = 0; i < collection.Count; i++){
            string folderName = naming(i);
            string folderId = await _repository.CreateFolder(folderName, parentId);
            //upload file to that folder;
            
            folderIds.Add(folderId);
        }

    }

    private async Task OneForMany<T>(FolderNamesEnum name, ICollection<T> collection, ICollection<string> parents) where T : BaseDto{

    }


    private Func<int, string> NamingMethod(FolderNamesEnum name){
        switch(name){
            case FolderNamesEnum.Id:
                return index => (index + 1).ToString();
            default:
                return _ => FolderNames.Names[name];
        }
    }
    private async Task<GoogleDriveFolderDto> GenerateSubFolders(string parentFolderId, int itemCount){
        List<string> itemFolderIds = [];
        List<string> imageIds = [];

        for(int i = 0; i < itemCount; i++){
            string id = await _repository.CreateFolder((i + 1).ToString(), parentFolderId);

            string imageFolderId = await _repository.CreateFolder("Resimler", id);

            imageIds.Add(imageFolderId);
            itemFolderIds.Add(id);
        }

        return new (itemFolderIds, imageIds);
    }

    private string GenerateFolderName(FolderNamesEnum name, int count){
        string countName = count <= 0 ? "(Yok)" : $"({count} Tane)";
        return $"{FolderNames.Names[name]} {countName}";
    }
    private int GetCount<T>(ICollection<T>? paths){
        return paths is null ? 0 : paths.Count;
    }

    /*

    private async Task 

    */
}
