using System;
using OrderBouncer.GoogleSheets.Constants;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Interfaces;

namespace OrderBouncer.GoogleSheets.Services;

public class RowFillerService : IRowFillerService
{
    private readonly IRowFillerHelperService _helper;

    public RowFillerService(IRowFillerHelperService helper)
    {
        _helper = helper;
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
