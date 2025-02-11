using HotelAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly ILogger<HotelController> _logger;

        public HotelController(IHotelService hotelService, ILogger<HotelController> logger)
        {
            _hotelService = hotelService;
            _logger = logger;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetHotelByName(string name)
        {
            try
            {
                var hotel = await _hotelService.GetHotelByNameAsync(name);
                if (hotel == null)
                {
                    return NotFound();
                }
                return Ok(hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the hotel.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
