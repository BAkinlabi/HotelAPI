using HotelAPI.Models;

namespace HotelAPI.Services
{
    public interface IHotelService
    {
        Task<Hotel> GetHotelByNameAsync(string name);
    }
}
