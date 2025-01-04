using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.GoogleDrive;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Application.Interfaces.UseCases;
using OrderBouncer.Domain.Aggregates;

namespace OrderBouncer.Application.UseCases;

public class OrderCreatedUseCase : IOrderCreatedUseCase
{
    private readonly IJsonMapping<Order> _orderMapping;
    private readonly IGoogleDriveHttpClient _googleDrive;
    public OrderCreatedUseCase(IJsonMapping<Order> orderMapping, IGoogleDriveHttpClient googleDrive){
        _orderMapping = orderMapping;
        _googleDrive = googleDrive;
    }
    public bool Create(string json)
    {
        ICollection<Order>? orders = _orderMapping.MapMany(json);
        //DriveUploadDto dto = new ();
        //_googleDrive.Upload();
        return false;
    }
}
