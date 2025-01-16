using System;

namespace OrderBouncer.GoogleDrive.Constants;

internal static class FolderNames
{
    internal static readonly Dictionary<FolderNamesEnum, string> Names = new(){
        {FolderNamesEnum.Accessory, "Aksesuar"},
        {FolderNamesEnum.Keychain, "Anahtarlık"},
        {FolderNamesEnum.Pet, "Evcil Hayvan"},
        {FolderNamesEnum.Figure, "Figür"},
        {FolderNamesEnum.Id, "ID"},
    };
}
