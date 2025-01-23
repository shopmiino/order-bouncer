using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.GoogleDrive;
using OrderBouncer.Application.Interfaces.UseCases;
using OrderBouncer.Domain.Models;

namespace OrderBouncer.Web.Controllers.v1
{
    public class OrdersController : CustomBase
    {
        private readonly IOrderCreatedUseCase _orderCreated;
        public OrdersController(IOrderCreatedUseCase orderCreated){
            _orderCreated = orderCreated;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders(GetOrdersDto orders){
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Created([FromBody]JsonDocument orderJsonModel){
            //TODO: implement Cancellation Token 
            await _orderCreated.ExecuteAsync(orderJsonModel, new CancellationToken());
            //Order created
            //JsonNode ile bu stingden verileri extract et
            //Add to database and upload to google drive
            return Ok(orderJsonModel);
        }

    }
}
