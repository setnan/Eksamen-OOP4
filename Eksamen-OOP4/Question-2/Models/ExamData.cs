namespace Question_2.Models;

/// <summary>
/// Container-klasse som inneholder alle data hentet fra API-et, inkludert lister med søkere og stillinger.
/// Denne klassen fungerer som hovedmodellen for applikasjonens data.
/// </summary>
public class ExamData
{
    /// <summary>
    /// Får eller setter listen over alle søkere i systemet.
    /// </summary>
    public required List<Applicant> Applicants { get; set; }
    
    /// <summary>
    /// Får eller setter listen over alle tilgjengelige stillingsposisjoner i systemet.
    /// </summary>
    public required List<Position> Positions { get; set; }
}