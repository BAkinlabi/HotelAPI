using HotelAPI.Models;

namespace HotelAPI.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking> GetBookingByReferenceNumberAsync(string referenceNumber);
        Task<Booking> SaveBookingAsync(Booking booking);
    }
}
