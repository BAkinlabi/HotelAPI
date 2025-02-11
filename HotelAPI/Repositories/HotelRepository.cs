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
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Hotel name cannot be null or empty.", nameof(name));
            }

            try
            {
                return await _context.Hotels.Include(h => h.Rooms).FirstOrDefaultAsync(h => h.Name == name)
                    ?? throw new InvalidOperationException("Hotel not found.");
            }
            catch (DbUpdateException ex)
            {
                // Log the exception (logging mechanism not shown here)
                throw new Exception("An error occurred while accessing the database.", ex);
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        
    }

}
