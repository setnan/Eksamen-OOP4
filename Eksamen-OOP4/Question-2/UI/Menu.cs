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
    /// Inneholder tupler av søkere og stillinger som har blitt matchet av algoritmen.
    /// </summary>
    private readonly List<(Applicant Applicant, Position MatchedPosition)> _matches;
    
    /// <summary>
    /// Henter bredden på konsolvinduet eller returnerer en sikker standardverdi.
    /// </summary>
    /// <returns>Konsollbredde minus en sikkerhetmargin, eller standardverdi ved feil.</returns>
    private int GetConsoleWidth()
    {
        try
        {
            // Bruker faktisk konsollbredde, men reduserer med en margin på 5 tegn for sikkerhet
            int width = Console.WindowWidth - 5;
            
            // Sikrer at vi ikke går under minimumbredde eller over maksbredde
            if (width < 60) return 60; // Minimum bredde
            if (width > 150) return 150; // Maksimum bredde for lesbarhet
            
            return width;
        }
        catch
        {
            // Fallback hvis vi ikke kan lese konsollbredden
            return 80;
        }
    }

    /// <summary>
    /// Initialiserer en ny instans av menyhåndtereren med en liste over matchede søkere og stillinger.
    /// </summary>
    /// <param name="matches">Liste over tupler som inneholder søkere og deres matchede stillinger.</param>
    public Menu(List<(Applicant, Position)> matches)
    {
        _matches = matches;
    }

    /// <summary>
    /// Viser hovedmenyen og håndterer brukerens interaksjon med denne.
    /// Fungerer som en løkke som fortsetter å vise menyalternativer til brukeren velger å avslutte.
    /// </summary>
    public void Show()
    {
        // Definerer hovedmenyalternativene
        List<string> options = new()
        {
            "Vis alle søkere",
            "Vis alle matchinger",
            "Velg stillingstittel og se matchinger",
            "Avslutt"
        };

        while (true)
        {
            AnsiConsole.Clear();
            
            // Viser applikasjonens hovedtittel
            ShowMainTitle();

            // Gir brukeren mulighet til å velge menyalternativer med piltaster
            string selected = AnsiConsole.Prompt(
                CreateStandardMenu("Velg et alternativ:")
                    .AddChoices(options));

            // Utfører handling basert på brukerens valg
            // Legger til hver søker som en rad i tabellen med navn, ønsket stilling, spesialisering og ferdigheter
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
    /// Presenterer informasjonen i et strukturert tabellformat ved hjelp av Spectre.Console.
    /// </summary>
    private void ShowAllApplicants()
    {
        // Henter unike søkere fra matchingslisten 
        List<Applicant> allApplicants = _matches
            .Select(m => m.Applicant)
            .Distinct()
            .ToList();

        AnsiConsole.Clear();

        // Oppretter tabell for strukturert visning av søkerinformasjon
        Table table = CreateStandardTable($"Viser {allApplicants.Count} søkere");

        // Definerer kolonner for tabellen
        table.AddColumn(new TableColumn("[green]Navn[/]").Centered());
        table.AddColumn(new TableColumn("[green]Ønsket stilling[/]").Centered());
        table.AddColumn(new TableColumn("[green]Spesialisering[/]").Centered());
        table.AddColumn(new TableColumn("[green]Ferdigheter[/]").Centered());

        foreach (Applicant applicant in allApplicants)
        {
            Position desired = applicant.DesireedPosition;

            // Legger til rad for hver søker i tabellen
            table.AddRow(
                $"[bold]{applicant.FirstName} {applicant.LastName}[/]",
                $"{desired.Title} ({desired.Seniority})",
                desired.Specialization,
                FormatSkills(desired.Skills, 3)); // Viser maks 3 ferdigheter for å holde linjen kort
        }

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Viser en tabell med alle matchinger mellom søkere og stillinger.
    /// Presenterer informasjonen i et strukturert tabellformat ved hjelp av Spectre.Console.
    /// </summary>
    private void ShowAllMatches()
    {
        AnsiConsole.Clear();

        // Oppretter tabell for å vise alle matchinger mellom søkere og stillinger
        Table table = CreateStandardTable($"Totalt {_matches.Count} matcher");

        // Definerer kolonner for tabellen
        table.AddColumn(new TableColumn("[green]Søker[/]").Centered());
        table.AddColumn(new TableColumn("[green]Matchet med stilling[/]").Centered());
        table.AddColumn(new TableColumn("[green]Seniority[/]").Centered());
        table.AddColumn(new TableColumn("[green]Spesialisering[/]").Centered());

        foreach ((Applicant applicant, Position position) in _matches)
        {
            // Legger til rad for hver match i tabellen
            table.AddRow(
                $"[bold]{applicant.FirstName} {applicant.LastName}[/]",
                position.Title,
                position.Seniority,
                position.Specialization);
        }

        AnsiConsole.Write(table);
    }
    
    /// <summary>
    /// Formaterer en liste med ferdigheter til en kort, lesbar streng.
    /// </summary>
    /// <param name="skills">Listen med ferdigheter.</param>
    /// <param name="maxCount">Maksimalt antall ferdigheter som skal vises.</param>
    /// <returns>En formatert streng med ferdigheter, begrenset til angitt antall.</returns>
    private string FormatSkills(List<string> skills, int maxCount)
    {
        if (skills == null || skills.Count == 0)
            return "(Ingen)"; // Hvis ingen ferdigheter er angitt
            
        if (skills.Count <= maxCount)
            return string.Join(", ", skills); // Vis alle hvis det er færre enn maksimum
            
        // Hvis det er flere enn maksimum, vis kun de første og legg til ellipsis
        var visibleSkills = skills.Take(maxCount).ToList();
        return string.Join(", ", visibleSkills) + $" +{skills.Count - maxCount} flere";
    }
    
    /// <summary>
    /// Viser applikasjonens hovedtittel med FigletText for visuell effekt.
    /// </summary>
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
    /// Konfigurerer en tabell med standard formatering for konsistent visuell presentasjon.
    /// </summary>
    /// <param name="tittel">Tittelen som skal vises over tabellen.</param>
    /// <returns>En forhåndskonfigurert tabell klar til å legge til kolonner og rader.</returns>
    private Table CreateStandardTable(string tittel)
    {
        var table = new Table();
        table.Title = new TableTitle($"[bold]{tittel}[/]");
        table.Border(TableBorder.Rounded);
        table.Width = GetConsoleWidth();
        table.Border(TableBorder.DoubleEdge);
        return table;
    }
    
    /// <summary>
    /// Oppretter en standard konfigurasjon for valgmenyer med konsistent utseende.
    /// </summary>
    /// <param name="tittel">Spørsmålet eller tittelen som vises over valgalternativene.</param>
    /// <returns>En forhåndskonfigurert SelectionPrompt klar til å legge til valgalternativer.</returns>
    private SelectionPrompt<string> CreateStandardMenu(string tittel)
    {
        return new SelectionPrompt<string>()
            .Title($"[green]{tittel}[/]")
            .PageSize(10)
            .HighlightStyle(new Style(Color.Green))
            .WrapAround();
    }
    
    /// <summary>
    /// Viser overskriften for stillingstittel-skjermen med FigletText.
    /// </summary>
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
    /// Lar brukeren velge en stillingstittel og viser søkere som er matchet med stillinger med den valgte tittelen.
    /// Gir brukeren muligheten til å filtrere resultater og se detaljert informasjon om matchinger for en spesifikk stillingstittel.
    /// </summary>
    private void SelectJobTitle()
    {
        while (true)
        {
            // Henter alle unike stillingstitler for filtrering
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

            // Legger til mulighet for å gå tilbake til hovedmenyen
            titles.Add("Gå tilbake");

            AnsiConsole.Clear();
            
            // Viser stillingstittel-overskrift med FigletText for visuell konsistens
            ShowJobTitleHeader();

            // Lar brukeren velge stillingstittel fra listen
            var selectedTitle = AnsiConsole.Prompt(
                CreateStandardMenu("Velg stillingstittel:")
                    .AddChoices(titles));

            if (selectedTitle == "Gå tilbake")
            {
                return;
            }

            // Filtrerer match basert på valgt stillingstittel
            List<(Applicant Applicant, Position MatchedPosition)> relevanteMatchinger = _matches
                .Where(m => m.MatchedPosition.Title.Equals(selectedTitle, StringComparison.OrdinalIgnoreCase))
                .ToList();

            AnsiConsole.Clear();

            // Viser overskrift for den valgte stillingstittelen
            var panel = new Panel($"[bold]Matchinger for stillingstittel \"{selectedTitle}\"[/]")
            {
                Border = BoxBorder.Rounded,
                Padding = new Padding(1, 0, 1, 0),
                Width = GetConsoleWidth() // Dynamisk tilpasset panelbredde
            };
            AnsiConsole.Write(panel);

            if (relevanteMatchinger.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]Ingen match funnet for denne tittelen.[/]");
            }
            else
            {
                // Viser matchende søkere i tabellformat
                var table = new Table();
                table.Border(TableBorder.Simple);
                table.Width = GetConsoleWidth() - 4; // Litt mindre enn panelbredden for bedre visuell tilpasning

                table.AddColumn(new TableColumn("[green]Søker[/]").Centered());
                table.AddColumn(new TableColumn("[green]Seniority[/]").Centered());
                table.AddColumn(new TableColumn("[green]Spesialisering[/]").Centered());

                foreach ((Applicant applicant, Position position) in relevanteMatchinger)
                {
                    table.AddRow(
                        $"[bold]{applicant.FirstName} {applicant.LastName}[/]",
                        position.Seniority,
                        position.Specialization);
                }

                AnsiConsole.Write(table);
            }

            // Gir brukeren valg om å fortsette eller gå tilbake
            var choices = new[] { "Velg ny stillingstittel", "Gå til hovedmeny" };
            var selection = AnsiConsole.Prompt(
                CreateStandardMenu("Hva vil du gjøre nå?")
                    .AddChoices(choices));

            if (selection == "Gå til hovedmeny")
            {
                return;
            }
        }
    }
}