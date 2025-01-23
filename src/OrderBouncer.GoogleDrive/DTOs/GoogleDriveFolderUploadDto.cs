using OrderBouncer.GoogleDrive.Constants;

namespace OrderBouncer.GoogleDrive.DTOs;

public record class GoogleDriveFolderUploadDto
{
    public ICollection<GoogleDriveEntityDto> Items;
    public FolderNamesEnum Name;

    public GoogleDriveFolderUploadDto(ICollection<GoogleDriveEntityDto> items, FolderNamesEnum name){
        Items = items;
        Name = name;
    }
}
