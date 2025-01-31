using System;
using OrderBouncer.GoogleSheets.Constants;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Models;
using SharedKernel.Enums;

namespace OrderBouncer.GoogleSheets.Tests.TestData;

internal static class DataGenerator
{
    public static OrderRow GenerateOrderRow()
    {
        return new(new("Diagram", cellType: CellTypesEnum.Diagram), new("Date", cellType: CellTypesEnum.Date), new("OrderCode"), new("SkinColor"), new("HairColor"), new("Gender"), new("KeychainType"), new("HeadModel"), new("BodyModel"), new("HeadPrint"), new("BodyPrint"), new("PrintReceived"), new("Sticker"), new("ExtraNotes"), new("Urgent"), new("Accessory"), new("AccessoryPrint"), new("Pet"), new("PetPrint"), new("Keychain"), new("KeychainPrint"), new("LatestShipmentDate"), new("ShipmentStatus"));
    }

    public static RowElements GenerateRowElements(){
        return new(){
            Count = 1,
            Elements = [EntityTypeEnum.Accessory, EntityTypeEnum.Figure, EntityTypeEnum.Keychain]
        };
    }
}
