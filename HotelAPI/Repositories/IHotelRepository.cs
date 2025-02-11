using HotelAPI.Models;

namespace HotelAPI.Repositories
{
    public interface IHotelRepository
    {
        Task<Hotel> GetHotelByNameAsync(string name);
    }
}
