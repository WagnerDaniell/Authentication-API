using ApiAuth.Models;
using ApiAuth.Repository;
using ApiAuth.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuth.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public ActionResult<dynamic> Authenticate([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
            return NotFound(new {message = "User ou password invalid!"});

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }
    }
}