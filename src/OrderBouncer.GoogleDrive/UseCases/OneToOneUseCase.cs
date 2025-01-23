using System;
using OrderBouncer.GoogleDrive.DTOs.UseCases;
using OrderBouncer.GoogleDrive.Interfaces.UseCases;

namespace OrderBouncer.GoogleDrive.UseCases;

public class OneToOneUseCase : IUseCase<OneToOneRequestDto>
{
    public Task ExecuteAsync(OneToOneRequestDto dto)
    {
        throw new NotImplementedException();
    }
}
