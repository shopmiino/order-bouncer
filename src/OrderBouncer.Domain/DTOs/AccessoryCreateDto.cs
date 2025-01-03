using OrderBouncer.Domain.Entities;

namespace OrderBouncer.Domain.DTOs;

public record class AccessoryCreateDto
{
    public ICollection<ImageEntity>? Images {get; protected set;}
    public NoteEntity? Note {get; protected set;}

    public AccessoryCreateDto(ICollection<ImageEntity>? images, NoteEntity? note){
        Images = images;
        Note = note;
    }
}
