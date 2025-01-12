using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Extractors;

namespace OrderBouncer.Application.Services.Extractors.Profiles;

public class NoteExtractorProfile : IJsonExtractorProfile
{
    public Task<JsonNode?> GetProfilePart(JsonNode json)
    {
        throw new NotImplementedException();
    }
}
