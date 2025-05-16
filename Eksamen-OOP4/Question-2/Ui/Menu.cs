using Question_2.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Question_2;

/// <summary>
/// Hovedklasse for brukergrensesnittet som håndterer visning av menyer og interaksjon med brukeren.
/// Benytter Spectre.Console for forbedret visuell presentasjon i terminalen.
/// </summary>
public class Menu
{
    /// <summary>
    /// Liste over matchede søkere og stillingsposisjoner som brukes i alle menymetoder.
    /// </summary>
    private readonly List<(Applicant Applicant, Position MatchedPosition)> _matches;

    /// <summary>
    /// Henter bredden på konsolvinduet eller returnerer en sikker standardverdi.
    /// </summary>
    private int GetConsoleWidth()
    {
        try
        {
            int width = Console.WindowWidth - 5; // Reduserer med en margin for sikkerhet
            
            if (width < 60) return 60; // Minimum bredde
            if (width > 150) return 150; // Maksimum bredde for lesbarhet

            return width;
        }
        catch
        {
            return 60; // Fallback hvis vi ikke kan lese konsollbredden
        }
    }

    /// <summary>
    /// Initialiserer en ny instans av menyhåndtereren med en liste over matchede søkere og stillinger.
    /// </summary>
    public Menu(List<(Applicant, Position)> matches)
    {
        _matches = matches;
    }

    /// <summary>
    /// Viser hovedmenyen og håndterer brukerens interaksjon med den.
    /// </summary>
    public void Show()
    {
        List<string> options = new()
        {
            "Vis alle søkere",
            "Vis alle matcher",
            "Velg stillingstittel og se matcher",
            "Avslutt"
        };

        while (true)
        {
            AnsiConsole.Clear();
            ShowMainTitle();

            string selected = AnsiConsole.Prompt(
                CreateStandardMenu("Velg et alternativ:")
                    .AddChoices(options));

            switch (options.IndexOf(selected))
            {
                case 0:
                    ShowAllApplicants();
                    AnsiConsole.WriteLine("\nTrykk en tast for å gå tilbake til menyen...");
                    Console.ReadKey();
                    break;
                case 1:
                    ShowAllMatches();
                    AnsiConsole.WriteLine("\nTrykk en tast for å gå tilbake til menyen...");
                    Console.ReadKey();
                    break;
                case 2:
                    SelectJobTitle();
                    break;
                case 3:
                    AnsiConsole.MarkupLine("[yellow]Avslutter...[/]");
                    return;
            }
        }
    }

