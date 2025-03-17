using System;
using OrderBouncer.Application.DTOs;

namespace OrderBouncer.Application.Interfaces.Normalizers;

public interface ILineItemsNormalizer
{
    public LineItem[] Normalize(LineItem[] lineItems);
}
