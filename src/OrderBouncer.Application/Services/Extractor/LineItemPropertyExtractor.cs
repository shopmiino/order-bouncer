using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Application.Options;

namespace OrderBouncer.Application.Services.Extractor;

public class LineItemPropertyExtractor : ILineItemPropertyExtractor
{
    private readonly ExtractorSettings _settings;
    private readonly ILineItemPropertyExtractorHelper _helper;
    private readonly ILogger<LineItemPropertyExtractor> _logger;

    public LineItemPropertyExtractor(IOptions<ExtractorSettings> options, ILineItemPropertyExtractorHelper helper, ILogger<LineItemPropertyExtractor> logger){
        _settings = options.Value;
        _helper = helper;
        _logger = logger;
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

    public NoteAttribute[]? GetNameNotes(NoteAttribute[] properties)
    {
        return GetNotes(properties, "Name");
    }

    private NoteAttribute[]? GetNotes(NoteAttribute[] props, string conditionName){
        string? condition = _helper.GetSingleCondition(_settings.NoteConditions, conditionName);
        if(condition is null) return null;
        _logger.LogInformation("Condition retrieved with ConditionName: {0} and ConditionValue: {1}", conditionName, condition);

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
                imagePropertyGroup.Add([]);
            }

            imagePropertyGroup[currentElement].Append(new() { Name = parts.Last(), Value = image.Value });
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
