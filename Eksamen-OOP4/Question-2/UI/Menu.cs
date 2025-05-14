using Question_2.Models;
using Spectre.Console;

namespace Question_2;

public class Menu
{
    private readonly List<(Applicant Applicant, Position MatchedPosition)> _matches;

    public Menu(List<(Applicant, Position)> matches)
    {
        _matches = matches;
    }

    public void Show()
    {
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
            
            // Tittelbanner
            var rule = new Rule("[bold green]MATCH-APP[/]")
                .Centered()
                .RuleStyle(Style.Parse("green"));
            
            AnsiConsole.Write(rule);
            AnsiConsole.WriteLine();
            
            var selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Velg et alternativ:")
                    .PageSize(10)
                    .HighlightStyle(new Style(Color.Green))
                    .WrapAround() // Gjør at man kan navigere fra topp til bunn og omvendt
                    .AddChoices(options));

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

    private void VisAlleSøkere()
    {
        List<Applicant> alleSøkere = _matches
            .Select(m => m.Applicant)
            .Distinct()
            .ToList();

        AnsiConsole.Clear();
        
        var table = new Table();
        table.Title = new TableTitle($"[bold]Viser {alleSøkere.Count} søkere[/]");
        table.Border(TableBorder.Rounded);
        // Setter bredde eksplisitt i stedet for å ekspandere
        table.Width = 80;
        // Legger til horisontal linje mellom hver rad
        // Bruker en bordtype som har skillelinjer
        table.Border(TableBorder.DoubleEdge);
        
        // Legg til kolonner
        table.AddColumn(new TableColumn("[green]Navn[/]").Centered());
        table.AddColumn(new TableColumn("[green]Ønsket stilling[/]").Centered());
        table.AddColumn(new TableColumn("[green]Spesialisering[/]").Centered());
        table.AddColumn(new TableColumn("[green]Ferdigheter[/]").Centered());

        foreach (Applicant applicant in alleSøkere)
        {
            Position desired = applicant.DesireedPosition;

            table.AddRow(
                $"[bold]{applicant.FirstName} {applicant.LastName}[/]",
                $"{desired.Title} ({desired.Seniority})",
                desired.Specialization,
                string.Join(", ", desired.Skills));
        }

        AnsiConsole.Write(table);
    }

    private void VisAlleMatchinger()
    {
        AnsiConsole.Clear();
        
        var table = new Table();
        table.Title = new TableTitle($"[bold]Totalt {_matches.Count} matcher[/]");
        table.Border(TableBorder.Rounded);
        // Setter bredde eksplisitt i stedet for å ekspandere
        table.Width = 80;
        // Legger til horisontal linje mellom hver rad
        // Bruker en bordtype som har skillelinjer
        table.Border(TableBorder.DoubleEdge);
        
        // Legg til kolonner
        table.AddColumn(new TableColumn("[green]Søker[/]").Centered());
        table.AddColumn(new TableColumn("[green]Matchet med stilling[/]").Centered());
        table.AddColumn(new TableColumn("[green]Seniority[/]").Centered());
        table.AddColumn(new TableColumn("[green]Spesialisering[/]").Centered());

        foreach ((Applicant applicant, Position position) in _matches)
        {
            table.AddRow(
                $"[bold]{applicant.FirstName} {applicant.LastName}[/]",
                position.Title,
                position.Seniority,
                position.Specialization);
        }

        AnsiConsole.Write(table);
    }

    private void VelgStillingstittel()
    {
        while (true)
        {
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

            titler.Add("Gå tilbake");

            AnsiConsole.Clear();
            var rule = new Rule("[bold green]Velg en stillingstittel[/]")
                .Centered()
                .RuleStyle(Style.Parse("green"));
            AnsiConsole.Write(rule);
            AnsiConsole.WriteLine();
            
            var valgtTittel = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Velg stillingstittel:")
                    .PageSize(10)
                    .HighlightStyle(new Style(Color.Green))
                    .WrapAround() // Gjør at man kan navigere fra topp til bunn og omvendt
                    .AddChoices(titler));

            if (valgtTittel == "Gå tilbake")
            {
                return;
            }

            List<(Applicant Applicant, Position MatchedPosition)> relevanteMatchinger = _matches
                .Where(m => m.MatchedPosition.Title.Equals(valgtTittel, StringComparison.OrdinalIgnoreCase))
                .ToList();

            AnsiConsole.Clear();
            
            var panel = new Panel($"[bold]Matchinger for stillingstittel \"{valgtTittel}\"[/]")
            {
                Border = BoxBorder.Rounded,
                Padding = new Padding(1, 0, 1, 0),
                // Begrenser bredden for å unngå for stor avstand
                Width = 80
            };
            AnsiConsole.Write(panel);

            if (relevanteMatchinger.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]Ingen matchinger funnet for denne tittelen.[/]");
            }
            else
            {
                var table = new Table();
                table.Border(TableBorder.Simple);
                // Setter bredde eksplisitt i stedet for å ekspandere
                table.Width = 76; // Litt mindre enn panel-bredden
                
                // Definerer kolonner først
                table.AddColumn(new TableColumn("[green]Søker[/]").Centered());
                table.AddColumn(new TableColumn("[green]Seniority[/]").Centered());
                table.AddColumn(new TableColumn("[green]Spesialisering[/]").Centered());
                
                // Legger til en tom rad for å skape litt avstand
                table.AddEmptyRow();

                foreach ((Applicant applicant, Position position) in relevanteMatchinger)
                {
                    table.AddRow(
                        $"[bold]{applicant.FirstName} {applicant.LastName}[/]",
                        position.Seniority,
                        position.Specialization);
                }
                
                AnsiConsole.Write(table);
            }

            var choices = new[] { "Velg ny stillingstittel", "Gå til hovedmeny" };
            var valg = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Hva vil du gjøre nå?")
                    .PageSize(10)
                    .HighlightStyle(new Style(Color.Green))
                    .WrapAround() // Gjør at man kan navigere fra topp til bunn og omvendt
                    .AddChoices(choices));

            if (valg == "Gå til hovedmeny")
            {
                return;
            }
        }
    }

    // Denne metoden er ikke lenger nødvendig da vi bruker Spectre.Console.SelectionPrompt i stedet
    // Beholdes for referanse, men brukes ikke
    private int ShowInteractiveMenu(List<string> options, string title)
    {
        AnsiConsole.Clear();
        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title)
                .PageSize(10)
                .HighlightStyle(new Style(Color.Green))
                .AddChoices(options));

        return options.IndexOf(selected);
    }
}
