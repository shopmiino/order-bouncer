namespace OrderBouncer.Domain.DTOs.Base;

public record class FigureDto
{
    public ICollection<string>? ImagePaths;
    public ICollection<AccessoryDto>? Accessories;
    public string? Note;

    public FigureDto(ICollection<string>? imagePaths, ICollection<AccessoryDto> accessoryDtos, string? note){
        ImagePaths = imagePaths;
        Accessories = accessoryDtos;
        Note = note;
    }
}
