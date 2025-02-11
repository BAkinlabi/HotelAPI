using HotelAPI.Models;
using HotelAPI.Repositories;

namespace HotelAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IRoomRepository roomRepository, IBookingRepository bookingRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<Booking> CreateBookingAsync(BookingRequest request)
        {
            // This implementation ensures that:
            // A room cannot be double booked for any given night.
            // A room cannot be occupied by more people than its capacity.
            // Guests do not need to change rooms during their stay.

            // Check if the room is available for the entire stay
            var room = await _roomRepository.GetRoomByIdAsync(request.RoomId);

            if (room == null)
            {
                throw new Exception("Room not found.");
            }

            if (room.Capacity < request.NumberOfGuests)
            {
                throw new Exception("Room capacity exceeded.");
            }

            var overlappingBookings = room.Bookings
                .Where(b => b.StartDate < request.EndDate && b.EndDate > request.StartDate)
                .ToList();

            if (overlappingBookings.Any())
            {
                throw new Exception("Room is already booked for the selected dates.");
            }

            // Create the booking
            var booking = new Booking
            {
                BookingNumber = Guid.NewGuid().ToString(),
                RoomId = request.RoomId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                NumberOfGuests = request.NumberOfGuests
            };

            return await _bookingRepository.SaveBookingAsync(booking);
        }

        public async Task<Booking> GetBookingByReferenceNumberAsync(string referenceNumber)
        {
            return await _bookingRepository.GetBookingByReferenceNumberAsync(referenceNumber);
        }
    }
}
