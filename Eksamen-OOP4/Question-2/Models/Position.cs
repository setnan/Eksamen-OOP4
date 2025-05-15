namespace Question_2.Models;

/// <summary>
/// Representerer en stillingsposisjon med egenskaper som tittel, spesialisering, senioritetsnivå, og ferdigheter.
/// Brukes både for søkernes ønskede stillinger og faktiske ledige stillinger.
/// </summary>
public class Position
{
    /// <summary>
    /// Får eller setter stillingstittelen (f.eks. "Utvikler", "Prosjektleder").
    /// </summary>
    public required string Title { get; set; }
    
    /// <summary>
    /// Får eller setter stillingsposisjons spesialisering (f.eks. "Frontend", "Backend", "Fullstack").
    /// </summary>
    public required string Specialization { get; set; }
    
    /// <summary>
    /// Får eller setter stillingsposisjons senioritetsnivå (f.eks. "Junior", "Senior", "Lead").
    /// </summary>
    public required string Seniority { get; set; }
    
    /// <summary>
    /// Får eller setter listen over ferdigheter som er assosiert med stillingen.
    /// </summary>
    public required List<string> Skills { get; set; }
}