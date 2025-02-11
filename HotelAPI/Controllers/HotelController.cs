using HotelAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
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
                // Log the exception (logging mechanism can be added here)
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
