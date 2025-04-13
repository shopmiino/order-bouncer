using System;
using OrderBouncer.GoogleSheets.Constants;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Interfaces.Helpers;

namespace OrderBouncer.GoogleSheets.Services.Helpers;

public class RowFillerHelperService : IRowFillerHelperService
{
    public ColorsEnum HasCondition(bool condition)
    {
        return condition ? ColorsEnum.Yellow : ColorsEnum.White;
    }

    public ColorsEnum StandaloneOrExtraColorCondition(FlattenRowDto flattenRowDto, bool condition){
        if(flattenRowDto.HasFigure && condition){
            return ColorsEnum.Blue;
        } else if (!flattenRowDto.HasFigure && condition) {
            return ColorsEnum.Green;
        }

        return ColorsEnum.White;
    }
}
