namespace OrderBouncer.Domain.DTOs.Base;

public record class MutableProductDto(
    ICollection<FigureDto>? Figures,
    ICollection<AccessoryDto>? Accessories,
    ICollection<KeychainDto>? Keychains,
    ICollection<PetDto>? Pets)
{

}
