using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Extractors;

namespace OrderBouncer.Application.Services.Extractors.Profiles;

public class ProductExtractorProfile : IJsonExtractorProfile
{
    public async Task<JsonNode?> GetProfilePart(JsonNode json)
    {
        JsonNode? lineItems = json["line_items"];
        return lineItems;
    }
}
