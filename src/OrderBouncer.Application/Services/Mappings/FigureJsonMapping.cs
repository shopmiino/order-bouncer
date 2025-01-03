using System;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Application.Services.Mappings;

public class FigureJsonMapping
{
    private readonly IEntityFactory<FigureCreateDto, FigureEntity> _figureFactory;
    private readonly IJsonMapping<AccessoryEntity> _accessoryMapping;
    private readonly IJsonMapping<ImageEntity> _imageMapping;
    private readonly IJsonMapping<NoteEntity> _noteMapping;

    public FigureJsonMapping(IEntityFactory<FigureCreateDto, FigureEntity> figureFactory, IJsonMapping<AccessoryEntity> accessoryMapping, IJsonMapping<ImageEntity> imageMapping, IJsonMapping<NoteEntity> noteMapping){
        _figureFactory = figureFactory;
        _accessoryMapping = accessoryMapping;
        _imageMapping = imageMapping;
        _noteMapping = noteMapping;
    }
}
