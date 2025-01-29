
using OrderBouncer.GoogleSheets.Constants;

namespace OrderBouncer.GoogleSheets.Entities;

public class OrderRow
{
    public Cell Diagram { get; private set; }
    public Cell Date { get; private set; }
    public Cell OrderCode { get; private set; }
    public Cell SkinColor { get; private set; }
    public Cell HairColor { get; private set; }
    public Cell Gender { get; private set; }
    public Cell KeychainType { get; private set; }
    public Cell HeadModel { get; private set; }
    public Cell BodyModel { get; private set; }
    public Cell HeadPrint { get; private set; }
    public Cell BodyPrint { get; private set; }
    public Cell PrintReceived { get; private set; }
    public Cell Sticker { get; private set; }
    public Cell ExtraNotes { get; private set; }
    public Cell Urgent { get; private set; }
    public Cell Accessory { get; private set; }
    public Cell AccessoryPrint { get; private set; }
    public Cell Pet { get; private set; }
    public Cell PetPrint { get; private set; }
    public Cell Keychain { get; private set; }
    public Cell KeychainPrint { get; private set; }
    public Cell LatestShipmentDate { get; private set; }
    public Cell ShipmentStatus { get; private set; }

    public OrderRow(Cell diagram, Cell date, Cell orderCode, Cell skinColor, Cell hairColor, Cell gender, Cell keychainType, Cell headModel, Cell bodyModel, Cell headPrint, Cell bodyPrint, Cell printReceived, Cell sticker, Cell extraNotes, Cell urgent, Cell accessory, Cell accessoryPrint, Cell pet, Cell petPrint, Cell keychain, Cell keychainPrint, Cell latestShipmentDate, Cell shipmentStatus)
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

    public OrderRow SetAccessory(Cell cell)
    {
        CheckIsSpecialType(cell);

        Accessory = cell;
        return this;
    }

    public OrderRow SetPet(Cell cell)
    {
        CheckIsSpecialType(cell);

        Pet = cell;
        return this;
    }

    public OrderRow SetKeychain(Cell cell)
    {
        CheckIsSpecialType(cell);

        Keychain = cell;
        return this;
    }

    public OrderRow SetDiagram(Cell cell)
    {
        if (cell.DiagramType is null || cell.CellType != CellTypesEnum.Diagram)
        {
            throw new InvalidOperationException("Can not set Diagram Cell with non-Diagram Cell");
        }

        Diagram = cell;
        return this;
    }

    public OrderRow SetDate(Cell cell)
    {
        if (cell.CellType != CellTypesEnum.Date || cell.InnerText is null)
        {
            throw new InvalidOperationException("Can not set Date Cell with non-Date Cell");
        }

        Date = cell;
        return this;
    }

    public OrderRow SetOrderCode(Cell cell)
    {
        if (cell.CellType != CellTypesEnum.OrderCode || cell.InnerText is null)
        {
            throw new InvalidOperationException("Can not set OrderCode Cell with non-OrderCode Cell");
        }

        OrderCode = cell;
        return this;
    }

    public IReadOnlyList<Cell> GetCellsInOrder()
    {
        return [
                Diagram,
                Date,
                OrderCode,
                SkinColor,
                HairColor,
                Gender,
                KeychainType,
                HeadModel,
                BodyModel,
                HeadPrint,
                BodyPrint,
                PrintReceived,
                Sticker,
                ExtraNotes,
                Urgent,
                Accessory,
                AccessoryPrint,
                Pet,
                PetPrint,
                Keychain,
                KeychainPrint,
                LatestShipmentDate,
                ShipmentStatus
            ];
    }

    private void CheckIsSpecialType(Cell cell)
    {
        bool anyCellTypes = cell.CellType == CellTypesEnum.Date || cell.CellType == CellTypesEnum.Diagram || cell.CellType == CellTypesEnum.OrderCode;

        if (anyCellTypes)
        {
            throw new InvalidOperationException("The cell you are trying to attach is special type cell which is not valid for this operation");
        }
    }
}
