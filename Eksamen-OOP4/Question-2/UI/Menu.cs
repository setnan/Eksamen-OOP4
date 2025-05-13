using Question_2.Models;

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
            int selected = ShowInteractiveMenu(options, "MATCH-APP");

            switch (selected)
            {
                case 0:
                    VisAlleSøkere();
                    Console.WriteLine("\nTrykk en tast for å gå tilbake til menyen...");
                    Console.ReadKey();
                    break;
                case 1:
                    VisAlleMatchinger();
                    Console.WriteLine("\nTrykk en tast for å gå tilbake til menyen...");
                    Console.ReadKey();
                    break;
                case 2:
                    VelgStillingstittel(); // Den håndterer alt selv!
                    break;
                case 3:
                    Console.WriteLine("Avslutter...");
                    return;
            }
        }
    }

    private void VisAlleSøkere()
    {
        var alleSøkere = _matches
            .Select(m => m.Applicant)
            .Distinct()
            .ToList();

        Console.Clear();
        Console.WriteLine($"Viser {alleSøkere.Count} søkere:\n");

        foreach (var applicant in alleSøkere)
        {
            var desired = applicant.DesireedPosition;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{applicant.FirstName} {applicant.LastName}");
            Console.ResetColor();
            Console.WriteLine($"  - Ønsket stilling: {desired.Title} ({desired.Seniority})");
            Console.WriteLine($"  - Spesialisering: {desired.Specialization}");
            Console.WriteLine($"  - Ferdigheter: {string.Join(", ", desired.Skills)}");
            Console.WriteLine();
        }
    }

    private void VisAlleMatchinger()
    {
        Console.Clear();
        Console.WriteLine($"Totalt {_matches.Count} matcher:\n");
        foreach (var (applicant, position) in _matches)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{applicant.FirstName} {applicant.LastName}");
            Console.ResetColor();
            Console.WriteLine($" → {position.Title} ({position.Seniority})");
        }
    }

    private void VelgStillingstittel()
    {
        while (true)
        {
            var titler = _matches
                .Select(m => m.MatchedPosition.Title)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(t => t)
                .ToList();

            if (titler.Count == 0)
            {
                Console.WriteLine("Ingen stillingstitler funnet.");
                Console.WriteLine("\nTrykk en tast for å gå tilbake...");
                Console.ReadKey();
                return;
            }

            titler.Add("Gå tilbake");

            int valgtIndex = ShowInteractiveMenu(titler, "Velg en stillingstittel");

            if (valgtIndex == titler.Count - 1)
            {
                // Brukeren valgte "Gå tilbake"
                return;
            }

            string valgtTittel = titler[valgtIndex];

            var relevanteMatchinger = _matches
                .Where(m => m.MatchedPosition.Title.Equals(valgtTittel, StringComparison.OrdinalIgnoreCase))
                .ToList();

            Console.Clear();
            Console.WriteLine($"\nMatchinger for stillingstittel \"{valgtTittel}\":\n");

            if (relevanteMatchinger.Count == 0)
            {
                Console.WriteLine("Ingen matchinger funnet for denne tittelen.");
            }
            else
            {
                foreach (var (applicant, position) in relevanteMatchinger)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{applicant.FirstName} {applicant.LastName}");
                    Console.ResetColor();
                    Console.WriteLine($" → {position.Title} ({position.Seniority})");
                }
            }

            Console.WriteLine("\nTrykk 'M' for hovedmeny, eller en annen tast for å velge ny stillingstittel...");
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.M)
            {
                return; 
            }
        }
    }




    private int ShowInteractiveMenu(List<string> options, string title)
    {
        int selectedIndex = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine(title);
            Console.WriteLine(new string('-', title.Length + 2));

            for (int i = 0; i < options.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("> ");
                }
                else
                {
                    Console.Write("  ");
                }

                Console.WriteLine(options[i]);
                Console.ResetColor();
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex - 1 + options.Count) % options.Count;
                    break;
                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex + 1) % options.Count;
                    break;
            }

        } while (key != ConsoleKey.Enter);

        return selectedIndex;
    }
}
