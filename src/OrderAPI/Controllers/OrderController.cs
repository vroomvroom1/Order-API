using Microsoft.AspNetCore.Mvc;

namespace OrderAPI.Controllers
{
    [Route("api/v1/hello")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"this", "is", "hard", "coded"};
        }
    }
}