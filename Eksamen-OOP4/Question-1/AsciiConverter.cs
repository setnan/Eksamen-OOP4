using System.Text;

namespace Question_1;

public class AsciiConverter
{
    public string ConvertToAscii(string input)
    {
        StringBuilder asciiStringBuilder = new StringBuilder();
        foreach (char character in input)
        {
            asciiStringBuilder.Append((int)character);  
        }
        string asciiString = asciiStringBuilder.ToString();
        Console.WriteLine($"ASCII verdi for '{input}': {asciiString}");
        return asciiString;
    }

    public bool IsValidInput(string input)
    {
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