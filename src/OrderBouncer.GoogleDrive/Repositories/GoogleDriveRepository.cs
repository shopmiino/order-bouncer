using System;
using Google.Apis.Drive.v3;
using Microsoft.Extensions.Configuration;
using OrderBouncer.GoogleDrive.Interfaces;
using File = Google.Apis.Drive.v3.Data.File;
using Microsoft.Extensions.Logging;
using OrderBouncer.GoogleDrive.Constants;

namespace OrderBouncer.GoogleDrive.Repositories;

public class GoogleDriveRepository : IGoogleDriveRepository
{
    private readonly DriveService _drive;
    private readonly IGoogleDriveRepositoryHelper _helper;
    private readonly IConfiguration _configuration;
    private readonly ILogger<GoogleDriveRepository> _logger;

    public GoogleDriveRepository(DriveService drive, IConfiguration configuration, ILogger<GoogleDriveRepository> logger, IGoogleDriveRepositoryHelper helper)
    {
        _drive = drive;
        _configuration = configuration;
        _logger = logger;
        _helper = helper;
    }

    public async Task<string> CreateFolder(string folderName, string? parentFolderId = null)
    {
        try{
            File metaData = new()
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder"
            };
            
            metaData.Parents = await _helper.GetParentId(parentFolderId);

            FilesResource.CreateRequest request = _drive.Files.Create(metaData);

            request.Fields = "id";

            File folder = await request.ExecuteAsync();

            return folder.Id;
        } catch (Exception ex){
            throw;
        }
    }

    public async Task UploadFile(string filePath, string? folderId = null)
    {
        try
        {
            File metaData = new()
            {
                Name = Path.GetFileName(filePath)
            };

            
            metaData.Parents = await _helper.GetParentId(folderId);

            using (Stream stream = new FileStream(filePath, FileMode.Open))
            {
                FilesResource.CreateMediaUpload request = _drive.Files.Create(metaData, stream, GoogleDriveExtensions.GetMimeType(filePath));

                request.Fields = "id, name";
                var response = await request.UploadAsync();

                if (response.Status == Google.Apis.Upload.UploadStatus.Completed)
                {
                    _logger.LogInformation("Upload is successfull!");
                }
                else
                {
                    throw response.Exception;
                }

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

}
