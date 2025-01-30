namespace OrderBouncer.Domain.DTOs.Base;

public record class BaseDto
{
    public ICollection<string>? ImagePaths {get; init;}
    public string? Note {get; init;}

    public BaseDto(){}
    public BaseDto(ICollection<string>? imagePaths = null, string? note = null){
        ImagePaths = imagePaths;
        Note = note;
    }
}
