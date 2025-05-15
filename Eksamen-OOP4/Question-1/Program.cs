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

            // Vis header
            UIHelper.DisplayHeader(config.AppSettings);

            // Initialiserer konverterere
            AsciiConverter converter = new();
            LuhnCalculator calculator = new();

            // Hovedloop
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
                        // Vis ASCII-tabell
                        UIHelper.DisplayAsciiTable(input, uiConfig);

                        // Konverter til ASCII-streng
                        string asciiDigits = converter.ConvertToAscii(input);

                        // Beregn sjekksiffer
                        int checkDigit = calculator.CalculateCheckDigit(asciiDigits);

                        // Vis resultater
                        UIHelper.DisplayResults(asciiDigits, checkDigit, uiConfig);
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.MarkupLine($"[{uiConfig.Colors.Error}]En feil oppstod: {ex.Message}[/]");
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine($"[{uiConfig.Colors.Error}]{uiConfig.Prompts.InvalidInputError}[/]\n");
                }
            }
        }
    }
}
