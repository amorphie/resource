using System.Text;

namespace amorphie.resource.Helper;

public static class StringExtensions
{
    public static string? ToAscii(this string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }
        
        // Create a StringBuilder to store the ASCII converted value
        var asciiStringBuilder = new StringBuilder();

        // Loop through each character in the input string
        foreach (char c in value)
        {
            // Check if the character is an ASCII character
            if (c <= sbyte.MaxValue)
            {
                // If it is, append it to the StringBuilder
                asciiStringBuilder.Append(c);
            }
            else
            {
                // If it is not, replace it with a placeholder character or ignore it
                // Here we use '?' as a placeholder, but you can choose any character or omit this line
                asciiStringBuilder.Append('?');
            }
        }

        // Return the ASCII converted string
        return asciiStringBuilder.ToString();
    }
}
