using System;
using System.Runtime.CompilerServices;
using OrderBouncer.Domain.DTOs.Base;

[assembly: InternalsVisibleTo("OrderBouncer.GoogleDrive.Tests")]

namespace OrderBouncer.GoogleDrive.Constants;
internal static class FolderNames
{
    internal static readonly Dictionary<FolderNamesEnum, string> Names = new(){
        {FolderNamesEnum.Accessory, "Aksesuar"},
        {FolderNamesEnum.Keychain, "Anahtarlık"},
        {FolderNamesEnum.Pet, "Evcil Hayvan"},
        {FolderNamesEnum.Figure, "Figür"},
        {FolderNamesEnum.Id, "ID"},
        {FolderNamesEnum.Images, "Fotoğraf"},
        {FolderNamesEnum.Product, "Ürün"}
    };
    internal static readonly Dictionary<Type, FolderNamesEnum> TypeNames = new(){
        {typeof(PetDto), FolderNamesEnum.Pet},
        {typeof(AccessoryDto), FolderNamesEnum.Accessory},
        {typeof(KeychainDto), FolderNamesEnum.Keychain},
        {typeof(FigureDto), FolderNamesEnum.Figure},
        {typeof(ProductDto), FolderNamesEnum.Product},
    };
}
