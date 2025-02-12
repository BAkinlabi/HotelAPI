using HotelAPI.Data;
using HotelAPI.ModelDTOs;
using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HotelRepository> _logger;

        public HotelRepository(AppDbContext context, ILogger<HotelRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<HotelDTO> GetHotelByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogError("Bad name input data.");
                throw new ArgumentException("Hotel name cannot be null or empty.", nameof(name));
            }

            try
            {
                HotelDTO hotelDTO = new HotelDTO();
                var _hotel = await _context.Hotels.FirstOrDefaultAsync(n => n.Name == name);
                
                if(_hotel == null ) { return null; }          
  
                Dictionary<string, int> _rooms = _context.RoomTypes.ToDictionary(n => n.Name, n => n.Capacity);
                hotelDTO.Id = _hotel.Id;
                hotelDTO.Name = _hotel.Name;
                hotelDTO.NumberOfRooms = _hotel.NoOfRooms;
                hotelDTO.RoomTypes = _rooms;
                

                return hotelDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return null;
            }
        }


    }

}
