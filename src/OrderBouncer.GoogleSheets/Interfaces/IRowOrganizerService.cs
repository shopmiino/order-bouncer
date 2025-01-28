using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.DTOs;

namespace OrderBouncer.GoogleSheets.Interfaces;

public interface IRowOrganizerService
{
    public Task<ICollection<FlattenRowDto>> Organize(OrderDto dto, CancellationToken cancellationToken);
}
