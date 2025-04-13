using System;
using OrderBouncer.GoogleSheets.Constants;
using OrderBouncer.GoogleSheets.DTOs;

namespace OrderBouncer.GoogleSheets.Interfaces.Helpers;

public interface IRowFillerHelperService
{
    public ColorsEnum HasCondition(bool condition);
    public ColorsEnum StandaloneOrExtraColorCondition(FlattenRowDto flattenRowDto, bool condition);
}
