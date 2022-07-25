using Microsoft.AspNetCore.Mvc;

using RestAPI.Methods;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    public class WalletsController : Controller
    {

        [HttpPost]
        [Route("wallet")]
        public IActionResult CreateWallet(int UserId, string Wallet, int Balance)
        {
            return Ok(Wallets.CreateWallet(UserId, Wallet, Balance));
        }
    }
}
