using Microsoft.AspNetCore.Mvc;

using RestAPI.Methods;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementsController : Controller
    {
        [Route("movement")]
        [HttpPost]
        public IActionResult Insert( int UserId, string Wallet, string Type, int Value )
        {
            return Ok(Movements.Insert(UserId, Wallet, Type, Value));
        }
                
    }
}
