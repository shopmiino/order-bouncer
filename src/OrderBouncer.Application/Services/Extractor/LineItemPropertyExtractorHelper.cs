using System;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Extractors;

namespace OrderBouncer.Application.Services.Extractor;

public class LineItemPropertyExtractorHelper : ILineItemPropertyExtractorHelper
{
    private readonly ILogger<LineItemPropertyExtractorHelper> _logger;

    public LineItemPropertyExtractorHelper(ILogger<LineItemPropertyExtractorHelper> logger){
        _logger = logger;
    }

    public IEnumerable<NoteAttribute>? GetContains(NoteAttribute[] props, string condition)
    {
        Func<NoteAttribute, bool> predicate = p => p.Name.Contains(condition);

        if(!props.Any(predicate)) {
            _logger.LogWarning("No element matching this condition!");
            return null;
        }

        IEnumerable<NoteAttribute> notes = props.Where(predicate);
        return notes;
    }

    public string? GetSingleCondition(NoteAttribute[] props, string conditionName)
    {
        Func<NoteAttribute, bool> predicate = p => p.Name == conditionName;
        
        if(!props.Any(predicate)) {
            _logger.LogWarning("No element matching this condition!");
            return null;
        }

        return props.SingleOrDefault(predicate)?.Value;
    }
}
