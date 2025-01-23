namespace OrderBouncer.Domain.DTOs.Base;

public record class ProductDto
{
    public ICollection<FigureDto>? Figures;
    public ICollection<AccessoryDto>? Accessories;
    public ICollection<KeychainDto>? Keychains;
    public ICollection<PetDto>? Pets;

    public ProductDto(ICollection<FigureDto>? figures, ICollection<AccessoryDto>? accessories, ICollection<KeychainDto>? keychains, ICollection<PetDto>? pets){
        Figures = figures;
        Accessories = accessories;
        Keychains = keychains;
        Pets = pets;
    }
}
