namespace HotelAPI.Models
{
    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int NumberOfRooms { get; set; }
        public int Capacity { get; set; }
    }
}
