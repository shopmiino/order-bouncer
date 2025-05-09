using System;
using Google.Apis.Drive.v3;
using Microsoft.Extensions.Configuration;
using OrderBouncer.GoogleDrive.Interfaces;
using File = Google.Apis.Drive.v3.Data.File;
using Microsoft.Extensions.Logging;
using OrderBouncer.GoogleDrive.Constants;
using System.Text;

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

    //TODO This can be waaay further optimizible with buffers and real batching. It is a wrapper just for now
    public async Task BatchUploadFile(List<string> filePaths, string? folderId = null, string? note = null)
    {
        foreach (string filePath in filePaths)
        {
            await UploadFile(filePath, folderId, note);
        }
    }

    public async Task<string> CreateFolder(string folderName, string? parentFolderId = null)
    {
        try
        {
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
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task UploadFile(string filePath, string? folderId = null, string? note = null)
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
                    _logger.LogError(response.Exception, "An error ocurred while uploading IMAGE file (inner using)");
                }

            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error ocurred while uploading IMAGE file");
        }
    }

    public async Task UploadNote(string note, string? folderId = null)
    {
        try
        {
            File metaData = new()
            {
                Name = "Note"
            };

            metaData.Parents = await _helper.GetParentId(folderId);

            using Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(note));

            FilesResource.CreateMediaUpload request = _drive.Files.Create(metaData, stream, "text/plain");

            request.Fields = "id, name";
            var response = await request.UploadAsync();

            if (response.Status == Google.Apis.Upload.UploadStatus.Completed)
            {
                _logger.LogInformation("Upload is successfull!");
            }
            else
            {
                _logger.LogError(response.Exception, "An error ocurred while uploading NOTE file (inner using)");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error ocurred while uploading NOTE file");
        }
    }

}
