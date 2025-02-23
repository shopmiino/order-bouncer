using System;

namespace OrderBouncer.Domain.Interfaces.Factories;

public interface IFactory<TIn, TOut>
{
    public TOut Create(TIn? dto);
}
