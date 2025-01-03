namespace OrderBouncer.Domain.DTOs;

public record class NoteCreateDto
{
    public string? Note {get; protected set;}

    public NoteCreateDto(string? note = null){
        Note = note;
    }
}
