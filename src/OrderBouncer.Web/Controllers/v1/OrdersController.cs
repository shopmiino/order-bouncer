using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.UseCases;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.Domain.Models;
using Polly.CircuitBreaker;

namespace OrderBouncer.Web.Controllers.v1
{
    public class OrdersController : CustomBase
    {
        private readonly IOrderCreatedUseCase _orderCreated;
        private readonly ILogger<OrdersController> _logger;
        public OrdersController(IOrderCreatedUseCase orderCreated, ILogger<OrdersController> logger){
            _orderCreated = orderCreated;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreatedShopifyRequestDto requestDto){
            //Add conversion mechanism
            try{
                //if(requestDto.Order is null) throw new ArgumentNullException("Order is null");

                await _orderCreated.ExecuteAsync(requestDto, new CancellationToken());
            } catch (BrokenCircuitException){
                _logger.LogWarning("Circuit is OPEN! Returning 503 Service Unavailable.");
                return StatusCode(503, "Service unavailable due to repeated failures.");
            } catch(Exception ex){
                _logger.LogError("An error occurred\nmessage: {0}\nstack trace: {1}", ex.Message, ex.StackTrace);
                return StatusCode(500, "Internal Server Error.");
            }
            return Ok();
        }
    }
}
