using HotelAPI.Models;
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


        /// <summary>
        /// Returns a hotel from the database
        /// </summary>
        /// <param name="name">a hotel name as parameter</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Hotel/{name}
        ///     
        /// </remarks>
        /// <returns>A hotel record</returns>
        /// <response code="200">Returns a hotel record</response>
        /// <response code="404">No hotel found</response>
        /// <response code="500">Internal Server error</response>
        #region Annotation
        [ProducesResponseType(typeof(Booking), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        #endregion
        [HttpGet("{name}")]
        public async Task<IActionResult> GetHotelByName(string name)
        {
            try
            {
                var hotel = await _hotelService.GetHotelByNameAsync(name);
                if (hotel == null)
                {
                    return NotFound("No hotel found");
                }
                return Ok(hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the hotel.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
