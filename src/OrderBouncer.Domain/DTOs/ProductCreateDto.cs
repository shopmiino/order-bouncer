using OrderBouncer.Domain.Entities;

namespace OrderBouncer.Domain.DTOs;

public record class ProductCreateDto
{
    public ICollection<AccessoryEntity>? Accessories {get; protected set;}
    public ICollection<PetEntity>? Pets {get; protected set;}
    public ICollection<FigureEntity>? Figures {get; protected set;}

    public ProductCreateDto(ICollection<AccessoryEntity>? accessories = null, ICollection<PetEntity>? pets = null, ICollection<FigureEntity>? figures = null){
        Accessories = accessories;
        Pets = pets;
        Figures = figures;
    }
}
