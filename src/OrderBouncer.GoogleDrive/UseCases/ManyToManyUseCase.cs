using System;
using OrderBouncer.GoogleDrive.DTOs.UseCases;
using OrderBouncer.GoogleDrive.Interfaces.UseCases;

namespace OrderBouncer.GoogleDrive.UseCases;

public class ManyToManyUseCase : IUseCase<ManyToManyRequestDto>
{
    public Task ExecuteAsync(ManyToManyRequestDto dto)
    {
        throw new NotImplementedException();
    }
}
