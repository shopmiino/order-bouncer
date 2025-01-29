using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Models;

namespace OrderBouncer.GoogleSheets.Interfaces;

public interface IRowOrganizerService
{
    public Stack<RowElements> Organize(OrderDto dto, CancellationToken cancellationToken);
}
