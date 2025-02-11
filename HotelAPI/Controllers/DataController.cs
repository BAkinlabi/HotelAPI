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
            await _dataService.SeedDataAsync();
            return Ok("Database seeded successfully.");
        }

        [HttpPost("reset")]
        public async Task<IActionResult> ResetData()
        {
            await _dataService.ResetDataAsync();
            return Ok("Database reset successfully.");
        }
    }
}
