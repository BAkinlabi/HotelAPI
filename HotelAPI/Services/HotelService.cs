using HotelAPI.Models;
using HotelAPI.Repositories;

namespace HotelAPI.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<Hotel> GetHotelByNameAsync(string name)
        {
            return await _hotelRepository.GetHotelByNameAsync(name);
        }
    }
}
