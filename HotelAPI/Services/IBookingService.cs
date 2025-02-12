using HotelAPI.ModelDTOs;
using HotelAPI.Models;

namespace HotelAPI.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<RoomAvailableDTO>> CheckRoomAvailibilityAsync(int numberOfPeople, DateOnly checkIn, DateOnly checkOut);
        Task<string> CreateBookingAsync(BookingDTO request);
        Task<Booking> GetBookingByReferenceNumberAsync(string referenceNumber);
    }
}