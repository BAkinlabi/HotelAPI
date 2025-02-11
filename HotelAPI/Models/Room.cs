using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        public int Capacity { get; set; }

        [Required]
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
