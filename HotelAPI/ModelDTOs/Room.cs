﻿namespace HotelAPI.ModelDTOs
{
    public class RoomAvailableDTO
    {
        public int Id {get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }
}
