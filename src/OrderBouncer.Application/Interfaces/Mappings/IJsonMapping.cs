using System;
using System.Text.Json.Nodes;

namespace OrderBouncer.Application.Interfaces.Mappings;

public interface IJsonMapping<T> where T : class
{
    public Task<T?> Map(JsonNode json);
    public Task<ICollection<T>?> MapMany(JsonNode json);
}
