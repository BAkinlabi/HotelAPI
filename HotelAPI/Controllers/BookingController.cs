using HotelAPI.Models;
using HotelAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IBookingService bookingService, ILogger<BookingController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequest request)
        {
            try
            {
                var booking = await _bookingService.CreateBookingAsync(request);
                return Ok(booking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the booking.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{referenceNumber}")]
        public async Task<IActionResult> GetBookingByReferenceNumber(string referenceNumber)
        {
            try
            {
                var booking = await _bookingService.GetBookingByReferenceNumberAsync(referenceNumber);
                if (booking == null)
                {
                    return NotFound();
                }
                return Ok(booking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the booking.");
                return BadRequest(ex.Message);
            }
        }
    }
}
