using System.Text;

namespace Question_1;

/// <summary>
/// Konverterer tekst til ASCII-verdier representert som en streng av siffer.
/// </summary>
public class AsciiConverter
{
    /// <summary>
    /// Konverterer en streng til en sammenhengende streng av ASCII-verdier.
    /// Hver karakter i input-strengen omdannes til sin numeriske ASCII-verdi og legges til i resultatet.
    /// </summary>
    /// <param name="input">Tekststreng som skal konverteres.</param>
    /// <returns>En streng med tall som representerer ASCII-verdiene til hvert tegn.</returns>
    /// <exception cref="ArgumentException">Kastes dersom input er null eller tom.</exception>
    public string ConvertToAscii(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Input kan ikke være tom eller null", nameof(input));
        }

        StringBuilder asciiStringBuilder = new();
        foreach (char character in input)
        {
            asciiStringBuilder.Append((int)character);  // Konverter hvert tegn til ASCII-verdi og legg til i streng
        }

        string asciiString = asciiStringBuilder.ToString();
        Console.WriteLine($"ASCII verdi for '{input}': {asciiString}");
        return asciiString;
    }

    /// <summary>
    /// Validerer at input kun inneholder gyldige ASCII-tegn (maks 255).
    /// </summary>
    /// <param name="input">Teksten som skal valideres.</param>
    /// <returns>True hvis alle tegn i input er innenfor gyldig ASCII-område, ellers false.</returns>
    public bool IsValidInput(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return false;
        }

        foreach (char character in input)
        {
            if (character > 255)
            {
                return false;
            }
        }
        return true;
    }
}