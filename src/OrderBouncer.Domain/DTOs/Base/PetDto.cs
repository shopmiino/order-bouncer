namespace OrderBouncer.Domain.DTOs.Base;

public record class PetDto : BaseDto
{
    public PetDto(){}
    public PetDto(List<string>? imagePaths = null, string? note = null) : base(imagePaths, note){
    }
}
