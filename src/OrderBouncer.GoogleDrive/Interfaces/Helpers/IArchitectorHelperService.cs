using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;

namespace OrderBouncer.GoogleDrive.Interfaces.Helpers;

public interface IArchitectorHelperService
{
    public Func<ProductDto, List<BaseDto>?> CollectionInitializer(FolderNamesEnum type);
    public Task Generate<T>(List<T>? collection, FolderNamesEnum type, string parentId) where T : ProductDto;
    public Task<string> GenerateGeneric(int count, FolderNamesEnum name, string? parentId = null);
    public int GetCount<T>(IList<T>? paths);
}
