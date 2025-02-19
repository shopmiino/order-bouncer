using System;
using OrderBouncer.Application.DTOs;

namespace OrderBouncer.Application.Interfaces.Extractors;

public interface ILineItemPropertyExtractorHelper
{
    public string? GetSingleCondition(NoteAttribute[] props, string conditionName);
    public IEnumerable<NoteAttribute>? GetContains(NoteAttribute[] props, string condition);
}
