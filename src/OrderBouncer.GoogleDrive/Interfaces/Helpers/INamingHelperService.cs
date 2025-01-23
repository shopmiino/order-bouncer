using System;
using OrderBouncer.GoogleDrive.Constants;

namespace OrderBouncer.GoogleDrive.Interfaces.Helpers;

public interface INamingHelperService
{
    public Func<int, string> NamingMethod(FolderNamesEnum name);
    public string GenerateFolderName(FolderNamesEnum name, int count);
}
