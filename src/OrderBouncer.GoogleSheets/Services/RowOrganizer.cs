using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Interfaces;
using SharedKernel.Enums;

namespace OrderBouncer.GoogleSheets.Services;

public class RowOrganizer : IRowOrganizerService
{
    private readonly IRowOrganizerHelper _helper;
    public RowOrganizer(IRowOrganizerHelper helper){
        _helper = helper;
    }
    public Task<ICollection<FlattenRowDto>> Organize(OrderDto dto, CancellationToken cancellationToken)
    {
        var kvp = _helper.GetElementCounts(dto);
        
    }

}
