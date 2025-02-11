using HotelAPI.Models;

namespace HotelAPI.Services
{
    public interface IBookingService
    {
        Task<Booking> CreateBookingAsync(BookingRequest request);
        Task<Booking> GetBookingByReferenceNumberAsync(string referenceNumber);
    }
}
