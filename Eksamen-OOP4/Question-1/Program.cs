using System;
using Question_1;

namespace Question_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ASCII og Luhn Algoritme Kalkulator");
            Console.ResetColor();
            Console.Write("--------------------------------");
            Console.WriteLine(@"
Dette programmet konverterer tekst til ASCII-verdier og beregner et sjekksiffer
ved hjelp av Luhn-algoritmen.");
            Console.WriteLine();

            AsciiConverter converter = new AsciiConverter();
            LuhnCalculator calculator = new LuhnCalculator();

            while (true)
            {
                Console.Write("Skriv inn et ord eller navn (kun ASCII-tegn): ");
                string input = Console.ReadLine().Trim();

                if (!string.IsNullOrEmpty(input) && converter.IsValidInput(input))
                {
                    Console.WriteLine();
                    Console.WriteLine("ASCII-verdier: ");
                    
                    // Vis tegn og ASCII-verdi for hvert tegn
                    Console.WriteLine("Input\tBeskrivelse\tOutput");
                    foreach (char c in input)
                    {
                        string description = char.IsUpper(c) ? $"uppercase {c}" : $"lowercase {c}";
                        Console.WriteLine($"{c}\t{description}\t{(int)c}");
                    }
                    
                    // 1. Konverter til ASCII-streng
                    string asciiDigits = converter.ConvertToAscii(input);
                    Console.WriteLine();
                    Console.WriteLine($"Kombinert ASCII-verdi: {asciiDigits}");
                    
                    Console.WriteLine();
                    Console.WriteLine("Luhn-algoritmen Sjekksiffer: ");
                    
                    // 2. Beregner sjekksiffer
                    int checkDigit = calculator.CalculateCheckDigit(asciiDigits);
                    Console.WriteLine($"Sjekksiffer: {checkDigit}");
                    
                    // 3. Slår sammen og vis sluttresultat
                    string finalOutput = asciiDigits + checkDigit;
                    Console.WriteLine($"Ferdig streng med sjekksiffer: {finalOutput}");
                    
                    break;
                }

                Console.WriteLine("Ugyldig input. Vennligst bruk kun gyldige ASCII-tegn.");
                Console.WriteLine();
            }
            
            Console.WriteLine();
            Console.WriteLine("Trykk en tast for å avslutte...");
            Console.ReadKey();
        }
    }
}