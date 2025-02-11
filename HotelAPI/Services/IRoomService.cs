using HotelAPI.Models;

namespace HotelAPI.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> FindAvailableRoomsAsync(DateTime startDate, DateTime endDate, int numberOfGuests);
    }
}
