using Expenses.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

public class AiService : IAiService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _configuration;

    public AiService(HttpClient http, IConfiguration configuration)
    {
        _http = http;
        _configuration = configuration;
    }

    public async Task<string> GenerateAsync(string prompt)
    {
        var apiKey = _configuration["DeepSeek:ApiKey"];

        if (string.IsNullOrWhiteSpace(apiKey))
            throw new Exception("DeepSeek API key not configured");

        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var response = await _http.PostAsJsonAsync("https://api.deepseek.com/chat/completions",
            new
            {
                model = "deepseek-v4-flash",
                messages = new[]
                {
                    new { role = "system", content = "Ты финансовый аналитик." },
                    new { role = "user", content = prompt }
                }
            });

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"DeepSeek error: {content}");

        var json = JsonDocument.Parse(content).RootElement;

        return json
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString()!;
    }
}