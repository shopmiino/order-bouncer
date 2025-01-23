using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;

namespace OrderBouncer.GoogleDrive.Interfaces.UseCases;

public interface IManyToManyUseCase<T> where T : BaseDto
{
    public Task<ICollection<string>> ExecuteAsync(FolderNamesEnum name, ICollection<T> collection, IList<string> parents, CreationModes mode = CreationModes.Folder);
}
