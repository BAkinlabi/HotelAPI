using HotelAPI.Data;
using HotelAPI.Models;

namespace HotelAPI.Services
{
    public class DataService : IDataService
    {
        private readonly AppDbContext _context;

        public DataService(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedDataAsync()
        {
            if (!_context.Hotels.Any())
            {
                var hotel = new Hotel
                {
                    Name = "Sample Hotel",
                    Rooms = new List<Room>
                {
                    new Room { Type = "Single", Capacity = 1 },
                    new Room { Type = "Double", Capacity = 2 },
                    new Room { Type = "Deluxe", Capacity = 4 },
                    new Room { Type = "Single", Capacity = 1 },
                    new Room { Type = "Double", Capacity = 2 },
                    new Room { Type = "Deluxe", Capacity = 4 }
                }
                };

                _context.Hotels.Add(hotel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ResetDataAsync()
        {
            _context.Bookings.RemoveRange(_context.Bookings);
            _context.Rooms.RemoveRange(_context.Rooms);
            _context.Hotels.RemoveRange(_context.Hotels);
            await _context.SaveChangesAsync();
        }
    }
}
