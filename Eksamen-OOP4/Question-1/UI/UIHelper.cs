using Question_1.Configuration;
using Spectre.Console;

namespace Question_1.UI;

public static class UIHelper
{
    public static void DisplayHeader(AppSettings settings)
    {
        AnsiConsole.Write(
            new Rule($"[green]{settings.AppName}[/]")
            {
                Justification = Justify.Left,
                Style = Style.Parse("grey")
            });

        AnsiConsole.MarkupLine($"{settings.AppDescription}\n");
    }

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

    public static void DisplayAsciiTable(string input, UISettings uiConfig)
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[bold underline]{uiConfig.Labels.AsciiValues}[/]");

        var table = new Table();
        table.AddColumn(uiConfig.TableHeaders.Input);
        table.AddColumn(uiConfig.TableHeaders.Description);
        table.AddColumn(uiConfig.TableHeaders.Output);

        foreach (char c in input)
        {
            string description = char.IsUpper(c) ? $"uppercase {c}" : $"lowercase {c}";
            table.AddRow(c.ToString(), description, ((int)c).ToString());
        }

        AnsiConsole.Write(table);
    }

    public static void DisplayResults(string asciiDigits, int checkDigit, UISettings uiConfig)
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[bold]{uiConfig.Labels.CombinedAscii}[/] [{uiConfig.Colors.Accent}]{asciiDigits}[/]");
        AnsiConsole.MarkupLine($"[bold]{uiConfig.Labels.Checksum}[/] [{uiConfig.Colors.Primary}]{checkDigit}[/]");

        string finalOutput = asciiDigits + checkDigit;
        AnsiConsole.MarkupLine($"[bold]{uiConfig.Labels.FinalOutput}[/] [{uiConfig.Colors.Secondary}]{finalOutput}[/]\n");
    }
}