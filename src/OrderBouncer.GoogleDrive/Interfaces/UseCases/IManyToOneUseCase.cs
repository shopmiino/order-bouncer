using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;

namespace OrderBouncer.GoogleDrive.Interfaces.UseCases;

public interface IManyToOneUseCase<T> where T : BaseDto
{
    public Task<ICollection<string>> ExecuteAsync(FolderNamesEnum name, ICollection<T> collection, string parentId, CreationModes mode = CreationModes.Folder);
}
