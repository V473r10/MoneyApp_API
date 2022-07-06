using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using RestAPI.Methods;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        [HttpGet]
        [Route("Users")]
        public IActionResult Login(string Username, string Password)
        {
            return base.Ok(Users.Login(Username, Password));

        }
        [HttpPost]
        [Route("Users")]
        public IActionResult Signup(string Username, string Email, string Password)
        {
            return base.Ok(Users.Signup(Username, Email, Password));

        }

        [HttpPut]
        [Route("Users")]
        public IActionResult UpdateUser(string Username, string FirstName, string LastName, string Email, string Phone, string Country, string Estate)
        {

            return base.Ok(Users.UpdateUser(Username, FirstName, LastName, Email, Phone, Country, Estate));

        }

        [HttpPut]
        [Route("Users/Pass")]
        public IActionResult ChangePass(string Username, string Password)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("Users")]
        public IActionResult DeleteUser(string Username, string Password)
        {
            return Ok();
        }

        [HttpGet]
        [Route("Users/Email")]
        public IActionResult GetEmail(string Username)
        {
            return base.Json(Users.GetEmail(Username));
        }

        [HttpPost]
        [Route("UpPhoto")]
        public async Task<IActionResult> UpPhoto(IFormFile photo)
        {
            string fileName = photo.FileName.Replace(" ", "");
            var Path = "C:\\Users\\Facundo\\Documents\\Dev\\" + fileName;

            using var stream = System.IO.File.Create(Path);
            await photo.CopyToAsync(stream);

            return Ok(Path);
        }
    }
}
