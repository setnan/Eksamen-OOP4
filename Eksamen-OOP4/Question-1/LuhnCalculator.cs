namespace Question_1;

/// <summary>
/// Utfører Luhn-algoritmen for å beregne et sjekksiffer basert på en streng av ASCII-sifre.
/// </summary>
public class LuhnCalculator
{
    /// <summary>
    /// Beregner sjekksifferet ved hjelp av Luhn-algoritmen.
    /// </summary>
    /// <param name="asciiDigits">En streng med tall basert på ASCII-koder av input-tegn.</param>
    /// <returns>Et enkelt tall (0-9) som er sjekksifferet.</returns>
    public int CalculateCheckDigit(string asciiDigits)
    {
        int sum = 0;
        bool alternate = true;

        // Går gjennom sifrene baklengs, fra høyre mot venstre
        for (int i = asciiDigits.Length - 1; i >= 0; i--)
        {
            int digit = int.Parse(asciiDigits[i].ToString());

            // Dobler annenhver verdi, startende fra høyre
            if (alternate)
            {
                digit *= 2;

                // Hvis det dobbelte er > 9, trekk fra 9 (tilsvarer å summere sifrene)
                if (digit > 9)
                {
                    digit -= 9;
                }
            }

            sum += digit;
            alternate = !alternate; // Veksler mellom å doble og ikke
        }

        // Regner ut kontrollsifferet: hvor mye som trengs for å nå nærmeste ti-tall
        return (10 - (sum % 10)) % 10;
    }
}