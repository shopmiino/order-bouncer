using System;
using Microsoft.Extensions.Options;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Application.Options;

namespace OrderBouncer.Application.Services.Extractor;

public class LineItemPropertyExtractor : ILineItemPropertyExtractor
{
    private readonly ExtractorSettings _settings;

    public LineItemPropertyExtractor(IOptions<ExtractorSettings> options){
        _settings = options.Value;
    }

    public NoteAttribute[] GetAccessoryNotes(NoteAttribute[] properties)
    {
        throw new NotImplementedException();
    }

    public NoteAttribute[] GetFigureNotes(NoteAttribute[] properties)
    {
        throw new NotImplementedException();
    }

    public NoteAttribute[] GetPetNotes(NoteAttribute[] properties)
    {
        throw new NotImplementedException();
    }

    public IList<NoteAttribute[]> GroupImages(NoteAttribute[] properties)
    {
        IEnumerable<NoteAttribute> imageProperties = properties.Where(p => p.Name.StartsWith(_settings.ImageString));
        List<NoteAttribute[]> imagePropertyGroup = [];

        int currentElement = -1;
        foreach (NoteAttribute image in imageProperties)
        {
            string[] parts = image.Name.Split("-");
            int position = Convert.ToInt32(parts[1]); //0, 1, 2, 3, 4, ...

            if (position == 0)
            {
                currentElement++;
                imagePropertyGroup[currentElement] = [];
            }

            imagePropertyGroup[currentElement][position] = new() { Name = parts.Last(), Value = image.Value };
        }

        return imagePropertyGroup;
    }

    public IList<NoteAttribute[]> GroupNotes(NoteAttribute[] properties)
    {
        IEnumerable<NoteAttribute> noteProperties = properties.Where(p => p.Name.Contains(_settings.NoteString));
        int conditionCount = _settings.NoteConditions.Count();

        List<NoteAttribute[]> notePropertyGroup = [];

        
    }
}
