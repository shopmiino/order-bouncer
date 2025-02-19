using System;

namespace OrderBouncer.Application.Options;

public class ExtractorSettings
{
    public string NoteString {get; set;} = string.Empty;
    public string ImageString {get; set;} = string.Empty;
    public NoteCondition[] NoteConditions {get; set;} = [];
}

public class NoteCondition{
    public string Condition {get; set;} = string.Empty;
    public string Value {get; set;} = string.Empty;
}
