using HotelAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;
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
                // Log the exception (logging mechanism can be added here)
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
                // Log the exception (logging mechanism can be added here)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
