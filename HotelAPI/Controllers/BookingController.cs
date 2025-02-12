using HotelAPI.ModelDTOs;
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
        private readonly ILogger<BookingController> _logger;

        public BookingController(IBookingService bookingService, ILogger<BookingController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all a list of available room types that can be booked
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Booking/CheckRooms
        ///       {
        ///          "numberOfPeople": 1,
        ///          "checkInDate": "14/02/2025",
        ///          "checkOutDate": "16/02/2025"
        ///       }
        ///     
        /// </remarks>
        /// <returns>A list of available room type</returns>
        /// <response code="200">A list of available room type</response>
        /// <response code="400">Invalid data</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server error</response>
        #region Annotation
        [ProducesResponseType(typeof(RoomType), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        #endregion
        [HttpPost("CheckRooms")]
        public async Task<IActionResult> CheckRoomsAvailable(RoomsAvailableRequestDTO roomsAvailableRequest)
        {
            if (roomsAvailableRequest == null) { return BadRequest(); }
            DateOnly _checkIn, _checkOut;

            if (roomsAvailableRequest.NumberOfPeople <= 0)
            {
                return BadRequest("Invalid number of people");
            }

            if (!DateOnly.TryParse(roomsAvailableRequest.CheckInDate, out _checkIn))
            {
                return BadRequest("Bad input date");
            }

            if (!DateOnly.TryParse(roomsAvailableRequest.CheckOutDate, out _checkOut))
            {
                return BadRequest("Bad input date");
            }

            if (_checkIn >= _checkOut)
            {
                return BadRequest("Start date must be before End date");
            }

            try
            {
                var result = await _bookingService.CheckRoomAvailibilityAsync(roomsAvailableRequest.NumberOfPeople, _checkIn, _checkOut);

                if (result == null) return BadRequest("Error checking room availability");
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking for room availability.");
                return StatusCode(500, $"Internal server error.");
            }
        }

        /// <summary>
        /// Creates a hotel booking
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/booking/CreateBooking
        ///       {
        ///         "roomTypeId": 1,
        ///         "startDate": "26/02/2025",
        ///         "endDate": "28/02/2025",
        ///         "numberOfGuests": 2
        ///        }
        ///     
        /// </remarks>
        /// <returns>Creates a hotel booking</returns>
        /// <response code="200">Returns a booking reference</response>
        /// <response code="400">Invalid data</response>
        /// <response code="500">Internal Server error</response>
        #region Annotation
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        #endregion
        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequestDTO request)
        {
            try
            {
                DateOnly _checkIn, _checkOut;
                if (!DateOnly.TryParse(request.StartDate, out _checkIn))
                {
                    _logger.LogError("Bad input start date");
                    return BadRequest("Bad input date");
                }

                if (!DateOnly.TryParse(request.EndDate, out _checkOut))
                {
                    _logger.LogError("Bad input end date");
                    return BadRequest("Bad input date");
                }

                if (_checkIn >= _checkOut)
                {
                    return BadRequest("Start date must be before end date.");
                }

                var booking = await _bookingService.CreateBookingAsync(new BookingDTO()
                {
                    RoomTypeId = request.RoomTypeId,
                    StartDate = _checkIn,
                    EndDate = _checkOut,
                    NumberOfGuests = request.NumberOfGuests,
                });

                return Ok(booking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the booking.");
                return StatusCode(500, $"Internal server error.");
            }
        }

        /// <summary>
        /// Returns a booking from the database
        /// </summary>
        /// <param name="referenceNumber">a booking reference as parameter</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Booking/{BookingReference}
        ///     
        /// </remarks>
        /// <returns>A Booking reference</returns>
        /// <response code="200">Returns a booking reference</response>
        /// <response code="404">No booking found</response>
        /// <response code="500">Internal Server error</response>
        #region Annotation
        [ProducesResponseType(typeof(Booking), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        #endregion
        [HttpGet("{referenceNumber}")]
        public async Task<IActionResult> GetBookingByReferenceNumber(string referenceNumber)
        {
            try
            {
                var booking = await _bookingService.GetBookingByReferenceNumberAsync(referenceNumber);
                if (booking == null)
                {
                    _logger.LogError("No booking found");
                    return BadRequest("No booking found");
                }
                return Ok(booking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the booking.");
                return StatusCode(500, $"Internal server error.");
            }
        }

    }
}
