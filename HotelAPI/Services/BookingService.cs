using HotelAPI.ModelDTOs;
using HotelAPI.Models;
using HotelAPI.Repositories;

namespace HotelAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<RoomAvailableDTO>> CheckRoomAvailibilityAsync(int numberOfPeople, DateOnly checkIn, DateOnly checkOut)
        {
            Console.WriteLine($"Check available rooms for {numberOfPeople}, people. Starting on {checkIn} and leaving on {checkOut}");

            // Check if any bookings exist in booking table and remove them from the available rooms to return to the caller
            var result = await _bookingRepository.FindRoomsAvailableAsync(numberOfPeople, checkIn, checkOut);


            return result;
        }

        public async Task<string> CreateBookingAsync(BookingDTO request)
        {
            // Create the booking
            return await _bookingRepository.SaveBookingAsync(request);

        }

        public async Task<Booking> GetBookingByReferenceNumberAsync(string referenceNumber)
        {
            return await _bookingRepository.GetBookingByReferenceNumberAsync(referenceNumber);
        }
    }
}
