using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderBouncer.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomBase : ControllerBase
    {
    }
}
