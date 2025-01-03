using System;

namespace OrderBouncer.Application.Interfaces.Mappings;

public interface IJsonMapping<T> where T : class
{
    public T? Map(string json);
    public T[]? MapMany(string json);
}
