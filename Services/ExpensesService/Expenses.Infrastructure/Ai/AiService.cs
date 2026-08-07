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
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["OpenAI:ApiKey"]);

        var response = await _http.PostAsJsonAsync("https://api.openai.com/v1/chat/completions",
            new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                new { role = "system", content = "Ты финансовый аналитик." },
                new { role = "user", content = prompt }
                }
            });

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"OpenAI error: {content}");

        var json = JsonDocument.Parse(content).RootElement;

        if (!json.TryGetProperty("choices", out var choices))
            throw new Exception($"Invalid OpenAI response: {content}");

        return choices[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString()!;
    }
}