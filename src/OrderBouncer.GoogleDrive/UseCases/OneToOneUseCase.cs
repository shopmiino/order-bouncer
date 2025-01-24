using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Interfaces.UseCases;

namespace OrderBouncer.GoogleDrive.UseCases;

public class OneToOneUseCase<T> : IOneToOneUseCase<T> where T : BaseDto
{
    public Task ExecuteAsync()
    {
        throw new NotImplementedException();
    }
}
