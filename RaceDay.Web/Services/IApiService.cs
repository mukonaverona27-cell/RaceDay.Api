namespace RaceDay.Web.Services
{
    public interface IApiService
    {
        Task<T?> GetAsync<T>(string endpoint, string? token = null);
        Task<(bool success, T? data, string? error)> PostAsync<T>(string endpoint, object data, string? token = null);
        Task<(bool success, string? error)> PutAsync(string endpoint, object data, string? token = null);
        Task<(bool success, string? error)> DeleteAsync(string endpoint, string? token = null);
    }
}