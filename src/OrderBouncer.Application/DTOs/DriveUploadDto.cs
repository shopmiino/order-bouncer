using System;

namespace OrderBouncer.Application.DTOs;

public class DriveUploadDto
{
    public required string FileName {get; set;}
    public required Stream Content {get; set;}
    public required string MimeType {get; set;}
    public required string Url {get; set;}
    public required string Folder {get; set;}
    public required string Directory {get; set;}
}
