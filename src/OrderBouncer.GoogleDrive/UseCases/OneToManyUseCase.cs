using System;
using OrderBouncer.GoogleDrive.DTOs.UseCases;
using OrderBouncer.GoogleDrive.Interfaces.UseCases;

namespace OrderBouncer.GoogleDrive.UseCases;

public class OneToManyUseCase : IUseCase<OneToManyRequestDto>
{
    public Task ExecuteAsync(OneToManyRequestDto dto)
    {
        throw new NotImplementedException();
    }
}
