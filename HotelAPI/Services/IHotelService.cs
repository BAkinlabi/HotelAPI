using HotelAPI.ModelDTOs;

namespace HotelAPI.Services
{
    public interface IHotelService
    {
        Task<HotelDTO> GetHotelByNameAsync(string name);
    }
}
