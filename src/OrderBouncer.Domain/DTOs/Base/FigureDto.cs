namespace OrderBouncer.Domain.DTOs.Base;

public record class FigureDto : BaseDto
{
    public string? Name {get; init;}
    public List<AccessoryDto>? Accessories {get; init;}
    public FigureDto(){}
    public FigureDto(List<string>? imagePaths, List<AccessoryDto>? accessoryDtos = null, string? note = null, string? name = null) : base(imagePaths, note){
        Accessories = accessoryDtos;
        Name = name;
    }
}
