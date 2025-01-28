using OrderBouncer.GoogleSheets.Entities;

namespace OrderBouncer.GoogleSheets.DTOs;

public record class OrderRowDto
{
    public Cell Diagram { get; }
    public Cell Date { get; }
    public Cell OrderCode { get; }
    public Cell SkinColor { get; }
    public Cell HairColor { get; }
    public Cell Gender { get; }
    public Cell KeychainType { get; }
    public Cell HeadModel { get; }
    public Cell BodyModel { get; }
    public Cell HeadPrint { get; }
    public Cell BodyPrint { get; }
    public Cell PrintReceived { get; }
    public Cell Sticker { get; }
    public Cell ExtraNotes { get; }
    public Cell Urgent { get; }
    public Cell Accessory { get; }
    public Cell AccessoryPrint { get; }
    public Cell Pet { get; }
    public Cell PetPrint { get; }
    public Cell Keychain { get; }
    public Cell KeychainPrint { get; }
    public Cell LatestShipmentDate { get; }
    public Cell ShipmentStatus { get; }

    public OrderRowDto(Cell diagram, Cell date, Cell orderCode, Cell skinColor, Cell hairColor, Cell gender, Cell keychainType, Cell headModel, Cell bodyModel, Cell headPrint, Cell bodyPrint, Cell printReceived, Cell sticker, Cell extraNotes, Cell urgent, Cell accessory, Cell accessoryPrint, Cell pet, Cell petPrint, Cell keychain, Cell keychainPrint, Cell latestShipmentDate, Cell shipmentStatus)
    {
        Date = date;
        OrderCode = orderCode;
        SkinColor = skinColor;
        HairColor = hairColor;
        Gender = gender;
        KeychainType = keychainType;
        HeadModel = headModel;
        BodyModel = bodyModel;
        HeadPrint = headPrint;
        BodyPrint = bodyPrint;
        PrintReceived = printReceived;
        Sticker = sticker;
        ExtraNotes = extraNotes;
        Urgent = urgent;
        Accessory = accessory;
        AccessoryPrint = accessoryPrint;
        Pet = pet;
        PetPrint = petPrint;
        Keychain = keychain;
        KeychainPrint = keychainPrint;
        LatestShipmentDate = latestShipmentDate;
        ShipmentStatus = shipmentStatus;
        Diagram = diagram;
    }
}
