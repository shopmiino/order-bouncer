namespace OrderBouncer.Domain.DTOs.Base;

public record class KeychainDto : BaseDto
{
    public KeychainDto(){}
    public KeychainDto(ICollection<string>? imagePaths = null, string? note = null) : base (imagePaths, note){
    }
}
