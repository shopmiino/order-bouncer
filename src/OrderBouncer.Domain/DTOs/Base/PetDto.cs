namespace OrderBouncer.Domain.DTOs.Base;

public record class PetDto
{
    public ICollection<string>? ImagePaths;
    public string? Note;
    public PetDto(ICollection<string> imagePaths, string? note = null){
        ImagePaths = imagePaths;
        Note = note;
    }
}
