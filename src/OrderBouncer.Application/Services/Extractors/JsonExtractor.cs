using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Extractors;

namespace OrderBouncer.Application.Services.Extractors;

public class JsonExtractor : IJsonExtractor
{
    public Task<JsonNode> Extract(string json)
    {
        throw new NotImplementedException();
    }
}
