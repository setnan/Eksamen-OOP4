using System;
using Question_1;
using Question_1.Configuration;
using Question_1.UI;
using Spectre.Console;

namespace Question_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Last konfigurasjon
            var config = AppConfig.Load();
            var uiConfig = config.UI;
            
            // Vis header
            UIHelper.DisplayHeader(config.AppSettings);

            // Initialiser konverterere
            var converter = new AsciiConverter();
            var calculator = new LuhnCalculator();

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
                    // Vis ASCII-tabell
                    UIHelper.DisplayAsciiTable(input, uiConfig);

                    // Konverter til ASCII-streng
                    string asciiDigits = converter.ConvertToAscii(input);
                    
                    // Beregn sjekksiffer
                    int checkDigit = calculator.CalculateCheckDigit(asciiDigits);
                    
                    // Vis resultater
                    UIHelper.DisplayResults(asciiDigits, checkDigit, uiConfig);
                }
                else
                {
                    AnsiConsole.MarkupLine($"[{uiConfig.Colors.Error}]{uiConfig.Prompts.InvalidInputError}[/]\n");
                }
            }
        }
    }
}