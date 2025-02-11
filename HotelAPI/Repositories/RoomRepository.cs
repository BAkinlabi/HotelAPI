using Azure.Core;
using HotelAPI.Data;
using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace HotelAPI.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _context;

        public RoomRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetAll() => _context.Rooms;

        public async Task<Room> GetRoomByIdAsync(int RoomId)
        {
            return await _context.Rooms
                .Include(r => r.Bookings)
                .FirstOrDefaultAsync(r => r.Id == RoomId) ?? throw new InvalidOperationException("No room found.");
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            try
            {
                var rooms = await _context.Rooms.Include(r => r.Bookings).ToListAsync();
                if (rooms == null || !rooms.Any())
                {
                    throw new InvalidOperationException("No rooms found.");
                }
                return rooms;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                throw new Exception("An error occurred while retrieving rooms.", ex);
            }
        }

        public async Task<IEnumerable<Room>> FindAvailableRoomsAsync(DateTime startDate, DateTime endDate, int numberOfGuests)
        {
            try
            {
                var availableRooms = await _context.Rooms
                    .Include(r => r.Bookings)
                    .Where(r => r.Capacity >= numberOfGuests &&
                                !r.Bookings.Any(b => b.StartDate < endDate && b.EndDate > startDate))
                    .ToListAsync();

                if (availableRooms == null || !availableRooms.Any())
                {
                    throw new InvalidOperationException("No available rooms found.");
                }

                return availableRooms;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                throw new Exception("An error occurred while finding available rooms.", ex);
            }
        }
    }
}
