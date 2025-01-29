using System;
using OrderBouncer.GoogleSheets.Constants;
using OrderBouncer.GoogleSheets.Interfaces;

namespace OrderBouncer.GoogleSheets.Services;

public class RowFillerHelperService : IRowFillerHelperService
{
    public ColorsEnum HasCondition(bool condition)
    {
        return condition ? ColorsEnum.Yellow : ColorsEnum.White;
    }
}
