using System.ComponentModel.DataAnnotations;

namespace HotelAPI.ModelDTOs
{
    public class RoomsAvailableRequestDTO
    {
        [Required]
        public int NumberOfPeople { get; set; }
        [Required]
        public string CheckInDate { get; set; }
        [Required]
        public string CheckOutDate { get; set; }
    }
}
