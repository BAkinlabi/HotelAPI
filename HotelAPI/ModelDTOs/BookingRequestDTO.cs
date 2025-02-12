namespace HotelAPI.ModelDTOs
{
    public class BookingRequestDTO
    {
        public int RoomTypeId { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int NumberOfGuests { get; set; }
    }
}
