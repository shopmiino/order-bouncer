namespace OrderBouncer.GoogleDrive.DTOs;

public record class GoogleDriveFolderDto
{
    public ICollection<string> ItemFolderIds;
    public ICollection<string>? ItemImageFolderIds;

    public GoogleDriveFolderDto(ICollection<string> itemFolderIds, ICollection<string>? itemImageFolderIds = null){
        ItemFolderIds = itemFolderIds;
        ItemImageFolderIds = itemImageFolderIds;
    }
}
