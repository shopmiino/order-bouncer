using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;
using OrderBouncer.GoogleDrive.Interfaces;
using OrderBouncer.GoogleDrive.Interfaces.Helpers;
using OrderBouncer.GoogleDrive.Interfaces.UseCases;

namespace OrderBouncer.GoogleDrive.Services.Helpers;

public class ArchitectorHelperService : IArchitectorHelperService
{
    private readonly IGoogleDriveRepository _repository;
    private readonly INamingHelperService _namingHelper;
    private readonly IOneToManyUseCase<BaseDto> _oneToManyUseCase; 
    private readonly IManyToOneUseCase<BaseDto> _manyToOneUseCase;
    private readonly IManyToManyUseCase<BaseDto> _manyToManyUseCase;

    public ArchitectorHelperService(IGoogleDriveRepository repository, INamingHelperService namingHelper, IOneToManyUseCase<BaseDto> oneToManyUseCase, IManyToOneUseCase<BaseDto> manyToOneUseCase, IManyToManyUseCase<BaseDto> manyToManyUseCase)
    {  
        _repository = repository;
        _namingHelper = namingHelper;
        _oneToManyUseCase = oneToManyUseCase;
        _manyToOneUseCase = manyToOneUseCase;
        _manyToManyUseCase = manyToManyUseCase;
    }
    public Func<ProductDto, ICollection<BaseDto>?> CollectionInitializer(FolderNamesEnum type)
    {
        switch (type)
        {
            case FolderNamesEnum.Accessory:
                return prod => prod.Accessories?.Cast<BaseDto>().ToList();
            case FolderNamesEnum.Pet:
                return prod => prod.Pets?.Cast<BaseDto>().ToList();
            case FolderNamesEnum.Figure:
                return prod => prod.Figures?.Cast<BaseDto>().ToList();
            case FolderNamesEnum.Keychain:
                return prod => prod.Keychains?.Cast<BaseDto>().ToList();
            default:
                return _ => throw new ArgumentException("Error while setting collection for products");
        }
    }

    public async Task Generate<T>(ICollection<T>? collection, FolderNamesEnum type, string parentId) where T : ProductDto
    {
        if(collection is null) return;
        if(collection.Count <= 0) return;
        
        Func<ProductDto, ICollection<BaseDto>?> coll = CollectionInitializer(type);
        int productCount = GetCount(collection);

        foreach(T product in collection){
            ICollection<BaseDto>? tempColl = coll(product);

            if (tempColl is null) continue;
            
            string parentFolderId = await GenerateGeneric(GetCount(tempColl), type, parentId);

            if(productCount > 1){
                parentFolderId = await GenerateGeneric(productCount, FolderNamesEnum.Product, parentId);
            }

            ICollection<string> parents = await _manyToOneUseCase.ExecuteAsync(FolderNamesEnum.Id, tempColl, parentFolderId); // 1, 2, 3, 4 ...  
            ICollection<string> ppp = await _manyToManyUseCase.ExecuteAsync(FolderNamesEnum.Images, tempColl, (IList<string>)parents, CreationModes.FolderAndFile);
        }
    }

    public async Task<string> GenerateGeneric(int count, FolderNamesEnum name, string? parentId = null)
    {
        if(count < 0) throw new ArgumentException("Count can not be less than zero");
        if(name == FolderNamesEnum.Product) throw new ArgumentException("Can not create folder for Product");

        string folderName = _namingHelper.GenerateFolderName(name, count);  //e.g. Evcil Hayvanlar (2 Tane)
        string entityFolderId = await _repository.CreateFolder(folderName, parentId);

        return entityFolderId;
    }

    public int GetCount<T>(ICollection<T>? paths)
    {
        return paths is null ? 0 : paths.Count;
    }
}
