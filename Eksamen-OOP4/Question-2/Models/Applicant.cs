namespace Question_2.Models;

/// <summary>
/// Representerer en søker i systemet med personlig informasjon og ønsket stillingsposisjon.
/// </summary>
public class Applicant
{
    /// <summary>
    /// Får eller setter søkerens fornavn.
    /// </summary>
    public required string FirstName { get; set; }
    
    /// <summary>
    /// Får eller setter søkerens etternavn.
    /// </summary>
    public required string LastName { get; set; }
    
    /// <summary>
    /// Får eller setter søkerens ønskede stillingsposisjon.
    /// Inneholder detaljer om stillingstittel, spesialisering, senioritetsnivå og ønskede ferdigheter.
    /// </summary>
    public required Position DesireedPosition { get; set; }
}