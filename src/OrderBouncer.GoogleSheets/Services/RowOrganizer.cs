using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.Interfaces.Helpers;
using OrderBouncer.GoogleSheets.Interfaces.Services;
using OrderBouncer.GoogleSheets.Models;

namespace OrderBouncer.GoogleSheets.Services;

public class RowOrganizer : IRowOrganizerService
{
    private readonly IRowOrganizerHelper _helper;

    public RowOrganizer(IRowOrganizerHelper helper)
    {
        _helper = helper;
    }
    public Stack<RowElements> Organize(OrderDto dto, CancellationToken cancellationToken)
    {
        Stack<RowElements> rowElements = [];

        var kvps = _helper.GetElementCounts(dto);
        var highestKvp = _helper.GetHighestCountElement(kvps);

        if (highestKvp is null) return null;


        for (int i = 0; i < highestKvp.Value.Value; i++)
        {
            RowElements atLeastHasOne = _helper.GetElementsHasAtLeastOne(kvps);
            _helper.RemoveOneFromEach(kvps);

            rowElements.Push(atLeastHasOne);
        }


        return rowElements;
    }

}
