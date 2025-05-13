using System.Text.Json;

namespace Question_2.Configuration;

public class AppConfig
{
    public ApiSettings Api { get; set; } = new ApiSettings();

    public static AppConfig Load()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
        
        if (!File.Exists(filePath))
        {
            filePath = "appsettings.json"; // Fallback til arbeidskatalogen
            
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Advarsel: appsettings.json ikke funnet. Bruker standardverdier.");
                return new AppConfig();
            }
        }

        string json = File.ReadAllText(filePath);
        
        try
        {
            return JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Feil ved lesing av konfigurasjon: {ex.Message}");
            return new AppConfig();
        }
    }
}

public class ApiSettings
{
    public string BaseUrl { get; set; } = "https://exam.05093218.nip.io/api/ExamTask";
    public string ApiKey { get; set; } = "b569e4f6-77c2-475a-bf77-d15e81dd4dbd";
}
