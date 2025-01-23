using System;

namespace OrderBouncer.GoogleDrive.Interfaces.UseCases;

public interface IUseCase<TDto>
{
    public Task ExecuteAsync(TDto dto);
}
