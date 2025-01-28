using System;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Interfaces;

namespace OrderBouncer.GoogleSheets.Services;

public class RowFactory : IRowFactory
{
    private readonly IRowConverterService _converter;

    public RowFactory(IRowConverterService converter){
        _converter = converter;
    }

    private FlattenRowDto? _internalFlattenRow = null;
    public OrderRowDto Build()
    {
        if(_internalFlattenRow is null) {
            throw new InvalidOperationException("Can not create an instance from null, internal flatten row data is null");
        }

        return _converter.ConvertFromFlatten(_internalFlattenRow);    
    }


    public IRowFactory From(FlattenRowDto flattenRowDto)
    {
        _internalFlattenRow = flattenRowDto;
        return this;
    }
}
