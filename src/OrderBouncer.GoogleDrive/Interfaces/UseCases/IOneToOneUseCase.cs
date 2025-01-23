using System;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.GoogleDrive.Interfaces.UseCases;

public interface IOneToOneUseCase<T> where T : BaseDto
{
    public Task ExecuteAsync();
}
