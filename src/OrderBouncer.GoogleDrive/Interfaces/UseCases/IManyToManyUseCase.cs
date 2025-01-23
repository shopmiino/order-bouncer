using System;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.GoogleDrive.Interfaces.UseCases;

public interface IManyToManyUseCase<T> where T : BaseDto
{
    public Task ExecuteAsync();
}
