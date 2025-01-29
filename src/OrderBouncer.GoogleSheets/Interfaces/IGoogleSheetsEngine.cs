using System;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.GoogleSheets.Interfaces;

public interface IGoogleSheetsEngine
{
    public Task UploadOrder(OrderDto dto, CancellationToken cancellationToken);
}
