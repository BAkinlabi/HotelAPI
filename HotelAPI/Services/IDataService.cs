namespace HotelAPI.Services
{
    public interface IDataService
    {
        Task SeedDataAsync();
        Task ResetDataAsync();
    }
}
