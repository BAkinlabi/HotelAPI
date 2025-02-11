using HotelAPI.Data;
using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> GetBookingByReferenceNumberAsync(string referenceNumber)
        {
            return await _context.Bookings
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.BookingNumber == referenceNumber) ?? throw new InvalidOperationException("Booking not found.");
        }  
        public async Task<Booking> SaveBookingAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }


    }

}
