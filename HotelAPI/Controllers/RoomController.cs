using HotelAPI.Controllers;
using HotelAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;
    private readonly ILogger<RoomController> _logger;

    public RoomController(IRoomService roomService, ILogger<RoomController> logger)
    {
        _roomService = roomService;
        _logger = logger;
    }

    [HttpGet("available")]
    public async Task<IActionResult> FindAvailableRooms([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int numberOfGuests)
    {
        try
        {
            var availableRooms = await _roomService.FindAvailableRoomsAsync(startDate, endDate, numberOfGuests);
            return Ok(availableRooms);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the available rooms.");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
