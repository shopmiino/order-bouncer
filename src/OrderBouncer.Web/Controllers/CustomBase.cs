using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderBouncer.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBase : ControllerBase
    {
    }
}
