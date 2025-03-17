using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Normalizers;

namespace OrderBouncer.Application.Services.Normalizers;

public class LineItemsNormalizer : ILineItemsNormalizer
{
    public LineItem[] Normalize(LineItem[] lineItems)
    {
        List<LineItem> normalizedLineItems = []; 
        foreach(LineItem lineItem in lineItems){
            for(int i = 0; i < lineItem.Quantity; i++){
                normalizedLineItems.Add(lineItem);
            }
        }
        return [.. normalizedLineItems];
    }
}
