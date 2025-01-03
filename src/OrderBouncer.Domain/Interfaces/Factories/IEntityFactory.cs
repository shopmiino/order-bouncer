using System;

namespace OrderBouncer.Domain.Interfaces.Factories;

public interface IEntityFactory<TDto, T>
{
    public T Create(TDto? dto);
}
