using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;
using OrderBouncer.GoogleDrive.Interfaces;
using OrderBouncer.GoogleDrive.Interfaces.Helpers;
using OrderBouncer.GoogleDrive.Interfaces.UseCases;

namespace OrderBouncer.GoogleDrive.UseCases;

public class ManyToManyUseCase<T> : IManyToManyUseCase<T> where T : BaseDto
{
    private readonly INamingHelperService _nameService;
    private readonly IGoogleDriveRepository _repository;
    public ManyToManyUseCase(INamingHelperService nameService, IGoogleDriveRepository repository){
        _nameService = nameService;
        _repository = repository;
    }
    public async Task<ICollection<string>> ExecuteAsync(FolderNamesEnum name, ICollection<T> collection, IList<string> parents, CreationModes mode = CreationModes.Folder)
    {
        if(parents.Count == 0 || parents.Count != collection.Count || collection.Count == 0) return [];
        
        ICollection<string> folderIds = [];

        Func<int, string> namingMethod = _nameService.NamingMethod(name);
         
        string changedParentId = string.Empty;

        int i = 0;
        foreach (T item in collection)
        {
            string folderName = namingMethod(i);

            string parentIdX = parents[i];
            string folderId = string.Empty;

            if(!GoogleDriveExtensions.IsFileCreation(mode))
                folderId = await _repository.CreateFolder(folderName, parentIdX);
            else 
                folderId = parentIdX;

            if(item.ImagePaths is not null && !GoogleDriveExtensions.IsFolderCreation(mode)){
                await _repository.BatchUploadFile(
                    item.ImagePaths, 
                    GoogleDriveExtensions.IsFolderAndFileCreation(mode) ? folderId : parentIdX,
                    item.Note
                    );

                if(item.Note is not null && item.Note != string.Empty) {
                    await _repository.UploadNote(item.Note, GoogleDriveExtensions.IsFolderAndFileCreation(mode) ? folderId : parentIdX);
                }
            }

            folderIds.Add(changedParentId);

            i++;
        }
        return folderIds;
    }
}
