using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    public class Booking
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? BookingNumber { get; set; }

    [Required]
    public int RoomId { get; set; }
    
    public Room? Room { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public int NumberOfGuests { get; set; }
}
}
