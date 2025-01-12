using System;

namespace OrderBouncer.Application.Interfaces.Mappings;

public interface IJsonMapping<T> where T : class
{
    public Task<T?> Map(string json);
    public Task<ICollection<T>?> MapMany(string json);
}
