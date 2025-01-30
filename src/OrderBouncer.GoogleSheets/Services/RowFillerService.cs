using System;
using OrderBouncer.GoogleSheets.Constants;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Interfaces.Helpers;
using OrderBouncer.GoogleSheets.Interfaces.Services;
using OrderBouncer.GoogleSheets.Models;
using SharedKernel.Enums;

namespace OrderBouncer.GoogleSheets.Services;

public class RowFillerService : IRowFillerService
{
    private readonly IRowFillerHelperService _helper;

    public RowFillerService(IRowFillerHelperService helper)
    {
        _helper = helper;
    }

    public FlattenRowDto FillFlattenWithElements(RowElements elements, FlattenRowDto flatten)
    {
        bool hasAccessory = elements.Elements.Contains(EntityTypeEnum.Accessory);
        bool hasKeychain = elements.Elements.Contains(EntityTypeEnum.Keychain);
        bool hasPet = elements.Elements.Contains(EntityTypeEnum.Pet);
        bool hasFigure = elements.Elements.Contains(EntityTypeEnum.Figure);

        RowTypeEnum rowType = hasFigure ? RowTypeEnum.Figure : hasKeychain ? RowTypeEnum.Keychain : RowTypeEnum.Default;
        
        return new FlattenRowDto(flatten.OrderCode, flatten.Date, rowType, hasAccessory, hasPet, hasKeychain);
    }

    public OrderRow FillWithFlatten(FlattenRowDto dto, OrderRow baseRow)
    {
        Cell accessoryCell = new("Accessory", _helper.HasCondition(dto.HasAccessory));
        Cell petCell = new("Pet", _helper.HasCondition(dto.HasPet));
        Cell keychainCell = new("Keychain", _helper.HasCondition(dto.HasKeychain));

        Cell dateCell = new("");
        dateCell.MarkAsDate(dto.Date);

        Cell orderCodeCell = new("");
        orderCodeCell.MarkAsOrderCode(dto.OrderCode);

        baseRow.SetAccessory(accessoryCell);
        baseRow.SetPet(petCell);
        baseRow.SetKeychain(keychainCell);
        baseRow.SetDate(dateCell);
        baseRow.SetOrderCode(orderCodeCell);

        return baseRow;
    }
}
