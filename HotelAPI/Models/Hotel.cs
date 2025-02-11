using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string? Name { get; set; }

        public ICollection<Room>? Rooms { get; set; }
    }
}
