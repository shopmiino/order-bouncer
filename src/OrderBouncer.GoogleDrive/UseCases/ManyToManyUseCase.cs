using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.DTOs.UseCases;
using OrderBouncer.GoogleDrive.Interfaces.UseCases;

namespace OrderBouncer.GoogleDrive.UseCases;

public class ManyToManyUseCase<T> : IManyToManyUseCase<T> where T : BaseDto
{
    public Task ExecuteAsync()
    {
        throw new NotImplementedException();
    }
}
