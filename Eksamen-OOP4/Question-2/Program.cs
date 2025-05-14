using System;
using System.Threading.Tasks;
using Question_2.Services;
using Question_2.Models;
using Spectre.Console;

namespace Question_2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Setter både input og output encoding til UTF-8 for støtte av norske tegn (æøå)
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            // Setter konsolltittel
            Console.Title = "Søker/Stilling Matchmaking System";
            
            // Opprett en loading-indikator med pen spinner
            ExamTaskService service = new ExamTaskService();
            ExamData? data = null;
            
            await AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots)
                .SpinnerStyle(Style.Parse("green"))
                .StartAsync("Henter data fra API...", async ctx => 
                {
                    // Oppdater status-tekst mens vi henter data
                    ctx.Status("Kobler til API...");
                    await Task.Delay(500); // Liten forsinkelse for visuell effekt
                    
                    ctx.Status("Laster ned data...");
                    data = await service.GetExamDataAsync();
                    
                    ctx.Status("Behandler resultater...");
                    await Task.Delay(300); // Liten forsinkelse for visuell effekt
                });

            if (data == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"Feil ved henting av data fra API. 
Sjekk API-nøkkel, URL, modellstruktur og nettverkstilkobling.");
                Console.ResetColor();
                return;
            }

            // Visuell feedback under matching-prosessen
            MatchService matcher = new MatchService();
            List<(Applicant Applicant, Position MatchedPosition)> matches = null!;
            
            // Viser en spinner mens matching-algoritmen kjører
            await AnsiConsole.Status()
                .Spinner(Spinner.Known.Star)
                .SpinnerStyle(Style.Parse("yellow"))
                .StartAsync("Matcher søkere med stillinger...", async ctx => 
                {
                    // Viser antall søkere og stillinger
                    ctx.Status($"Analyserer {data.Applicants.Count} søkere og {data.Positions.Count} stillinger...");
                    await Task.Delay(300); // Kort forsinkelse for visuell effekt
                    
                    // Kjører matching-algoritmen
                    matches = matcher.MatchApplicantsToPositions(data.Applicants, data.Positions);
                    
                    // Viser hvor mange matcher vi fant
                    ctx.Status($"Fant {matches.Count} matcher!");
                    await Task.Delay(500); // Kort forsinkelse for visuell effekt
                });

            Menu meny = new Menu(matches);
            meny.Show();
        }
    }
}