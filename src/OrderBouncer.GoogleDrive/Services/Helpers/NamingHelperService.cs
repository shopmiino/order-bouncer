using System;
using OrderBouncer.GoogleDrive.Constants;
using OrderBouncer.GoogleDrive.Interfaces.Helpers;

namespace OrderBouncer.GoogleDrive.Services.Helpers;

public class NamingHelperService : INamingHelperService
{
    public Func<int, string?, string> NamingMethod(FolderNamesEnum nameType)
    {
        switch (nameType)
        {
            case FolderNamesEnum.Id:
                return (index, name) => (index + 1).ToString();
            case FolderNamesEnum.IdWName:
                return (index, name) => $"{index + 1} - {name}";
            default:
                return (_,_) => FolderNames.Names[nameType];
        }
    }

    public string GenerateFolderName(FolderNamesEnum name, int count){
        if(name == FolderNamesEnum.Product) throw new ArgumentException("Invalid Enum type for generating folder names");
        
        string countName = count <= 0 ? "(Yok)" : $"({count} Tane)";
        return $"{FolderNames.Names[name]} {countName}";
    }
}
