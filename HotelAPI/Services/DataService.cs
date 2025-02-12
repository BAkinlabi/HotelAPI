using HotelAPI.Data;
using HotelAPI.Models;

namespace HotelAPI.Services
{
    public class DataService : IDataService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DataService> _logger;

        public DataService(AppDbContext context, ILogger<DataService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedDataAsync()
        {
            try
            {
                if (!_context.Hotels.Any())
                {
                    var hotel = new Hotel
                    {
                        Name = "Test Hotel",
                        NoOfRooms = 6
                    };

                    _context.Hotels.Add(hotel);

                    var roomTypes = new List<RoomType>() {
                            new RoomType { Name = "Single", NumberOfRooms = 3, Capacity = 1 },
                            new RoomType { Name = "Double", NumberOfRooms = 1, Capacity = 2 },
                            new RoomType { Name = "Deluxe", NumberOfRooms = 2, Capacity = 4 }
                        };
                    _context.RoomTypes.AddRange(roomTypes);

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding data.");
            }
        }

        public async Task ResetDataAsync()
        {
            try
            {
                _context.Bookings.RemoveRange(_context.Bookings);
                _context.RoomTypes.RemoveRange(_context.RoomTypes);
                _context.Hotels.RemoveRange(_context.Hotels);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while resetting data.");
            }
        }
    }
}
