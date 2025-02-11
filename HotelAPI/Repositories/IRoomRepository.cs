using HotelAPI.Models;

namespace HotelAPI.Repositories
{
    public interface IRoomRepository
    {
        Task<Room> GetRoomByIdAsync(int RoomId);
        Task<IEnumerable<Room>> FindAvailableRoomsAsync(DateTime startDate, DateTime endDate, int numberOfGuests);
        Task<IEnumerable<Room>> GetAllRoomsAsync();
    }
}
