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
            try
            {
                var booking = await _context.Bookings
                    .Include(b => b.Room)
                    .FirstOrDefaultAsync(b => b.BookingNumber == referenceNumber);

                if (booking == null)
                {
                    throw new InvalidOperationException("Booking not found.");
                }

                return booking;
            }
            catch (DbUpdateException ex)
            {
                // Log the exception (logging mechanism not shown here)
                throw new Exception("An error occurred while retrieving the booking.", ex);
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
        public async Task<Booking> SaveBookingAsync(Booking booking)
        {
            try
            {
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
                return booking;
            }
            catch (DbUpdateException ex)
            {
                // Log the exception (logging mechanism not shown here)
                throw new Exception("An error occurred while saving the booking.", ex);
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                throw new Exception("An unexpected error occurred.", ex);
            }
        }


    }

}
