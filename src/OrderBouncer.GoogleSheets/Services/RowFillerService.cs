using System;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<RowFillerService> _logger;

    public RowFillerService(IRowFillerHelperService helper, ILogger<RowFillerService> logger)
    {
        _helper = helper;
        _logger = logger;
    }

    public FlattenRowDto FillFlattenWithElements(RowElements elements, FlattenRowDto flatten, string? name = null)
    {
        _logger.LogInformation("{className}'s {methodName} is started", nameof(RowFillerService), nameof(FillFlattenWithElements));
        _logger.LogInformation("Desired Name for this row is {0}", name);

        bool hasAccessory = elements.Elements.Contains(EntityTypeEnum.Accessory);
        bool hasKeychain = elements.Elements.Contains(EntityTypeEnum.Keychain);
        bool hasPet = elements.Elements.Contains(EntityTypeEnum.Pet);
        bool hasFigure = elements.Elements.Contains(EntityTypeEnum.Figure);

        _logger.LogDebug("HasAccessory: {0}, HasKeychain: {1}, HasPet: {2}, HasFigure: {3}", hasAccessory, hasKeychain, hasPet, hasFigure);

        RowTypeEnum rowType = hasKeychain ? hasFigure ? RowTypeEnum.Figure : RowTypeEnum.Keychain : RowTypeEnum.Default;
        
        _logger.LogDebug("Calculated RowType is {0}", nameof(rowType));

        return new FlattenRowDto(flatten.OrderCode, flatten.Date, name, rowType, hasAccessory, hasPet, hasKeychain, hasFigure);
    }

    public OrderRow FillWithFlatten(FlattenRowDto dto, OrderRow baseRow)
    {
        Cell accessoryCell = new("Accessory", _helper.HasCondition(dto.HasAccessory));
        Cell petCell = new("Pet", _helper.HasCondition(dto.HasPet));
        Cell keychainCell = new("Keychain", _helper.HasCondition(dto.HasKeychain));

        Cell dateCell = new("");
        dateCell.MarkAsDate(dto.Date);

        Cell latestShipmentDateCell = new("");
        latestShipmentDateCell.MarkAsDate(dto.Date.AddDays(10));

        Cell orderCodeCell = new("");
        orderCodeCell.MarkAsOrderCode(dto.OrderCode);

        Cell nameCell = new("");
        nameCell.MarkAsName(dto.Name);

        baseRow.SetAccessory(accessoryCell);
        baseRow.SetPet(petCell);
        baseRow.SetKeychain(keychainCell);
        baseRow.SetDate(dateCell);
        baseRow.SetOrderCode(orderCodeCell);
        baseRow.SetLatestShipmentDate(latestShipmentDateCell);
        //standard colors of cells

        baseRow.SetName(nameCell);
        
        baseRow.PrintReceived.SetStandardColor(ColorsEnum.Red);
        baseRow.Sticker.SetStandardColor(ColorsEnum.Red);

        if(dto.HasAccessory){
            baseRow.AccessoryPrint.SetStandardColor(ColorsEnum.Red);
        }

        if(dto.HasPet){
            baseRow.PetPrint.SetStandardColor(ColorsEnum.Red);
        }

        if(dto.HasKeychain && dto.HasFigure){                          //Order has keychain and figure
            baseRow.HeadModel.SetStandardColor(ColorsEnum.Red);
            baseRow.BodyModel.SetStandardColor(ColorsEnum.Red);
            baseRow.HeadPrint.SetStandardColor(ColorsEnum.Red);
            baseRow.BodyPrint.SetStandardColor(ColorsEnum.Red);

            baseRow.KeychainPrint.SetStandardColor(ColorsEnum.Red);

        } else if(!dto.HasKeychain && dto.HasFigure){                  //Order just has figure
            baseRow.HeadModel.SetStandardColor(ColorsEnum.Red);
            baseRow.BodyModel.SetStandardColor(ColorsEnum.Red);
            baseRow.HeadPrint.SetStandardColor(ColorsEnum.Red);
            baseRow.BodyPrint.SetStandardColor(ColorsEnum.Red);

        } else if(dto.HasKeychain && !dto.HasFigure){                  //Order just has keychain
            baseRow.HeadModel.SetStandardColor(ColorsEnum.Red);
            baseRow.HeadPrint.SetStandardColor(ColorsEnum.Red);

            baseRow.KeychainPrint.SetStandardColor(ColorsEnum.Red);
        }

        baseRow.ShipmentStatus.SetStandardColor(ColorsEnum.Red);

        //some fucked up logic, god bless future myself when I see that
        ColorsEnum keychainTypeColor = dto.HasKeychain ? dto.HasFigure ? ColorsEnum.Blue : ColorsEnum.Green : ColorsEnum.White;
        baseRow.KeychainType.SetStandardColor(keychainTypeColor);

        return baseRow;
    }
}
