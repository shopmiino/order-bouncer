using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderBouncer.Application.DTOs;

namespace OrderBouncer.Web.Controllers.v1
{
    public class OrdersController : CustomBase
    {

        [HttpGet]
        public async Task<IActionResult> GetOrders(GetOrdersDto orders){
            return Ok(orders);
        }
    }
}
