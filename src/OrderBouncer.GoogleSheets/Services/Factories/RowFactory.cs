using System;
using OrderBouncer.GoogleSheets.Constants;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Interfaces.Factories;
using OrderBouncer.GoogleSheets.Interfaces.Services;

namespace OrderBouncer.GoogleSheets.Services.Factories;

public class RowFactory : IRowFactory
{
    private readonly IRowFillerService _filler;

    public RowFactory(IRowFillerService filler)
    {
        _filler = filler;
    }

    private FlattenRowDto? _internalFlattenRow = null;
    private OrderRow? _internalRow = null;
    public OrderRow Build()
    {
        if (_internalFlattenRow is null)
        {
            throw new InvalidOperationException("Can not create an instance from null, internal flatten row data is null. Call From(flattenRow) before Build()");
        }

        if (_internalRow is null)
        {
            throw new InvalidOperationException("Can not create an instance from null, internal reference row is null. Probably you forgot to call Create() method in top of the chain");
        }

        return _filler.FillWithFlatten(_internalFlattenRow, _internalRow);
    }


    public IRowFactory From(FlattenRowDto flattenRowDto)
    {
        _internalFlattenRow = flattenRowDto;
        return this;
    }
    

    public IRowFactory Create()
    {
        _internalRow = new(new("Diagram"), new("Date", cellType: CellTypesEnum.Date), new("OrderCode"), new("SkinColor"), new("HairColor"), new("Gender"), new("KeychainType"), new("HeadModel"), new("BodyModel"), new("HeadPrint"), new("BodyPrint"), new("PrintReceived"), new("Sticker"), new("ExtraNotes"), new("Urgent"), new("Accessory"), new("AccessoryPrint"), new("Pet"), new("PetPrint"), new("Keychain"), new("KeychainPrint"), new("LatestShipmentDate"), new("ShipmentStatus"), new("Name"), new("PrintNotes"));

        return this;
    }
}
