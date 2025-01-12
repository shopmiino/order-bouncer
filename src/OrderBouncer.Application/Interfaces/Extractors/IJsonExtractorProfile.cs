using System;
using System.Text.Json.Nodes;

namespace OrderBouncer.Application.Interfaces.Extractors;

public interface IJsonExtractorProfile
{
    public Task<JsonNode?> GetProfilePart(string json);
}
