namespace OrderBouncer.Domain.DTOs.Base;

public record class FigureDto : BaseDto
{
    public string? Name {get; init;}
    public ICollection<AccessoryDto>? Accessories {get; init;}
    public FigureDto(){}
    public FigureDto(ICollection<string>? imagePaths, ICollection<AccessoryDto>? accessoryDtos = null, string? note = null, string? name = null) : base(imagePaths, note){
        Accessories = accessoryDtos;
        Name = name;
    }
}
