namespace AseanPhonebook;

/// <summary>Reusable, validated console input.</summary>
public static class InputHelper
{
    public static string ReadNonEmpty(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? text = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(text)) return text;
            Console.WriteLine("Input cannot be empty. Try again.");
        }
    }

    public static char ReadGender(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? text = Console.ReadLine()?.Trim().ToUpper();
            if (text == "M" || text == "F") return text[0];
            Console.WriteLine("Invalid gender. Enter M or F.");
        }
    }

    public static int ReadCountryCode(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine()?.Trim(), out int code) && CountryRegistry.IsValidCode(code))
                return code;
            Console.WriteLine("Invalid country code. Valid codes: 60, 62, 63, 65, 66.");
        }
    }

    public static string ReadDigits(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? text = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(text) && text.All(char.IsDigit)) return text;
            Console.WriteLine("Invalid input. Digits only.");
        }
    }

    public static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? text = Console.ReadLine()?.Trim().ToUpper();
            if (text == "Y") return true;
            if (text == "N") return false;
            Console.WriteLine("Please enter Y or N.");
        }
    }

    public static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine()?.Trim(), out int value)) return value;
            Console.WriteLine("Invalid input. Enter a number.");
        }
    }
}