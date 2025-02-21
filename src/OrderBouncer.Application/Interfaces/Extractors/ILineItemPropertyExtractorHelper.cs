using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Options;

namespace OrderBouncer.Application.Interfaces.Extractors;

public interface ILineItemPropertyExtractorHelper
{
    public string? GetSingleCondition(NoteCondition[] props, string conditionName);
    public IEnumerable<NoteAttribute>? GetContains(NoteAttribute[] props, string condition);
}
