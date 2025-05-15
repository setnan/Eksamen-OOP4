using System.Text.Json;

namespace Question_2.Configuration;

/// <summary>
/// Konfigurasjonklasse som håndterer innlasting av applikasjonsinnstillinger fra appsettings.json og miljøvariabler.
/// Inneholder innstillinger for API-forbindelsen og andre konfigurasjonsparametre.
/// </summary>
public class AppConfig
{
    /// <summary>
    /// Får eller setter API-innstillingene for applikasjonen.
    /// Inneholder URL og API-nøkkel for API-tilgang.
    /// </summary>
    public ApiSettings Api { get; set; } = new ApiSettings();

    /// <summary>
    /// Laster applikasjonsinnstillinger fra appsettings.json-filen.
    /// </summary>
    /// <returns>En instans av AppConfig med innstillinger fra konfigurasjonsfilen, eller standardinnstillinger hvis filen ikke finnes eller er ugyldig.</returns>
    /// <remarks>
    /// Metoden søker etter konfigurasjonsfilen først i applikasjonens katalog, deretter i arbeidskatalogen.
    /// Ved feil under innlasting brukes standardverdier og en advarsel vises i konsollen.
    /// </remarks>
    /// <summary>
    /// Laster applikasjonsinnstillinger fra appsettings.json-filen og miljøvariabler.
    /// Miljøvariabler vil overstyre verdier fra konfigurasjonsfilen hvis de finnes.
    /// </summary>
    /// <returns>En instans av AppConfig med innstillinger fra konfigurasjonsfiler og/eller miljøvariabler.</returns>
    public static AppConfig Load()
    {
        // Start med standardkonfigurasjon
        AppConfig config = new AppConfig();
        
        // Prøv å laste fra JSON-fil
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
        
        if (!File.Exists(filePath))
        {
            filePath = "appsettings.json"; // Fallback til arbeidskatalogen
            
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Advarsel: appsettings.json ikke funnet. Sjekker miljøvariabler.");
            }
            else
            {
                // Fil funnet i arbeidskatalog, last inn
                config = LoadFromJsonFile(filePath) ?? config;
            }
        }
        else
        {
            // Fil funnet i applikasjonens katalog, last inn
            config = LoadFromJsonFile(filePath) ?? config;
        }
        
        // Sjekk for miljøvariabler og la dem overstyre konfigurasjonsfilen
        config = LoadFromEnvironment(config);
        
        return config;
    }
    
    /// <summary>
    /// Laster konfigurasjon fra en JSON-fil.
    /// </summary>
    /// <param name="filePath">Sti til JSON-konfigurasjonsfilen.</param>
    /// <returns>En AppConfig-instans eller null ved feil.</returns>
    private static AppConfig? LoadFromJsonFile(string filePath)
    {
        try
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<AppConfig>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Advarsel: Feil ved lesing av konfigurasjonsfil: {ex.Message}");
            return null;
        }
    }
    
    /// <summary>
    /// Oppdaterer konfigurasjon med verdier fra miljøvariabler hvis disse finnes.
    /// </summary>
    /// <param name="config">Eksisterende konfigurasjon som skal oppdateres.</param>
    /// <returns>Oppdatert konfigurasjon.</returns>
    private static AppConfig LoadFromEnvironment(AppConfig config)
    {
        // Sjekk og last miljøvariabel for API URL
        string? apiUrlEnv = Environment.GetEnvironmentVariable("EXAM_API_URL");
        if (!string.IsNullOrWhiteSpace(apiUrlEnv))
        {
            config.Api.BaseUrl = apiUrlEnv;
            Console.WriteLine("Info: API URL lastet fra miljøvariabel.");
        }
        
        // Sjekk og last miljøvariabel for API-nøkkel
        string? apiKeyEnv = Environment.GetEnvironmentVariable("EXAM_API_KEY");
        if (!string.IsNullOrWhiteSpace(apiKeyEnv))
        {
            config.Api.ApiKey = apiKeyEnv;
            Console.WriteLine("Info: API-nøkkel lastet fra miljøvariabel.");
        }
        
        return config;
    }
}

/// <summary>
/// Inneholder spesifikke innstillinger for API-tilkobling.
/// </summary>
/// <summary>
/// Inneholder spesifikke innstillinger for API-tilkobling.
/// Kan konfigureres via appsettings.json eller miljøvariabler (EXAM_API_URL, EXAM_API_KEY).
/// </summary>
public class ApiSettings
{
    /// <summary>
    /// Får eller setter basis-URL til API-endepunktet.
    /// Kan overstyres med miljøvariabelen EXAM_API_URL.
    /// </summary>
    public string BaseUrl { get; set; } = "https://exam.05093218.nip.io/api/ExamTask";
    
    /// <summary>
    /// Får eller setter API-nøkkelen som brukes for autentisering mot API-et.
    /// Kan overstyres med miljøvariabelen EXAM_API_KEY.
    /// </summary>
    public string ApiKey { get; set; } = "b569e4f6-77c2-475a-bf77-d15e81dd4dbd";
}
