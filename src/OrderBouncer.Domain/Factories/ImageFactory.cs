using System;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Domain.Factories;

public class ImageFactory : IEntityFactory<ImageCreateDto, ImageEntity>
{
    public ImageEntity Create(ImageCreateDto? dto)
    {
        throw new NotImplementedException();
    }
}
