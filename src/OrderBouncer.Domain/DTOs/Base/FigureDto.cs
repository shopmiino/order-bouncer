namespace OrderBouncer.Domain.DTOs.Base;

public record class FigureDto : BaseDto
{
    public string? Name {get; init;}
    public ICollection<AccessoryDto>? Accessories {get; init;}
    public FigureDto(){}
    public FigureDto(ICollection<AccessoryDto> accessoryDtos, ICollection<string>? imagePaths, string? note, string? name = null) : base(imagePaths, note){
        Accessories = accessoryDtos;
        Name = name;
    }
}
