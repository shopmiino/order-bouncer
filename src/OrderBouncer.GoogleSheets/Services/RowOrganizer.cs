using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Interfaces;
using OrderBouncer.GoogleSheets.Models;
using SharedKernel.Enums;

namespace OrderBouncer.GoogleSheets.Services;

public class RowOrganizer : IRowOrganizerService
{
    private readonly IRowOrganizerHelper _helper;
    
    private Stack<RowElements> _rowElements;
    public RowOrganizer(IRowOrganizerHelper helper){
        _helper = helper;
    }
    public Task<ICollection<FlattenRowDto>> Organize(OrderDto dto, CancellationToken cancellationToken)
    {
        var kvps = _helper.GetElementCounts(dto);
        var highestKvp = _helper.GetHighestCountElement(kvps);

        if(highestKvp is null) return null;

        for(int i = 0; i < highestKvp.Value.Value; i++){
            _rowElements.Push(_helper.GetElementsHasAtLeastOne(kvps));
        }

        
        
    }

}
