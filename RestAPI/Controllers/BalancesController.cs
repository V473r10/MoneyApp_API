using Microsoft.AspNetCore.Mvc;

using RestAPI.Methods;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalancesController : Controller
    {

        [Route("balance")]
        [HttpGet]
        public IActionResult GetBalance(int UserId)
        {
            return Ok(Balances.GetBalance(UserId));
        }
    }
}
