namespace OrderBouncer.Domain.DTOs.Base;

public record class ProductDto
{
    public List<FigureDto>? Figures {get; init;}
    public List<AccessoryDto>? Accessories {get; init;}
    public List<KeychainDto>? Keychains {get; init;}
    public List<PetDto>? Pets {get; init;}

    public ProductDto(){}
    public ProductDto(List<FigureDto>? figures, List<AccessoryDto>? accessories, List<KeychainDto>? keychains, List<PetDto>? pets){
        Figures = figures;
        Accessories = accessories;
        Keychains = keychains;
        Pets = pets;
    }
}
