using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.DTOs.UseCases;
using OrderBouncer.GoogleDrive.Interfaces.UseCases;

namespace OrderBouncer.GoogleDrive.UseCases;

public class ManyToOneUseCase<T> : IUseCase<ManyToOneRequestDto<T>> where T : BaseDto
{
    public Task ExecuteAsync(ManyToOneRequestDto<T> dto)
    {
        throw new NotImplementedException();
    }
}
