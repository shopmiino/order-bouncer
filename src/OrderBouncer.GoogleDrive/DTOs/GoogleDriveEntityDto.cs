using OrderBouncer.Domain.Entities;

namespace OrderBouncer.GoogleDrive.DTOs;

public record class GoogleDriveEntityDto
{
    public ICollection<ImageEntity> Images;
    public string Note;

    public GoogleDriveEntityDto(ICollection<ImageEntity> images, string note){
        Images = images;
        Note = note;
    }
}
