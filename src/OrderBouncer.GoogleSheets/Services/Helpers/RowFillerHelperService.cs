using System;
using OrderBouncer.GoogleSheets.Constants;
using OrderBouncer.GoogleSheets.Interfaces.Helpers;

namespace OrderBouncer.GoogleSheets.Services.Helpers;

public class RowFillerHelperService : IRowFillerHelperService
{
    public ColorsEnum HasCondition(bool condition)
    {
        return condition ? ColorsEnum.Yellow : ColorsEnum.White;
    }
}
