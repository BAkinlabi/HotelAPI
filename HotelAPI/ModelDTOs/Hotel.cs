namespace HotelAPI.ModelDTOs
{
    public class HotelDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int NumberOfRooms { get; set; }
        public Dictionary<string, int>? RoomTypes { get; set; }
    }
}
