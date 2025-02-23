using System;

namespace OrderBouncer.Application.Interfaces.Converters;

public interface IRequestConverterService<T,TResult>
{
    public Task<TResult> Convert(T input, Guid scopeId); 
}
