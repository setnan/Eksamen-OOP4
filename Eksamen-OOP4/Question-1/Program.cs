using System;
using Question_1;
using Question_1.Configuration;
using Question_1.Ui;
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
            AppConfig config = AppConfig.Load();
            UiSettings uiConfig = config.Ui;
            
            UiHelper.DisplayHeader(config.AppSettings);
            
            AsciiConverter converter = new();
            LuhnCalculator calculator = new();
            
            while (true)
            {
                string input = UiHelper.GetUserInput(uiConfig.Prompts, uiConfig.Colors);

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
                        UiHelper.DisplayAsciiTable(input, uiConfig);

                        // Konverter input og beregn sjekksiffer
                        string asciiDigits = converter.ConvertToAscii(input);
                        int checkDigit = calculator.CalculateCheckDigit(asciiDigits);

                        // Viser resultatet
                        UiHelper.DisplayResults(asciiDigits, checkDigit, uiConfig);
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
