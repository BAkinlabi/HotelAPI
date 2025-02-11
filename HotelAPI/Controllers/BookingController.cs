using HotelAPI.Models;
using HotelAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
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
                // Log the exception (logging mechanism can be added here)
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
                // Log the exception (logging mechanism can be added here)
                return BadRequest(ex.Message);
            }
        }
    }
}
