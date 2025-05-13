using System.Text;

namespace Question_1;

/// <summary>
/// Konverterer tekst til ASCII-verdier representert som en streng av siffer.
/// </summary>
public class AsciiConverter
{
    /// <summary>
    /// Konverterer en tekststreng til dens korresponderende ASCII-verdier.
    /// </summary>
    /// <param name="input">Tekststrengen som skal konverteres</param>
    /// <returns>En streng som representerer ASCII-verdiene til hver karakter</returns>
    public string ConvertToAscii(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Input kan ikke være tom eller null", nameof(input));
        }

        StringBuilder asciiStringBuilder = new StringBuilder();
        foreach (char character in input)
        {
            asciiStringBuilder.Append((int)character);  
        }
        string asciiString = asciiStringBuilder.ToString();
        Console.WriteLine($"ASCII verdi for '{input}': {asciiString}");
        return asciiString;
    }

    /// <summary>
    /// Validerer at input består av gyldige ASCII-tegn (under 256).
    /// </summary>
    /// <param name="input">Tekststrengen som skal valideres</param>
    /// <returns>True hvis alle tegn er gyldige ASCII-tegn, ellers false</returns>
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