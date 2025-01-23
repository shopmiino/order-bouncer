using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;
using OrderBouncer.GoogleDrive.DTOs.UseCases;
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
    public async Task ExecuteAsync(FolderNamesEnum name, T dto, ICollection<string> parents)
    {
        //ICollection<string> folderIds = [];

        Func<int, string> namingMethod = _nameService.NamingMethod(name);

        int i = 0;
        foreach (string parentId in parents)
        {
            string folderName = namingMethod(i);
            string folderId = await _repository.CreateFolder(folderName, parentId);
            
            if(dto.ImagePaths is not null)
                await _repository.BatchUploadFile(dto.ImagePaths, folderId);

            //folderIds.Add(folderId);

            i++;
        }
    }
}
