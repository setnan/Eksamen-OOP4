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

            // Viser visuell tittel for å tydeliggjøre brukergrensesnittet
            Rule rule = new Rule("[bold green]MATCH-APP[/]")
                .Centered()
                .RuleStyle(Style.Parse("green"));

            AnsiConsole.Write(rule);
            AnsiConsole.WriteLine();

            // Gir brukeren mulighet til å velge menyalternativer med piltaster
            string selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Velg et alternativ:[/]")
                    .PageSize(10)
                    .HighlightStyle(new Style(Color.Green))
                    .WrapAround() // Tillater navigasjon fra topp til bunn og omvendt
                    .AddChoices(options));

            // Utfører handling basert på brukerens valg
            // Legger til hver søker som en rad i tabellen med navn, ønsket stilling, spesialisering og ferdigheter
            switch (options.IndexOf(selected))
            {
                case 0:
                    VisAlleSøkere();
                    AnsiConsole.WriteLine("\nTrykk en tast for å gå tilbake til menyen...");
                    Console.ReadKey();
                    break;
                case 1:
                    VisAlleMatchinger();
                    AnsiConsole.WriteLine("\nTrykk en tast for å gå tilbake til menyen...");
                    Console.ReadKey();
                    break;
                case 2:
                    VelgStillingstittel();
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
    private void VisAlleSøkere()
    {
        // Henter unike søkere fra matchingslisten 
        List<Applicant> alleSøkere = _matches
            .Select(m => m.Applicant)
            .Distinct()
            .ToList();

        AnsiConsole.Clear();

        // Oppretter tabell for strukturert visning av søkerinformasjon
        Table table = new Table();
        table.Title = new TableTitle($"[bold]Viser {alleSøkere.Count} søkere[/]");
        table.Border(TableBorder.Rounded);
        table.Width = 80; // Tabellbredden er fast definert til 80 tegn
        table.Border(TableBorder.DoubleEdge);

        // Definerer kolonner for tabellen
        table.AddColumn(new TableColumn("[green]Navn[/]").Centered());
        table.AddColumn(new TableColumn("[green]Ønsket stilling[/]").Centered());
        table.AddColumn(new TableColumn("[green]Spesialisering[/]").Centered());
        table.AddColumn(new TableColumn("[green]Ferdigheter[/]").Centered());

        foreach (Applicant applicant in alleSøkere)
        {
            Position desired = applicant.DesireedPosition;

            // Legger til rad for hver søker i tabellen
            table.AddRow(
                $"[bold]{applicant.FirstName} {applicant.LastName}[/]",
                $"{desired.Title} ({desired.Seniority})",
                desired.Specialization,
                string.Join(", ", desired.Skills));
        }

        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Viser en tabell med alle matchinger mellom søkere og stillinger.
    /// Presenterer informasjonen i et strukturert tabellformat ved hjelp av Spectre.Console.
    /// </summary>
    private void VisAlleMatchinger()
    {
        AnsiConsole.Clear();

        // Oppretter tabell for å vise alle matchinger mellom søkere og stillinger
        Table table = new Table();
        table.Title = new TableTitle($"[bold]Totalt {_matches.Count} matcher[/]");
        table.Border(TableBorder.Rounded);
        table.Width = 80; // Fast tabellbredde for konsistent visning
        table.Border(TableBorder.DoubleEdge);

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
    /// Lar brukeren velge en stillingstittel og viser søkere som er matchet med stillinger med den valgte tittelen.
    /// Gir brukeren muligheten til å filtrere resultater og se detaljert informasjon om matchinger for en spesifikk stillingstittel.
    /// </summary>
    private void VelgStillingstittel()
    {
        while (true)
        {
            // Henter alle unike stillingstitler for filtrering
            List<string> titler = _matches
                .Select(m => m.MatchedPosition.Title)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(t => t)
                .ToList();

            if (titler.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]Ingen stillingstitler funnet.[/]");
                AnsiConsole.MarkupLine("\n[grey]Trykk en tast for å gå tilbake...[/]");
                Console.ReadKey();
                return;
            }

            // Legger til mulighet for å gå tilbake til hovedmenyen
            titler.Add("Gå tilbake");

            AnsiConsole.Clear();
            var rule = new Rule("[bold green]Velg en stillingstittel[/]")
                .Centered()
                .RuleStyle(Style.Parse("green"));
            AnsiConsole.Write(rule);
            AnsiConsole.WriteLine();

            // Lar brukeren velge stillingstittel fra listen
            var valgtTittel = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Velg stillingstittel:[/]")
                    .PageSize(10)
                    .HighlightStyle(new Style(Color.Green))
                    .WrapAround()
                    .AddChoices(titler));

            if (valgtTittel == "Gå tilbake")
            {
                return;
            }

            // Filtrerer match basert på valgt stillingstittel
            List<(Applicant Applicant, Position MatchedPosition)> relevanteMatchinger = _matches
                .Where(m => m.MatchedPosition.Title.Equals(valgtTittel, StringComparison.OrdinalIgnoreCase))
                .ToList();

            AnsiConsole.Clear();

            // Viser overskrift for den valgte stillingstittelen
            var panel = new Panel($"[bold]Matchinger for stillingstittel \"{valgtTittel}\"[/]")
            {
                Border = BoxBorder.Rounded,
                Padding = new Padding(1, 0, 1, 0),
                Width = 80 // Fast panelbredde for konsistent visning
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
                table.Width = 76; // Litt mindre enn panelbredden for bedre visuell tilpasning

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
            var valg = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Hva vil du gjøre nå?[/]")
                    .PageSize(10)
                    .HighlightStyle(new Style(Color.Green))
                    .WrapAround()
                    .AddChoices(choices));

            if (valg == "Gå til hovedmeny")
            {
                return;
            }
        }
    }
}