using System;
using OrderBouncer.GoogleDrive.Constants;

namespace OrderBouncer.GoogleDrive.Interfaces.Helpers;

public interface INamingHelperService
{
    public Func<int, string?, string> NamingMethod(FolderNamesEnum nameType);
    public string GenerateFolderName(FolderNamesEnum name, int count);
}
