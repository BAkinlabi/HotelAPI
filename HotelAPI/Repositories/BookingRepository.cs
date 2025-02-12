using HotelAPI.Controllers;
using HotelAPI.Data;
using HotelAPI.ModelDTOs;
using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BookingRepository> _logger;

        public BookingRepository(AppDbContext context, ILogger<BookingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Booking> GetBookingByReferenceNumberAsync(string referenceNumber)
        {
            try
            {
                var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingNumber == referenceNumber);

                if (booking == null)
                {
                    _logger.LogInformation("Booking not found.");
                    throw new InvalidOperationException("Booking not found.");
                }

                return booking;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        public async Task<string> SaveBookingAsync(BookingDTO booking)
        {
            try
            {
                var bookingRef = GenerateBookingReference();

                Booking myBooking = new Booking()
                {
                    BookingNumber = bookingRef.ToString(),
                    RoomTypeId = booking.RoomTypeId,
                    StartDate = booking.StartDate,
                    EndDate = booking.EndDate,
                    NumberOfGuests = booking.NumberOfGuests,
                    CreatedDate = DateTime.UtcNow
                };
                _context.Bookings.Add(myBooking);

                await _context.SaveChangesAsync();
                return bookingRef;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while saving the booking.");
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                return string.Empty;
            }
        }

        public async Task<IEnumerable<RoomAvailableDTO>> FindRoomsAvailableAsync(int numberOfPeople, DateOnly checkIn, DateOnly checkOut)
        {
            try
            {
                if (!_context.Bookings.Any())
                {
                    return _context.RoomTypes.Where(c => c.Capacity >= numberOfPeople)
                        .Select(c => new RoomAvailableDTO()
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Capacity = c.Capacity,
                        });
                }

                var existingBookings = await _context.Bookings.Where(d => (d.StartDate <= checkIn && d.EndDate >= checkIn)).ToListAsync();
                var allRoomTypes = await _context.RoomTypes.Where(c => c.Capacity >= numberOfPeople).ToListAsync();

                // Console.WriteLine("Number of items in the all room types list is  " + allRoomTypes.Count);

                foreach (var booking in existingBookings)
                {
                    var _roomType = allRoomTypes.Find(rt => booking.RoomTypeId == rt.Id);

                    if (_roomType != null)
                    {
                        _roomType.NumberOfRooms--;

                        if (_roomType.NumberOfRooms == 0)
                        {
                            allRoomTypes.Remove(_roomType);
                        }
                    }
                }

                // Console.WriteLine("Number of items in the all room types list is now " + allRoomTypes.Count);

                return allRoomTypes.Select(c => new RoomAvailableDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Capacity = c.Capacity,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while finding available rooms.");
                return null;
            }
        }


        private Random random = new Random();

        private string GenerateUniqueString(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GenerateBookingReference()
        {
            string bookingRef;
            try
            {
                do
                {
                    bookingRef = GenerateUniqueString();
                } while (BookingReferenceExists(bookingRef));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating the booking reference.");
                return string.Empty;
            }

            return bookingRef;
        }
        private bool BookingReferenceExists(string bookingRef)
        {
            return _context.Bookings.Any(b => b.BookingNumber == bookingRef);
        }
    }

}
