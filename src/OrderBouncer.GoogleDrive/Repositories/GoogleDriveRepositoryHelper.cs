using System;
using Microsoft.Extensions.Configuration;
using OrderBouncer.GoogleDrive.Interfaces;

namespace OrderBouncer.GoogleDrive.Repositories;

public class GoogleDriveRepositoryHelper : IGoogleDriveRepositoryHelper
{
    private readonly IConfiguration _configuration;

    public GoogleDriveRepositoryHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<List<string>> GetParentId(string? id = null)
    {
        bool noParent = string.IsNullOrEmpty(id);

        if (!noParent)
        {
            return [id];
        }

        string? baseId = _configuration["Settings:Google:Drive:BaseFolderId"];

        if (baseId is null)
        {
            throw new ArgumentNullException("Id can not readable from AppSettings, it is null");
        }

        return [baseId];
    }
}
