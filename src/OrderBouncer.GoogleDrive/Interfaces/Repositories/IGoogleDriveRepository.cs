using System;

namespace OrderBouncer.GoogleDrive.Interfaces;

public interface IGoogleDriveRepository
{
    public Task UploadFile(string filePath, string? folderId = null);
    public Task BatchUploadFile(ICollection<string> filePaths, string? folderId = null);
    public Task<string> CreateFolder (string folderName, string? parentFolderId = null);
}
