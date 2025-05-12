using System;
using Question_1;

namespace Question_1
{
    class Program
    {
        static void Main(string[] args)
        {
            AsciiConverter converter = new AsciiConverter();
            LuhnCalculator calculator = new LuhnCalculator();

            while (true)
            {
                Console.Write("Skriv inn et ord eller navn: ");
                string input = Console.ReadLine().Trim();

                if (!string.IsNullOrEmpty(input) && converter.IsValidInput(input))
                {
                    // 1. Konverter til ASCII-streng
                    string asciiDigits = converter.ConvertToAscii(input);

                    // 2. Beregn sjekksiffer
                    int checkDigit = calculator.CalculateCheckDigit(asciiDigits);
                    
                    // Console.WriteLine($"Sjekksiffer: {checkDigit}");
                    
                    // 3. Slå sammen og vis sluttresultat
                    string finalOutput = asciiDigits + checkDigit;
                    Console.WriteLine($"Ferdig streng med sjekksiffer: {finalOutput}");

                    break;
                }

                Console.WriteLine("Ugyldig input. Prøv igjen.");
            }
        }
    }
}