using System;
using OrderBouncer.Application.DTOs;

namespace OrderBouncer.Application.Interfaces.Extractors;

public interface ILineItemPropertyExtractor
{
    public IList<NoteAttribute[]> GroupImages(NoteAttribute[] properties);
    public IList<NoteAttribute[]> GroupNotes(NoteAttribute[] properties);
    
    public NoteAttribute[] GetFigureNotes(NoteAttribute[] properties);
    public NoteAttribute[] GetAccessoryNotes(NoteAttribute[] properties);
    public NoteAttribute[] GetPetNotes(NoteAttribute[] properties);

}
