using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace RaceDay.Web.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<T?> GetAsync<T>(string endpoint, string? token = null)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var resp = await _http.SendAsync(request);
            if (!resp.IsSuccessStatusCode) return default;
            var json = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<(bool success, T? data, string? error)> PostAsync<T>(string endpoint, object data, string? token = null)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(data);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await _http.SendAsync(request);

            if (!resp.IsSuccessStatusCode)
            {
                var errorBody = await resp.Content.ReadAsStringAsync();
                return (false, default, $"{resp.StatusCode} - {errorBody}");
            }

            var resultJson = await resp.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(resultJson))
                return (true, default, null);

            return (true, JsonSerializer.Deserialize<T>(resultJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }), null);
        }

        public async Task<(bool success, string? error)> PutAsync(string endpoint, object data, string? token = null)
        {
            using var request = new HttpRequestMessage(HttpMethod.Put, endpoint);
            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(data);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await _http.SendAsync(request);
            return (resp.IsSuccessStatusCode, resp.ReasonPhrase);
        }

        public async Task<(bool success, string? error)> DeleteAsync(string endpoint, string? token = null)
        {
            using var request = new HttpRequestMessage(HttpMethod.Delete, endpoint);
            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var resp = await _http.SendAsync(request);
            return (resp.IsSuccessStatusCode, resp.ReasonPhrase);
        }
    }

    public class AuthResponse
    {
        public string Token { get; set; } = "";
        public string Role { get; set; } = "";
        public string UserId { get; set; } = "";
        public string FullName { get; set; } = "";
    }
}