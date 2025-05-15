using System;
using System.Threading.Tasks;
using Question_2.Services;
using Question_2.Models;
using Spectre.Console;

namespace Question_2
{
    /// <summary>
    /// Startpunkt for applikasjonen som henter data fra et API og matcher søkere med stillinger.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Hovedmetoden som konfigurerer konsollvisningen, henter og validerer data, kjører matching og viser menyen.
        /// </summary>
        /// <param name="args">Kommandolinjeargumenter (ikke brukt).</param>
        static async Task Main(string[] args)
        {
            // Setter både input og output encoding til UTF-8 for støtte av norske tegn (æøå)
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            // Setter konsolltittel
            Console.Title = "JOB-MATCH - Søker/Stilling Matchmaking System";
            
            // Oppretter tjeneste for å hente data fra API
            ExamTaskService service = new();
            ExamData? data = null;
            
            // Viser spinner mens data hentes og behandles
            await AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots)
                .SpinnerStyle(Style.Parse("green"))
                .StartAsync("Henter data fra API...", async ctx => 
                {
                    ctx.Status("Kobler til API...");
                    await Task.Delay(500);

                    ctx.Status("Laster ned data...");
                    data = await service.GetExamDataAsync();

                    ctx.Status("Behandler resultater...");
                    await Task.Delay(300);
                });

            // Håndterer tilfelle hvor API ikke returnerer gyldige data
            if (data == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"Feil ved henting av data fra API. 
Sjekk API-nøkkel, URL, modellstruktur og nettverkstilkobling.");
                Console.ResetColor();
                return;
            }

            // Starter match-prosessen med visuell statusindikator
            MatchService matcher = new();
            List<(Applicant Applicant, Position MatchedPosition)> matches = null!;

            await AnsiConsole.Status()
                .Spinner(Spinner.Known.Star)
                .SpinnerStyle(Style.Parse("yellow"))
                .StartAsync("Matcher søkere med stillinger...", async ctx => 
                {
                    ctx.Status($"Analyserer {data.Applicants.Count} søkere og {data.Positions.Count} stillinger...");
                    await Task.Delay(300);

                    matches = matcher.MatchApplicantsToPositions(data.Applicants, data.Positions);

                    ctx.Status($"Fant {matches.Count} matcher!");
                    await Task.Delay(500);
                });

            // Viser hovedmenyen med interaktiv brukeropplevelse
            Menu meny = new(matches);
            meny.Show();
        }
    }
}
