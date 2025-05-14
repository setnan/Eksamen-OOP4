using System;
using System.Threading.Tasks;
using Question_2.Services;
using Question_2.Models;

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
            
            Console.WriteLine("Henter data fra API...");

            ExamTaskService service = new ExamTaskService();
            ExamData? data = await service.GetExamDataAsync();

            if (data == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Klarte ikke hente data fra API.");
                Console.WriteLine("Tips: Sjekk at API-nøkkelen er riktig, at URLen er tilgjengelig, og at modellene matcher JSON-strukturen.");
                Console.ResetColor();
                return;
            }

            var matcher = new MatchService();
            var matches = matcher.MatchApplicantsToPositions(data.Applicants, data.Positions);

            var meny = new Menu(matches);
            meny.Show();
        }
    }
}