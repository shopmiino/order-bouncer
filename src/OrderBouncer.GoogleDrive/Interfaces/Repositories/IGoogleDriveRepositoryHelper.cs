using System;

namespace OrderBouncer.GoogleDrive.Interfaces;

public interface IGoogleDriveRepositoryHelper
{
    public Task<List<string>> GetParentId(string? id = null);
}
