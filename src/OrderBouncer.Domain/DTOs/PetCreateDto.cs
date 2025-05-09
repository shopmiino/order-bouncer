using OrderBouncer.Domain.Entities;

namespace OrderBouncer.Domain.DTOs;

public record class PetCreateDto
{
    public List<ImageEntity>? Images {get; protected set;}
    public NoteEntity? Note {get; protected set;}

    public PetCreateDto(List<ImageEntity>? images = null, NoteEntity? note = null){
        Images = images;
        Note = note;
    }
}
