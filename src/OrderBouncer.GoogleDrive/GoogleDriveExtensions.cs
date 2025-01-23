using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;

namespace OrderBouncer.GoogleDrive;

public static class GoogleDriveExtensions
{
    public static string GetMimeType(string filePath){
        var extension = Path.GetExtension(filePath)?.ToLower();
        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".pdf" => "application/pdf",
            ".txt" => "text/plain",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xls" => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".zip" => "application/zip",
            _ => "application/octet-stream",
        };
    }

    public static bool IsFileCreation(CreationModes mode){
        return mode == CreationModes.File;
    }
    public static bool IsFolderCreation(CreationModes mode){
        return mode == CreationModes.Folder;
    }
    public static bool IsFolderAndFileCreation(CreationModes mode){
        return mode == CreationModes.FolderAndFile;
    }
    public static bool HasManyProducts(ICollection<ProductDto> collection){
        return collection.Count > 1;
    }
}
