namespace OrderBouncer.Domain.DTOs.Base;

public record class FigureDto : BaseDto
{
    public ICollection<AccessoryDto>? Accessories;
    public FigureDto(){}
    public FigureDto(ICollection<AccessoryDto> accessoryDtos, ICollection<string>? imagePaths, string? note) : base(imagePaths, note){
        Accessories = accessoryDtos;
    }
}
