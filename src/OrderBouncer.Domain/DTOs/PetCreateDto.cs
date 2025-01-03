using OrderBouncer.Domain.Entities;

namespace OrderBouncer.Domain.DTOs;

public record class PetCreateDto
{
    public ICollection<ImageEntity>? Images {get; protected set;}
    public NoteEntity? Note {get; protected set;}

    public PetCreateDto(ICollection<ImageEntity>? images = null, NoteEntity? note = null){
        Images = images;
        Note = note;
    }
}
