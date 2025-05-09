namespace OrderBouncer.Domain.DTOs.Base;

public record class AccessoryDto : BaseDto
{
    public AccessoryDto(){}
    public AccessoryDto(List<string>? imagePaths = null, string? note = null) : base(imagePaths, note)
    {
    }
}