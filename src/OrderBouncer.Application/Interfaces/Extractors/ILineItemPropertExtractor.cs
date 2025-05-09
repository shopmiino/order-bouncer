using System;
using OrderBouncer.Application.DTOs;

namespace OrderBouncer.Application.Interfaces.Extractors;

public interface ILineItemPropertyExtractor
{
    public List<NoteAttribute[]>? GroupImages(NoteAttribute[] properties);
    public List<NoteAttribute[]>? GroupNotes(NoteAttribute[] properties);
    
    public NoteAttribute[]? GetFigureNotes(NoteAttribute[] properties);
    public NoteAttribute[]? GetAccessoryNotes(NoteAttribute[] properties);
    public NoteAttribute[]? GetPetNotes(NoteAttribute[] properties);
    public NoteAttribute[]? GetKeychainNotes(NoteAttribute[] properties);
    public NoteAttribute[]? GetNameNotes(NoteAttribute[] properties);

}
