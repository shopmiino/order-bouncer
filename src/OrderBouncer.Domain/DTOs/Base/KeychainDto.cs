namespace OrderBouncer.Domain.DTOs.Base;

public record class KeychainDto
{
    public ICollection<string>? ImagePaths;
    public string? Note;
    public KeychainDto(ICollection<string> imagePaths, string? note = null){
        ImagePaths = imagePaths;
        Note = note;
    }
}
