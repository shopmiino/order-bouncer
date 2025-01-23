using OrderBouncer.Domain.Entities;
using OrderBouncer.GoogleDrive.Constants;

namespace OrderBouncer.GoogleDrive.DTOs;

public record class GoogleDriveEntityDto
{
    public ICollection<string>? ImagePaths;
    public string? Note;
    public FolderNamesEnum FolderName;
    public string? ParentFolderId;

    public GoogleDriveEntityDto(ICollection<string>? imagePaths, FolderNamesEnum folderNames,string? parentFolderId = null, string? note = null){
        ImagePaths = imagePaths;
        Note = note;
        FolderName = folderNames;
    }
}
