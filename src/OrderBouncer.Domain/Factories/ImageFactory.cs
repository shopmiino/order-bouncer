using System;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Domain.Factories;

public class ImageFactory : IFactory<ImageCreateDto, ImageEntity>
{
    public ImageEntity Create(ImageCreateDto? dto)
    {
        throw new NotImplementedException();
    }
}
