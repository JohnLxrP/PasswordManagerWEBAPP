using System.Text;
using Newtonsoft.Json;
using PasswordManagerWEBAPP.Models;

namespace PasswordManagerWEBAPP.Data.Repositories;

public class PasswordmngrsRepository : IPasswordmngrsRepository
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configs;

    public PasswordmngrsRepository(IConfiguration configs)
    {
        _httpClient = new HttpClient();
        _configs = configs;
        // jsonplaceholder.typicode server
        //_httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
        // Local server
        _httpClient.BaseAddress = new Uri("http://localhost:5283");
    }

    public async Task<Passwordmngr?> CreatePasswordmngr(Passwordmngr newPasswordmngr)
    {
        var newPasswordmngrAsString = JsonConvert.SerializeObject(newPasswordmngr);
        var requestBody = new StringContent(newPasswordmngrAsString, Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Add("ApiKey", "RANDomValuetoDenoteAPIKeyWithNumbers131235");
        _httpClient.DefaultRequestHeaders.Add("Authorization","Bearer " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2ODQyMTM3MzUsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTI4MyIsImF1ZCI6IlVzZXIifQ.mgRVAgMveIbDBFrGhYqV4LqqSkKea3me5XGq8gZZSxk");
        var response = await _httpClient.PostAsync("/passwordmngrs", requestBody);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var passwordmngr = JsonConvert.DeserializeObject<Passwordmngr>(content);
            return passwordmngr;
        }

        return null;
    }

public async Task DeletePasswordmngr(int passwordmngrId, string token)
{
    _httpClient.DefaultRequestHeaders.Clear();
    _httpClient.DefaultRequestHeaders.Add("ApiKey", _configs.GetValue<string>("ApiKey"));
    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

    var response = await _httpClient.DeleteAsync($"/passwordmngrs/{passwordmngrId}");
    if (response.IsSuccessStatusCode)
    {
        var data = await response.Content.ReadAsByteArrayAsync();
        Console.WriteLine("Delete Password Response: ", data);
    }
}

    public async Task<List<Passwordmngr>> GetAllPasswordmngrs(string token)
    {
        _httpClient.DefaultRequestHeaders.Add("ApiKey", _configs.GetValue<string>("ApiKey"));
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        var response = await _httpClient.GetAsync("/Passwordmngrs");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var passwordmngrs = JsonConvert.DeserializeObject<List<Passwordmngr>>(content);
            return passwordmngrs ?? new();
        }

        return new();
    }

    public async Task<Passwordmngr?> GetPasswordmngrById(int passwordmngrId)
    {
        var response = await _httpClient.GetAsync($"/passwordmngrs/{passwordmngrId}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var passwordmngr = JsonConvert.DeserializeObject<Passwordmngr>(content);
            return passwordmngr;
        }

        return null;
    }

    public async Task<Passwordmngr?> UpdatePasswordmngr(int passwordmngrId, Passwordmngr updatedPasswordmngr, string token)
    {
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("ApiKey", _configs.GetValue<string>("ApiKey"));
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

        var newPasswordmngrAsString = JsonConvert.SerializeObject(updatedPasswordmngr);
        var responseBody = new StringContent(newPasswordmngrAsString, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"/passwordmngrs/{passwordmngrId}", responseBody);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var passwordmngr = JsonConvert.DeserializeObject<Passwordmngr>(content);
            return passwordmngr;
        }

        return null;
    }
}