using OrderBouncer.Domain.Entities;

namespace OrderBouncer.Domain.DTOs;

public record class ProductCreateDto
{
    public List<AccessoryEntity>? Accessories {get; protected set;}
    public List<PetEntity>? Pets {get; protected set;}
    public List<FigureEntity>? Figures {get; protected set;}

    public ProductCreateDto(List<AccessoryEntity>? accessories = null, List<PetEntity>? pets = null, List<FigureEntity>? figures = null){
        Accessories = accessories;
        Pets = pets;
        Figures = figures;
    }
}
