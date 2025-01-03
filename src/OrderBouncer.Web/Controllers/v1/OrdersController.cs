using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Domain.Models;

namespace OrderBouncer.Web.Controllers.v1
{
    public class OrdersController : CustomBase
    {

        [HttpGet]
        public async Task<IActionResult> GetOrders(GetOrdersDto orders){
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Created(string orderJsonModel){
            //Order created
            //JsonNode ile bu stingden verileri extract et
            //Add to database and upload to google drive
            return Ok(orderJsonModel);
        }
    }
}
