using System;
using System.Text.Json.Nodes;

namespace OrderBouncer.Application.Interfaces.Extractors;

public interface IJsonExtractor
{
    public Task<JsonNode?> Extract<TProfile>(string json) where TProfile: class;
}
