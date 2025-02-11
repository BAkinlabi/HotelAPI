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
            return await _context.Rooms.Include(r => r.Bookings).ToListAsync() ?? throw new InvalidOperationException("No rooms found.");
        }

        public async Task<IEnumerable<Room>> FindAvailableRoomsAsync(DateTime startDate, DateTime endDate, int numberOfGuests)
        {
            return await _context.Rooms
                .Include(r => r.Bookings)
                .Where(r => r.Capacity >= numberOfGuests &&
                            !r.Bookings.Any(b => b.StartDate<endDate && b.EndDate> startDate))
               .ToListAsync();
        }
    }
}
