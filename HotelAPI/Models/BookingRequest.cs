namespace HotelAPI.Models
{
    public class BookingRequest
    {
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfGuests { get; set; }
    }
}
