using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Question_2.Models;

namespace Question_2.Services;

public class ExamTaskService
{
    private readonly HttpClient _httpClient;
    private const string ApiUrl = "https://exam.05093218.nip.io/api/ExamTask";
    private const string ApiKey = "b569e4f6-77c2-475a-bf77-d15e81dd4dbd";

    public ExamTaskService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("X-API-KEY", ApiKey);
    }

    public async Task<ExamData?> GetExamDataAsync()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            var data = await JsonSerializer.DeserializeAsync<ExamData>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Feil under API-kall: {ex.Message}");
            return null;
        }
    }
}