    /// <summary>
    /// Viser en tabell med alle søkere i systemet og deres ønskede stillingsdetaljer.
    /// </summary>
    private void ShowAllApplicants()
    {
        List<Applicant> allApplicants = _matches
            .Select(m => m.Applicant)
            .Distinct()
            .ToList();

        AnsiConsole.Clear();
        Table table = CreateStandardTable($"Viser {allApplicants.Count} søkere");

        table.AddColumn(new TableColumn("[green]Navn[/]").Centered());
        table.AddColumn(new TableColumn("[green]Ønsket stilling[/]").Centered());
        table.AddColumn(new TableColumn("[green]Spesialisering[/]").Centered());
        table.AddColumn(new TableColumn("[green]Ferdigheter[/]").Centered());

        foreach (Applicant applicant in allApplicants)
        {
            Position disireed = applicant.DesireedPosition;

            table.AddRow(
                $"[bold]{applicant.FirstName} {applicant.LastName}[/]",
                $"{disireed.Title} ({disireed.Seniority})",
                disireed.Specialization,
                FormatSkills(disireed.Skills, 3)); // Begrenser til 3 ferdigheter for bedre lesbarhet
        }

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Viser en tabell med alle matcher mellom søkere og stillinger.
    /// </summary>
    private void ShowAllMatches()
    {
        AnsiConsole.Clear();
        Table table = CreateStandardTable($"Totalt {_matches.Count} matcher");

        table.AddColumn(new TableColumn("[green]Søker[/]").Centered());
        table.AddColumn(new TableColumn("[green]Matchet med stilling[/]").Centered());
        table.AddColumn(new TableColumn("[green]Seniority[/]").Centered());
        table.AddColumn(new TableColumn("[green]Spesialisering[/]").Centered());

        try
        {
            foreach ((Applicant applicant, Position position) in _matches)
            {
                string firstName = string.IsNullOrEmpty(applicant.FirstName) ? "[grey](Ikke angitt)[/]" : applicant.FirstName;
                string lastName = string.IsNullOrEmpty(applicant.LastName) ? "[grey](Ikke angitt)[/]" : applicant.LastName;
                string title = string.IsNullOrEmpty(position.Title) ? "[grey](Ikke angitt)[/]" : position.Title;
                string seniority = string.IsNullOrEmpty(position.Seniority) ? "[grey](Ikke angitt)[/]" : position.Seniority;
                string specialization = string.IsNullOrEmpty(position.Specialization) ? "[grey](Ikke angitt)[/]" : position.Specialization;

                table.AddRow(
                    $"[bold]{firstName} {lastName}[/]",
                    title,
                    seniority,
                    specialization);
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Feil ved visning av matcher: {ex.Message}[/]");
        }

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Formaterer en liste med ferdigheter til en kort, lesbar streng.
    /// </summary>
    private string FormatSkills(List<string> skills, int maxCount)
    {
        if (skills == null || skills.Count == 0)
            return "(Ingen)";

        if (skills.Count <= maxCount)
            return string.Join(", ", skills);

        List<string> visibleSkills = skills.Take(maxCount).ToList();
        return string.Join(", ", visibleSkills) + $" +{skills.Count - maxCount} flere";
    }

    private void ShowMainTitle()
    {
        AnsiConsole.Write(
            new FigletText("JOB-MATCH")
                .Color(Color.Green)
                .Centered());

        AnsiConsole.Write(
            new Rule()
                .Centered()
                .RuleStyle(Style.Parse("green")));
        AnsiConsole.WriteLine();
    }

    /// <summary>
    /// Konfigurerer en tabell med standard formatering for enhetlig visuell presentasjon.
    /// </summary>
    private Table CreateStandardTable(string tittel)
    {
        Table table = new Table();
        table.Title = new TableTitle($"[bold]{tittel}[/]");
        table.Border(TableBorder.Rounded);
        table.Width = GetConsoleWidth();
        table.Border(TableBorder.DoubleEdge);
        return table;
    }

    /// <summary>
    /// Oppretter en standard konfigurasjon for valgmenyer og konsistent utseende.
    /// </summary>
    private SelectionPrompt<string> CreateStandardMenu(string tittel)
    {
        return new SelectionPrompt<string>()
            .Title($"[green]{tittel}[/]")
            .PageSize(10)
            .HighlightStyle(new Style(Color.Green))
            .WrapAround();
    }

    private void ShowJobTitleHeader()
    {
        AnsiConsole.Write(
            new FigletText("Stillinger")
                .Color(Color.Green)
                .Centered());

        AnsiConsole.Write(
            new Rule()
                .Centered()
                .RuleStyle(Style.Parse("green")));
        AnsiConsole.WriteLine();
    }

    /// <summary>
    /// Lar brukeren velge en stillingstittel og viser matchende søkere for den valgte stillingen.
    /// </summary>
    private void SelectJobTitle()
    {
        while (true)
        {
            List<string> titles = _matches
                .Select(m => m.MatchedPosition.Title)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(t => t)
                .ToList();

            if (titles.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]Ingen stillingstitler funnet.[/]");
                AnsiConsole.MarkupLine("\n[grey]Trykk en tast for å gå tilbake...[/]");
                Console.ReadKey();
                return;
            }

            titles.Add("Gå tilbake");

            AnsiConsole.Clear();
            ShowJobTitleHeader();

            string selectedTitle = AnsiConsole.Prompt(
                CreateStandardMenu("Velg stillingstittel:")
                    .AddChoices(titles));

            if (selectedTitle == "Gå tilbake")
            {
                return;
            }

            List<(Applicant Applicant, Position MatchedPosition)> relevanteMatcher = new();
            try
            {
                relevanteMatcher = _matches
                    .Where(m => m.MatchedPosition?.Title != null &&
                               m.MatchedPosition.Title.Equals(selectedTitle, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Feil ved filtrering av matcher: {ex.Message}[/]");
            }

            AnsiConsole.Clear();

            Panel panel = new($"[bold]Matcher for stillingstittel \"{selectedTitle}\"[/]")
            {
                Border = BoxBorder.Rounded,
                Padding = new Padding(1, 0, 1, 0),
                Width = GetConsoleWidth()
            };
            AnsiConsole.Write(panel);

            if (relevanteMatcher.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]Ingen match funnet for denne tittelen.[/]");
            }
            else
            {
                Table table = new();
                table.Border(TableBorder.Simple);
                table.Width = GetConsoleWidth() - 4;

                table.AddColumn(new TableColumn("[green]Søker[/]").Centered());
                table.AddColumn(new TableColumn("[green]Seniority[/]").Centered());
                table.AddColumn(new TableColumn("[green]Spesialisering[/]").Centered());

                try
                {
                    foreach ((Applicant applicant, Position position) in relevanteMatcher)
                    {
                        string firstName = string.IsNullOrEmpty(applicant.FirstName) ? "[grey](Ikke angitt)[/]" : applicant.FirstName;
                        string lastName = string.IsNullOrEmpty(applicant.LastName) ? "[grey](Ikke angitt)[/]" : applicant.LastName;
                        string seniority = string.IsNullOrEmpty(position.Seniority) ? "[grey](Ikke angitt)[/]" : position.Seniority;
                        string specialization = string.IsNullOrEmpty(position.Specialization) ? "[grey](Ikke angitt)[/]" : position.Specialization;

                        table.AddRow(
                            $"[bold]{firstName} {lastName}[/]",
                            seniority,
                            specialization);
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]Feil ved visning av matcher: {ex.Message}[/]");
                }

                AnsiConsole.Write(table);
            }

            string[] choices = new[] { "Velg ny stillingstittel", "Gå til hovedmeny" };
            string selection = AnsiConsole.Prompt(
                CreateStandardMenu("Hva vil du gjøre nå?")
                    .AddChoices(choices));

            if (selection == "Gå til hovedmeny")
            {
                return;
            }
        }
    }
}