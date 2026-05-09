using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Zyprix.Models;

namespace ZypryxAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest data)
        {
            try
            {
                //Need to validate the user credentials against the database
                string userName = data.Email;
                string password = data.Password;

                string token = Utils.JwtFactory.CreateUserToken(_configuration, "user", 60);

                if (string.IsNullOrEmpty(token))
                {
                    return StatusCode(500, "An error occurred during login.");
                }

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
                return StatusCode(500, "An error occurred during login.");
            }
        }
    }
}
