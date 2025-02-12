using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public string? BookingNumber { get; set; }
        [Required]
        public int RoomTypeId { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        public int NumberOfGuests { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
