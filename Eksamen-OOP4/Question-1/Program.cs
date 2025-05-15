using System;
using Question_1;
using Question_1.Configuration;
using Question_1.UI;
using Spectre.Console;

namespace Question_1
{
    /// <summary>
    /// Startpunkt for ASCII- og Luhn-algoritme-kalkulatoren.
    /// Håndterer brukerinput, konvertering og beregning av sjekksiffer.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Hovedmetode som initierer programmet, laster konfigurasjon og kjører hovedloop for brukerinteraksjon.
        /// </summary>
        /// <param name="args">Kommando-linje argumenter (ikke brukt).</param>
        static void Main(string[] args)
        {
            // Laster konfigurasjon
            AppConfig config = AppConfig.Load();
            UISettings uiConfig = config.UI;

            // Viser header "ASCII og Luhn Algoritme Kalkulato"
            UIHelper.DisplayHeader(config.AppSettings);

            // Initialiserer konverterere
            AsciiConverter converter = new();
            LuhnCalculator calculator = new();
            
            while (true)
            {
                string input = UIHelper.GetUserInput(uiConfig.Prompts, uiConfig.Colors);

                if (input.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    AnsiConsole.MarkupLine($"\n[{uiConfig.Colors.Info}]{uiConfig.Prompts.ExitMessage}[/]");
                    return;
                }

                if (!string.IsNullOrEmpty(input) && converter.IsValidInput(input))
                {
                    try
                    {
                        // Viser ASCII-tabell
                        UIHelper.DisplayAsciiTable(input, uiConfig);

                        // Konverterer til ASCII-streng
                        string asciiDigits = converter.ConvertToAscii(input);

                        // Beregner sjekksiffer
                        int checkDigit = calculator.CalculateCheckDigit(asciiDigits);

                        // Viser resultatet
                        UIHelper.DisplayResults(asciiDigits, checkDigit, uiConfig);
                    }
                    catch (Exception ex)
                    {
                        // Viser en generell feilmelding dersom noe uforutsett skjer
                        AnsiConsole.MarkupLine($"[{uiConfig.Colors.Error}]En feil oppstod: {ex.Message}[/]");
                    }
                } 
                // Viser feilmelding dersom brukeren gir ugyldig eller tom input
                else
                {
                    AnsiConsole.MarkupLine($"[{uiConfig.Colors.Error}]{uiConfig.Prompts.InvalidInputError}[/]\n");
                }

            }
        }
    }
}
