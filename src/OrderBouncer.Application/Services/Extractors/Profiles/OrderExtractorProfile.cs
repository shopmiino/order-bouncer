using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Extractors;

namespace OrderBouncer.Application.Services.Extractors.Profiles;

public class OrderExtractorProfile : IJsonExtractorProfile
{
    public Task<JsonNode?> GetProfilePart(string json)
    {
        throw new NotImplementedException();
    }
}
