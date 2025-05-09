using OrderBouncer.Domain.Entities;

namespace OrderBouncer.Domain.DTOs;

public record class FigureCreateDto
{
    public List<AccessoryEntity>? Accessories {get; protected set;}
    public List<ImageEntity>? Images {get; protected set;}
    public NoteEntity? Note {get; protected set;}

    public FigureCreateDto(List<AccessoryEntity>? accessories = null, List<ImageEntity>? images = null, NoteEntity? note = null){
        Accessories = accessories;
        Images = images;
        Note = note;
    }
}
