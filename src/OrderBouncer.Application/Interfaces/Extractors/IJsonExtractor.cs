using System;
using System.Text.Json.Nodes;

namespace OrderBouncer.Application.Interfaces.Extractors;

public interface IJsonExtractor
{
    public JsonNode Extract(string json);
}
