using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Extractors;

namespace OrderBouncer.Application.Services.Extractors.Profiles;

public class OrderExtractorProfile : IJsonExtractorProfile
{
    public async Task<JsonNode?> GetProfilePart(JsonNode json)
    {
        JsonNode? order = json["order"];
        
        return order;
    }
}
