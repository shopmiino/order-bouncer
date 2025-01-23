using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;
using OrderBouncer.GoogleDrive.DTOs.UseCases;
using OrderBouncer.GoogleDrive.Interfaces;
using OrderBouncer.GoogleDrive.Interfaces.Helpers;
using OrderBouncer.GoogleDrive.Interfaces.UseCases;

namespace OrderBouncer.GoogleDrive.UseCases;

/// <summary>
/// Many subfolders for one parent folder
/// </summary>
/// <typeparam name="T"></typeparam>
public class ManyToOneUseCase<T> : IManyToOneUseCase<T> where T : BaseDto
{
    private readonly INamingHelperService _nameService;
    private readonly IGoogleDriveRepository _repository;
    public ManyToOneUseCase(INamingHelperService nameService, IGoogleDriveRepository repository){
        _nameService = nameService;
        _repository = repository;
    }
    public async Task ExecuteAsync(FolderNamesEnum name, ICollection<T> collection, string parentId)
    {
        //ICollection<string> folderIds = [];

        Func<int, string> namingMethod = _nameService.NamingMethod(name);

        int i = 0;
        foreach (T item in collection)
        {
            string folderName = namingMethod(i);
            string folderId = await _repository.CreateFolder(folderName, parentId);
            
            if(item.ImagePaths is not null)
                await _repository.BatchUploadFile(item.ImagePaths, folderId);

            //folderIds.Add(folderId);

            i++;
        }
    }
}
