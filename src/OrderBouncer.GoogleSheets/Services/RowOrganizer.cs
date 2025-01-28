using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Interfaces;

namespace OrderBouncer.GoogleSheets.Services;

public class RowOrganizer : IRowOrganizerService
{
    public Task<ICollection<FlattenRowDto>> Organize(OrderDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
