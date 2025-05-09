namespace OrderBouncer.Domain.DTOs.Base;

public record class MutableProductDto(
    List<FigureDto>? Figures,
    List<AccessoryDto>? Accessories,
    List<KeychainDto>? Keychains,
    List<PetDto>? Pets)
{

}
