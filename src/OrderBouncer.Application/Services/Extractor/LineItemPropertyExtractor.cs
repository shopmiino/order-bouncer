using System;
using Microsoft.Extensions.Options;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Application.Options;

namespace OrderBouncer.Application.Services.Extractor;

public class LineItemPropertyExtractor : ILineItemPropertyExtractor
{
    private readonly ExtractorSettings _settings;
    private readonly ILineItemPropertyExtractorHelper _helper;

    public LineItemPropertyExtractor(IOptions<ExtractorSettings> options, ILineItemPropertyExtractorHelper helper){
        _settings = options.Value;
        _helper = helper;
    }

    public NoteAttribute[]? GetAccessoryNotes(NoteAttribute[] properties)
    {
        return GetNotes(properties, "Accessory");
    }

    public NoteAttribute[]? GetFigureNotes(NoteAttribute[] properties)
    {
        return GetNotes(properties, "Figure");
    }

    public NoteAttribute[]? GetKeychainNotes(NoteAttribute[] properties)
    {
        return GetNotes(properties, "Keychain");
    }

    public NoteAttribute[]? GetPetNotes(NoteAttribute[] properties)
    {
        return GetNotes(properties, "Pet");
    }

    private NoteAttribute[]? GetNotes(NoteAttribute[] props, string conditionName){
        string? condition = _helper.GetSingleCondition(props, conditionName);
        if(condition is null) return null;

        var notes = _helper.GetContains(props, condition);

        if(notes is null) return null;

        return [.. notes];
    }

    public IList<NoteAttribute[]>? GroupImages(NoteAttribute[] properties)
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

    public IList<NoteAttribute[]>? GroupNotes(NoteAttribute[] properties)
    {
        IEnumerable<NoteAttribute> noteProperties = properties.Where(p => p.Name.Contains(_settings.NoteString));
        int conditionCount = _settings.NoteConditions.Count();

        List<NoteAttribute[]> notePropertyGroup = [];
        foreach(var condition in _settings.NoteConditions){
            Func<NoteAttribute, bool> predicate = p => p.Name.Contains(condition.Value); 

            bool hasElement = noteProperties.Any(predicate);
            if(!hasElement) continue;

            IEnumerable<NoteAttribute> notes = noteProperties.Where(predicate);

            notePropertyGroup.Add([.. notes]);
        }
        
        return notePropertyGroup;
    }
}
