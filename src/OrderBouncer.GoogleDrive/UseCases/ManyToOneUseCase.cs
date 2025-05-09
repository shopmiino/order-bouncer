using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;
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
    public async Task<List<string>> ExecuteAsync(FolderNamesEnum name, List<T> collection, string parentId, CreationModes mode = CreationModes.Folder)
    {
        List<string> folderIds = [];

        Func<int, string?, string> namingMethod = _nameService.NamingMethod(name);
         
        string changedParentId = string.Empty;

        if(GoogleDriveExtensions.IsFileCreation(mode)){
            changedParentId = parentId;
        }

        int i = 0;
        foreach (T item in collection)
        {
            string folderName = namingMethod(i, "Naming Work Ongoing");

            if(!GoogleDriveExtensions.IsFileCreation(mode))
                changedParentId = await _repository.CreateFolder(folderName, parentId);
            
            if(item.ImagePaths is not null && !GoogleDriveExtensions.IsFolderCreation(mode))
                await _repository.BatchUploadFile(item.ImagePaths, changedParentId);

            folderIds.Add(changedParentId);

            i++;
        }
        return folderIds;
    }
}
