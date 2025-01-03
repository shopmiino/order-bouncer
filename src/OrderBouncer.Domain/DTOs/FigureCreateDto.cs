using OrderBouncer.Domain.Entities;

namespace OrderBouncer.Domain.DTOs;

public record class FigureCreateDto
{
    public ICollection<AccessoryEntity>? Accessories {get; protected set;}
    public ICollection<ImageEntity>? Images {get; protected set;}
    public NoteEntity? Note {get; protected set;}

    public FigureCreateDto(ICollection<AccessoryEntity>? accessories = null, ICollection<ImageEntity>? images = null, NoteEntity? note = null){
        Accessories = accessories;
        Images = images;
        Note = note;
    }
}
