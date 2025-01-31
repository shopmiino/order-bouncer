using OrderBouncer.GoogleSheets.Constants;

namespace OrderBouncer.GoogleSheets.DTOs;

public record class FlattenRowDto
{
    public RowTypeEnum RowType {get;}
    public DateTime Date {get;}
    public string OrderCode {get;}
    public bool HasAccessory {get;}
    public bool HasPet {get;}
    public bool HasKeychain {get;}
    public bool HasFigure {get;}

    public FlattenRowDto(string orderCode, DateTime date, RowTypeEnum rowType = RowTypeEnum.Figure, bool hasAccessory = false, bool hasPet = false, bool hasKeychain = false, bool hasFigure = false){
        OrderCode = orderCode;
        Date = date;
        RowType = rowType;
        HasAccessory = hasAccessory;
        HasPet = hasPet;
        HasKeychain = hasKeychain;
        HasFigure = hasFigure;

        if(rowType == RowTypeEnum.Keychain && !hasKeychain){
            throw new InvalidDataException($"{nameof(hasKeychain)} can not be false if the row type is {RowTypeEnum.Keychain.ToString()}");
        }
    }

}
