namespace OrderBouncer.Domain.DTOs.Base;

public record class ProductDto
{
    public ICollection<FigureDto>? Figures {get; init;}
    public ICollection<AccessoryDto>? Accessories {get; init;}
    public ICollection<KeychainDto>? Keychains {get; init;}
    public ICollection<PetDto>? Pets {get; init;}

    public ProductDto(){}
    public ProductDto(ICollection<FigureDto>? figures, ICollection<AccessoryDto>? accessories, ICollection<KeychainDto>? keychains, ICollection<PetDto>? pets){
        Figures = figures;
        Accessories = accessories;
        Keychains = keychains;
        Pets = pets;
    }
}
