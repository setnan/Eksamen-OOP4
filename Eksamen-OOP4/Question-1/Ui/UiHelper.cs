using Question_1.Configuration;
using Spectre.Console;

namespace Question_1.Ui;

/// <summary>
/// Inneholder metoder for å vise og hente informasjon i konsollgrensesnittet.
/// </summary>
public static class UiHelper
{
    /// <summary>
    /// Viser applikasjonstittel og beskrivelse med tilpasset format.
    /// </summary>
    /// <param name="settings">AppSettings med tittel og beskrivelse.</param>
    public static void DisplayHeader(AppSettings settings)
    {
        AnsiConsole.WriteLine(new string('_', settings.AppName.Length + 10));
        AnsiConsole.MarkupLine($"[bold green]{settings.AppName.Trim()}[/]");
        AnsiConsole.WriteLine(new string('_', settings.AppName.Length + 10));

        foreach (string line in settings.AppDescription.Split('\n'))
        {
            AnsiConsole.MarkupLine($"[grey]{line.Trim()}[/]");
        }

        AnsiConsole.WriteLine();
    }

    /// <summary>
    /// Ber brukeren om input med tilpasset farge og validering.
    /// </summary>
    /// <param name="prompts">Tekstmeldinger til bruker.</param>
    /// <param name="colors">Farger brukt i konsollen.</param>
    /// <returns>En validerte tekststreng fra bruker.</returns>
    public static string GetUserInput(PromptSettings prompts, ColorSettings colors)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>($"[bold]{prompts.InputPrompt}[/]")
                .PromptStyle(colors.Primary)
                .Validate(text =>
                {
                    return !string.IsNullOrWhiteSpace(text)
                        ? ValidationResult.Success()
                        : ValidationResult.Error($"[{colors.Error}]{prompts.EmptyInputError}[/]");
                }));
    }

    /// <summary>
    /// Viser en ASCII-tabell med tegn, beskrivelse og ASCII-verdier.
    /// </summary>
    /// <param name="input">Teksten brukeren skrev inn.</param>
    /// <param name="uiConfig">Ui-innstillinger for etiketter og farger.</param>
    public static void DisplayAsciiTable(string input, UiSettings uiConfig)
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[bold underline]{uiConfig.Labels.AsciiValues}[/]");

        Table table = new();
        table.AddColumn(uiConfig.TableHeaders.Input);
        table.AddColumn(uiConfig.TableHeaders.Description);
        table.AddColumn(uiConfig.TableHeaders.Output);

        foreach (char character in input)
        {
            string description = char.IsUpper(character) ? $"uppercase {character}" : $"lowercase {character}";
            table.AddRow(character.ToString(), description, ((int)character).ToString());
        }

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Viser resultatene av konvertering og Luhn-beregning.
    /// </summary>
    /// <param name="asciiDigits">Kombinert ASCII-streng.</param>
    /// <param name="checkDigit">Beregnet kontrollsiffer.</param>
    /// <param name="uiConfig">Ui-innstillinger for etiketter og farger.</param>
    public static void DisplayResults(string asciiDigits, int checkDigit, UiSettings uiConfig)
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[bold]{uiConfig.Labels.CombinedAscii}[/] [{uiConfig.Colors.Accent}]{asciiDigits}[/]");
        AnsiConsole.MarkupLine($"[bold]{uiConfig.Labels.Checksum}[/] [{uiConfig.Colors.Primary}]{checkDigit}[/]");

        string finalOutput = asciiDigits + checkDigit;
        AnsiConsole.MarkupLine($"[bold]{uiConfig.Labels.FinalOutput}[/] [{uiConfig.Colors.Secondary}]{finalOutput}[/]\n");
    }
}