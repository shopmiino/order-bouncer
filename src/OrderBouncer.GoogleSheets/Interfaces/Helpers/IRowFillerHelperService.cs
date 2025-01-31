using System;
using OrderBouncer.GoogleSheets.Constants;

namespace OrderBouncer.GoogleSheets.Interfaces.Helpers;

public interface IRowFillerHelperService
{
    public ColorsEnum HasCondition(bool condition);
}
