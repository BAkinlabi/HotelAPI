using HotelAPI.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
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
            // Log the exception (logging mechanism can be added here)
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
