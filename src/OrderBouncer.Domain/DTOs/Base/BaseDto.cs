namespace OrderBouncer.Domain.DTOs.Base;

public record class BaseDto
{
    public ICollection<string>? ImagePaths;
    public string? Note;

    public BaseDto(){}
    public BaseDto(ICollection<string>? imagePaths = null, string? note = null){
        ImagePaths = imagePaths;
        Note = note;
    }
}
