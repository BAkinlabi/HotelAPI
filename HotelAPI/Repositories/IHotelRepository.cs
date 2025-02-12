using HotelAPI.ModelDTOs;
using HotelAPI.Models;

namespace HotelAPI.Repositories
{
    public interface IHotelRepository
    {
        Task<HotelDTO> GetHotelByNameAsync(string name);
    }
}
