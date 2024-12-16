using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using MyDictionaryApp.Models;

namespace MyDictionaryApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ApiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            
            // Чтение базового URL из конфигурации
            _baseUrl = configuration["ApiSettings:BaseUrl"] ?? throw new InvalidOperationException("Base URL is not configured.");
        }

        public async Task<List<WordDto>> GetAllWordsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Words");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<WordDto>>(json) ?? new List<WordDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
                return new List<WordDto>();
            }
        }

        public async Task AddNewWordAsync(WordDto wordDto)
        {
            var json = JsonSerializer.Serialize(wordDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/api/Words", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateWordAsync(int id, WordDto wordDto)
        {
            var content = new StringContent(JsonSerializer.Serialize(wordDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/api/Words/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteWordAsync(string original)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/Words/original/{original}");
            response.EnsureSuccessStatusCode();
        }
    }
}
