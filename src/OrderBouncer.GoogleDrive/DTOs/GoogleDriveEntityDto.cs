using OrderBouncer.Domain.Entities;
using OrderBouncer.GoogleDrive.Constants;

namespace OrderBouncer.GoogleDrive.DTOs;

public record class GoogleDriveEntityDto
{
    public ICollection<string>? ImagePaths;
    public string? Note;

    public GoogleDriveEntityDto(ICollection<string>? imagePaths, string? note = null){
        ImagePaths = imagePaths;
        Note = note;
    }
}
