using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;

namespace OrderBouncer.GoogleDrive.Interfaces.UseCases;

public interface IManyToManyUseCase<T> where T : BaseDto
{
    public Task<List<string>> ExecuteAsync(FolderNamesEnum name, List<T> collection, List<string> parents, CreationModes mode = CreationModes.Folder);
}
