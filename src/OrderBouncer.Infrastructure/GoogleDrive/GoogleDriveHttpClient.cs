using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.GoogleDrive;

namespace OrderBouncer.Infrastructure.GoogleDrive;

public class GoogleDriveHttpClient : IGoogleDriveHttpClient
{
    public Task Upload(DriveUploadDto uploadDto)
    {
        throw new NotImplementedException();
    }
}
