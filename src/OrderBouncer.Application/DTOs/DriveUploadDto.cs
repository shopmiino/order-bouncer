using System;

namespace OrderBouncer.Application.DTOs;

public record class DriveUploadDto
{
    public string FileName {get; protected set;}
    public Stream Content {get; protected set;}
    public string MimeType {get; protected set;}
    public string Url {get; protected set;}
    public string Folder {get; protected set;}
    public string Directory {get; protected set;}

    public DriveUploadDto(string fileName, Stream content, string mimeType, string url, string folder, string directory){
        FileName = fileName;
        Content = content;
        MimeType = mimeType;
        Url = url;
        Folder = folder;
        Directory = directory;
    }
}
