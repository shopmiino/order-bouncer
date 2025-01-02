using System;
using OrderBouncer.Application.DTOs;

namespace OrderBouncer.Application.Interfaces.GoogleDrive;

public interface IGoogleDriveHttpClient
{
    public Task Upload(DriveUploadDto uploadDto);
}
