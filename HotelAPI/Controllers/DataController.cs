using HotelAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly ILogger<DataController> _logger;

        public DataController(IDataService dataService, ILogger<DataController> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        [HttpPost("seed")]
        public async Task<IActionResult> SeedData()
        {
            try
            {
                await _dataService.SeedDataAsync();
                return Ok("Database seeded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("reset")]
        public async Task<IActionResult> ResetData()
        {
            try
            {
                await _dataService.ResetDataAsync();
                return Ok("Database reset successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
