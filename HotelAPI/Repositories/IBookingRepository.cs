using HotelAPI.ModelDTOs;
using HotelAPI.Models;

namespace HotelAPI.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking> GetBookingByReferenceNumberAsync(string referenceNumber);
        Task<string> SaveBookingAsync(BookingDTO booking);
        Task<IEnumerable<RoomAvailableDTO>> FindRoomsAvailableAsync(int numberOfPeople, DateOnly checkIn, DateOnly checkOut);
    }
}
