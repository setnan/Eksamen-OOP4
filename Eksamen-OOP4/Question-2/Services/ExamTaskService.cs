using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Question_2.Configuration;
using Question_2.Models;

namespace Question_2.Services;

/// <summary>
/// Tjenesteklasse ansvarlig for kommunikasjon med API-et for henting av eksamensdata.
/// Håndterer HTTP-forespørsler, autentisering med API-nøkkel og deserialisering av JSON-respons.
/// </summary>
public class ExamTaskService
{
    /// <summary>
    /// HttpClient-instans for å utføre HTTP-forespørsler mot API-et.
    /// </summary>
    private readonly HttpClient _httpClient;
    
    /// <summary>
    /// URL til API-endepunktet som skal kontaktes.
    /// </summary>
    private readonly string _apiUrl;

    /// <summary>
    /// Initialiserer en ny instans av ExamTaskService.
    /// Laster innstillinger fra konfigurasjonsfilen og konfigurerer HttpClient med nødvendig API-nøkkel.
    /// </summary>
    public ExamTaskService()
    {
        AppConfig config = AppConfig.Load();
        _apiUrl = config.Api.BaseUrl;
        
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("X-API-KEY", config.Api.ApiKey);
    }

    /// <summary>
    /// Henter eksamensdata asynkront fra API-et.
    /// </summary>
    /// <returns>Et ExamData-objekt med søkere og stillinger hvis vellykket, eller null hvis forespørselen mislykkes.</returns>
    /// <remarks>
    /// Metoden håndterer forskjellige feilscenarier som nettverksfeil, deserialiseringsfeil og tidsavbrudd,
    /// og gir passende feilmeldinger til konsollen.
    /// </remarks>
    public async Task<ExamData?> GetExamDataAsync()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            ExamData? data = await JsonSerializer.DeserializeAsync<ExamData>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (data == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Advarsel: Ingen data mottatt fra API-et, eller data kunne ikke deserialiseres.");
                Console.ResetColor();
                return null;
            }

            // Validerer at vi har mottatt data
            if (data.Applicants == null || data.Positions == null || !data.Applicants.Any() || !data.Positions.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Advarsel: Mottok ufullstendige data fra API-et. Søkere: {(data.Applicants?.Count ?? 0)}, Stillinger: {(data.Positions?.Count ?? 0)}");
                Console.ResetColor();
            }

            return data;
        }
        catch (HttpRequestException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Nettverksfeil ved API-kall: {ex.Message}");
            Console.WriteLine($"StatusCode: {ex.StatusCode}");
            Console.ResetColor();
            return null;
        }
        catch (JsonException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Feil ved deserialisering av API-respons: {ex.Message}");
            Console.ResetColor();
            return null;
        }
        catch (TaskCanceledException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"API-kallet ble kansellert eller tidsavbrutt: {ex.Message}");
            Console.ResetColor();
            return null;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Uventet feil under API-kall: {ex.Message}");
            Console.ResetColor();
            return null;
        }
    }
}