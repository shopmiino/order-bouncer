namespace OrderBouncer.Domain.DTOs.Base;

public record class AccessoryDto
{
    public ICollection<string>? ImagePaths;
    public string? Note;

    public AccessoryDto(ICollection<string> imagePaths, string? note = null){
        ImagePaths = imagePaths;
        Note = note;
    }
}