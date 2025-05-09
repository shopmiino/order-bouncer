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

    private const string IMAGE_NAME_SPLITTER = "-";
    private const int IMAGE_POSITION_PLACE = 1;

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

    public List<NoteAttribute[]>? GroupImages(NoteAttribute[] properties)
    {
        _logger.LogInformation("GroupImages is starting");

        IEnumerable<NoteAttribute> imageProperties = properties.Where(p => p.Name.StartsWith(_settings.ImageString));
        List<List<NoteAttribute>> imagePropertyGroup = [];

        int currentElement = -1;
        foreach (NoteAttribute image in imageProperties)
        {
            string[] parts = image.Name.Split(IMAGE_NAME_SPLITTER);
            _logger.LogDebug("Parts splitted into {0} pieces with {1}", parts.Count(), IMAGE_NAME_SPLITTER);

            int position = Convert.ToInt32(parts[IMAGE_POSITION_PLACE]); //0, 1, 2, 3, 4, ...
            _logger.LogDebug("Looking into {0}. place for position", IMAGE_POSITION_PLACE);
            _logger.LogDebug("Extracted position of current image is {0}", position);
            
            if (position == 0)
            {
                currentElement++;
                imagePropertyGroup.Add([]);

                _logger.LogDebug("Position is 0, new ImageProperty is adding and currentElement count increasing (now the currentElement is {0} and imageProperyGroup's count is {1})", currentElement, imagePropertyGroup.Count());
            }

            string name = parts.Last();
            _logger.LogDebug("Adding new NoteAttribute(Name: {0}, Value: {1}) to the imagePropertyGroup's {2}. element", name, image.Value, currentElement);
            
            NoteAttribute noteAttribute = new() { Name = name, Value = image.Value };
            imagePropertyGroup[currentElement].Add(noteAttribute);
        }

        return imagePropertyGroup.Select(p => p.ToArray()).ToList();
    }

    public List<NoteAttribute[]>? GroupNotes(NoteAttribute[] properties)
    {
        _logger.LogInformation("GroupNotes is starting");

        IEnumerable<NoteAttribute> noteProperties = properties.Where(p => p.Name.Contains(_settings.NoteString));
        int conditionCount = _settings.NoteConditions.Count();

        List<NoteAttribute[]> notePropertyGroup = [];
        foreach(var condition in _settings.NoteConditions){
            Func<NoteAttribute, bool> predicate = p => p.Name.Contains(condition.Value); 
            _logger.LogDebug("Looking for conditionName: {0} in NoteAttribute names", condition.Value);

            bool hasElement = noteProperties.Any(predicate);
            if(!hasElement) {
                _logger.LogDebug("No element found for this condition, continuing");
                continue;
            }

            IEnumerable<NoteAttribute> notes = noteProperties.Where(predicate);

            _logger.LogDebug("Found {0} matching notes for this condition, adding to the notePropertyGroup", notes.Count());
            notePropertyGroup.Add([.. notes]);
        }
        
        return notePropertyGroup;
    }
}
