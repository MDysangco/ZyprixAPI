using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zyprix.Data.Interfaces;
using Zyprix.Models;

namespace ZypryxAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationRepository _configurationRepository;

        public ConfigurationController(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Insert([FromBody] Configuration configuration)
        {
            try
            {
                int rowId = await _configurationRepository.InsertConfiguration(configuration);
                if(rowId <= 0)
                {
                    return StatusCode(500, "An error occurred while inserting configuration.");
                }

                return Ok(rowId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting configuration: {ex.Message}");
                return StatusCode(500, "An error occurred while inserting configuration.");
            }
        }

    }
}
