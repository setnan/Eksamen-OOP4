namespace Question_1;

public class LuhnCalculator
{
    public int CalculateCheckDigit(string asciiDigits)
    {
        int sum = 0;
        bool alternate = true; // ← fiksen er her

        for (int i = asciiDigits.Length - 1; i >= 0; i--)
        {
            int digit = int.Parse(asciiDigits[i].ToString());

            if (alternate)
            {
                digit *= 2;
                if (digit > 9)
                {
                    digit -= 9;
                }
            }

            sum += digit;
            alternate = !alternate;
        }

        return (10 - (sum % 10)) % 10;
    }
}