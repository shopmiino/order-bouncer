namespace OrderBouncer.GoogleSheets.DTOs;

public record class OrderRowDto
{
    public CellDto Date { get; }
    public CellDto OrderCode { get; }
    public CellDto SkinColor { get; }
    public CellDto HairColor { get; }
    public CellDto Gender { get; }
    public CellDto KeychainType { get; }
    public CellDto HeadModel { get; }
    public CellDto BodyModel { get; }
    public CellDto HeadPrint { get; }
    public CellDto BodyPrint { get; }
    public CellDto PrintReceived { get; }
    public CellDto Sticker { get; }
    public CellDto ExtraNotes { get; }
    public CellDto Urgent { get; }
    public CellDto Accessory { get; }
    public CellDto AccessoryPrint { get; }
    public CellDto Pet { get; }
    public CellDto PetPrint { get; }
    public CellDto Keychain { get; }
    public CellDto KeychainPrint { get; }
    public CellDto LatestShipmentDate { get; }
    public CellDto ShipmentStatus { get; }

    public OrderRowDto(CellDto date, CellDto orderCode, CellDto skinColor, CellDto hairColor, CellDto gender, CellDto keychainType, CellDto headModel, CellDto bodyModel, CellDto headPrint, CellDto bodyPrint, CellDto printReceived, CellDto sticker, CellDto extraNotes, CellDto urgent, CellDto accessory, CellDto accessoryPrint, CellDto pet, CellDto petPrint, CellDto keychain, CellDto keychainPrint, CellDto latestShipmentDate, CellDto shipmentStatus){
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
    }
}
