using System.Text.Json;

namespace Question_1.Configuration;

public class AppConfig
{
    public AppSettings AppSettings { get; set; } = new AppSettings();
    public UISettings UI { get; set; } = new UISettings();

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

public class AppSettings
{
    public string AppName { get; set; } = "ASCII og Luhn Algoritme Kalkulator\n";
    
    public string AppDescription { get; set; } = "Dette programmet konverterer tekst til ASCII-verdier og beregner et sjekksiffer ved hjelp av Luhn-algoritmen.";
}

public class UISettings
{
    public ColorSettings Colors { get; set; } = new ColorSettings();
    public PromptSettings Prompts { get; set; } = new PromptSettings();
    public TableHeaderSettings TableHeaders { get; set; } = new TableHeaderSettings();
    public LabelSettings Labels { get; set; } = new LabelSettings();
}

public class ColorSettings
{
    public string Primary { get; set; } = "green";
    public string Secondary { get; set; } = "blue";
    public string Accent { get; set; } = "cyan";
    public string Error { get; set; } = "red";
    public string Info { get; set; } = "grey";
}

public class PromptSettings
{
    public string InputPrompt { get; set; } = "Skriv inn et ord eller navn (kun ASCII-tegn) eller tast Q for å avslutte:";
    public string EmptyInputError { get; set; } = "Input kan ikke være tom";
    public string InvalidInputError { get; set; } = "Ugyldig input. Vennligst bruk kun gyldige ASCII-tegn.";
    public string ExitMessage { get; set; } = "Avslutter programmet...";
    public string EndMessage { get; set; } = "Trykk en tast for å avslutte...";
}

public class TableHeaderSettings
{
    public string Input { get; set; } = "Input";
    public string Description { get; set; } = "Beskrivelse";
    public string Output { get; set; } = "Output";
}

public class LabelSettings
{
    public string AsciiValues { get; set; } = "ASCII-verdier:";
    public string CombinedAscii { get; set; } = "Kombinert ASCII-verdi:";
    public string Checksum { get; set; } = "Sjekksiffer (Luhn):";
    public string FinalOutput { get; set; } = "Ferdig streng med sjekksiffer:";
}
