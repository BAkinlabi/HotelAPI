namespace HotelAPI.ModelDTOs
{
    public class BookingDTO
    {
        public int RoomTypeId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int NumberOfGuests { get; set; }
    }
}
