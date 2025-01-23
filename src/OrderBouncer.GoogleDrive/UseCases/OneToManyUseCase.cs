using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;
using OrderBouncer.GoogleDrive.Interfaces;
using OrderBouncer.GoogleDrive.Interfaces.Helpers;
using OrderBouncer.GoogleDrive.Interfaces.UseCases;

namespace OrderBouncer.GoogleDrive.UseCases;

/// <summary>
/// one subfolder for many parent folders
/// </summary>
/// <typeparam name="T"></typeparam>
public class OneToManyUseCase<T> : IOneToManyUseCase<T> where T : BaseDto
{
    private readonly INamingHelperService _nameService;
    private readonly IGoogleDriveRepository _repository;
    public OneToManyUseCase(INamingHelperService nameService, IGoogleDriveRepository repository){
        _nameService = nameService;
        _repository = repository;
    }
    public async Task<ICollection<string>> ExecuteAsync(FolderNamesEnum name, T dto, ICollection<string> parents, CreationModes mode = CreationModes.Folder)
    {
        ICollection<string> folderIds = [];

        Func<int, string> namingMethod = _nameService.NamingMethod(name);

        int i = 0;
        foreach (string parentId in parents)
        {
            string folderName = namingMethod(i);
            string folderId = string.Empty;

            if(!GoogleDriveExtensions.IsFileCreation(mode))
                folderId = await _repository.CreateFolder(folderName, parentId);
            else 
                folderId = parentId;
            
            if(dto.ImagePaths is not null && !GoogleDriveExtensions.IsFolderCreation(mode))
                await _repository.BatchUploadFile(
                    dto.ImagePaths,
                     GoogleDriveExtensions.IsFolderAndFileCreation(mode) ? folderId : parentId
                     );

            folderIds.Add(folderId);

            i++;
        }
        return folderIds;
    }
}
