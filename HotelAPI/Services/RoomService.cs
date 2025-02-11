using HotelAPI.Data;
using HotelAPI.Models;
using HotelAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
             _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Room>> FindAvailableRoomsAsync(DateTime startDate, DateTime endDate, int numberOfGuests)
        {
            var availableRooms = await _roomRepository.FindAvailableRoomsAsync(startDate, endDate, numberOfGuests);

            return availableRooms;
        }
    }
}
