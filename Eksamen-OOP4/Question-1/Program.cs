using System;
using Question_1;
using Spectre.Console;

namespace Question_1
{
    class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.Write(
                new Rule("[green]ASCII og Luhn Algoritme Kalkulator[/]")
                {
                    Justification = Justify.Left,
                    Style = Style.Parse("grey")
                });



            AnsiConsole.MarkupLine("Dette programmet [bold]konverterer[/] tekst til [italic]ASCII-verdier[/] og beregner et sjekksiffer ved hjelp av [underline]Luhn-algoritmen[/].\n");

            AsciiConverter converter = new AsciiConverter();
            LuhnCalculator calculator = new LuhnCalculator();

            while (true)
            {
                string input = AnsiConsole.Prompt(
                    new TextPrompt<string>("[bold]Skriv inn et ord eller navn (kun ASCII-tegn) eller tast Q for å avslutte:[/]")
                        .PromptStyle("green")
                        .Validate(text =>
                        {
                            return !string.IsNullOrWhiteSpace(text)
                                ? ValidationResult.Success()
                                : ValidationResult.Error("[red]Input kan ikke være tom[/]");
                        }));

                
                if (input.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    AnsiConsole.MarkupLine("\n[grey]Avslutter programmet...[/]");
                    return;
                }


                if (!string.IsNullOrEmpty(input) && converter.IsValidInput(input))
                {
                    AnsiConsole.WriteLine();
                    AnsiConsole.MarkupLine("[bold underline]ASCII-verdier:[/]");

                    // Tabell for tegn og ASCII-verdi
                    var table = new Table();
                    table.AddColumn("Input");
                    table.AddColumn("Beskrivelse");
                    table.AddColumn("Output");

                    foreach (char c in input)
                    {
                        string description = char.IsUpper(c) ? $"uppercase {c}" : $"lowercase {c}";
                        table.AddRow(c.ToString(), description, ((int)c).ToString());
                    }

                    AnsiConsole.Write(table);

                    // 1. Konverter til ASCII-streng
                    string asciiDigits = converter.ConvertToAscii(input);
                    AnsiConsole.WriteLine();
                    AnsiConsole.MarkupLine($"[bold]Kombinert ASCII-verdi:[/] [cyan]{asciiDigits}[/]");

                    // 2. Beregner sjekksiffer
                    int checkDigit = calculator.CalculateCheckDigit(asciiDigits);
                    AnsiConsole.MarkupLine($"[bold]Sjekksiffer (Luhn):[/] [green]{checkDigit}[/]");

                    // 3. Slår sammen og vis sluttresultat
                    string finalOutput = asciiDigits + checkDigit;
                    AnsiConsole.MarkupLine($"[bold]Ferdig streng med sjekksiffer:[/] [blue]{finalOutput}[/]\n");
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Ugyldig input.[/] Vennligst bruk kun gyldige ASCII-tegn.\n");
                }
            }

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[grey]Trykk en tast for å avslutte...[/]");
            Console.ReadKey();
        }
    }
}
