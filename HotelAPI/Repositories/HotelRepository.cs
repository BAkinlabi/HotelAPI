using HotelAPI.Data;
using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly AppDbContext _context;

        public HotelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Hotel> GetHotelByNameAsync(string name)
        {
            return await _context.Hotels.Include(h => h.Rooms).FirstOrDefaultAsync(h => h.Name == name) ?? throw new InvalidOperationException("Hotel not found.");
        }

        
    }

}
