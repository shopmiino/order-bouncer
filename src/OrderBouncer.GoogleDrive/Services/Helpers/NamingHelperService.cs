using System;
using OrderBouncer.GoogleDrive.Constants;
using OrderBouncer.GoogleDrive.Interfaces.Helpers;

namespace OrderBouncer.GoogleDrive.Services.Helpers;

public class NamingHelperService : INamingHelperService
{
    public Func<int, string> NamingMethod(FolderNamesEnum name)
    {
        switch (name)
        {
            case FolderNamesEnum.Id:
                return index => (index + 1).ToString();
            default:
                return _ => FolderNames.Names[name];
        }
    }

    public string GenerateFolderName(FolderNamesEnum name, int count){
        string countName = count <= 0 ? "(Yok)" : $"({count} Tane)";
        return $"{FolderNames.Names[name]} {countName}";
    }
}
