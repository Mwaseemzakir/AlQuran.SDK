using System.Text;
using System.Text.RegularExpressions;

namespace AlQuran.Extensions;

/// <summary>
/// Extension methods for working with Arabic text, including tashkeel removal and normalization.
/// </summary>
public static class ArabicTextExtensions
{
    // Arabic diacritical marks (tashkeel) Unicode range
    private const char Fathatan = '\u064B';
    private const char Dammatan = '\u064C';
    private const char Kasratan = '\u064D';
    private const char Fatha = '\u064E';
    private const char Damma = '\u064F';
    private const char Kasra = '\u0650';
    private const char Shadda = '\u0651';
    private const char Sukun = '\u0652';
    private const char MaddahAbove = '\u0653';
    private const char HamzaAbove = '\u0654';
    private const char HamzaBelow = '\u0655';
    private const char Superscript = '\u0670';

    private static readonly Regex TashkeelRegex = new(
        @"[\u064B-\u0655\u0670\u06D6-\u06ED\u0610-\u061A\u06D6-\u06DC\u06DF-\u06E4\u06E7\u06E8\u06EA-\u06ED\uFE70-\uFE7F]",
        RegexOptions.Compiled);

    private static readonly Regex NormalizationRegex = new(
        @"[\u0622\u0623\u0625]",
        RegexOptions.Compiled);

    /// <summary>
    /// Removes all tashkeel (diacritical marks) from the Arabic text.
    /// </summary>
    /// <param name="input">The Arabic text with tashkeel.</param>
    /// <returns>The text without tashkeel.</returns>
    public static string RemoveTashkeel(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return TashkeelRegex.Replace(input, string.Empty);
    }

    /// <summary>
    /// Removes tashkeel from the Arabic text and provides an index mapping from the 
    /// normalized text positions back to the original text positions.
    /// </summary>
    /// <param name="input">The Arabic text with tashkeel.</param>
    /// <param name="charMap">Output dictionary mapping normalized index to original index.</param>
    /// <returns>The text without tashkeel.</returns>
    public static string RemoveTashkeelWithMapping(this string input, out Dictionary<int, int> charMap)
    {
        charMap = new Dictionary<int, int>();

        if (string.IsNullOrEmpty(input))
            return input;

        var sb = new StringBuilder(input.Length);
        int normalizedIndex = 0;

        for (int i = 0; i < input.Length; i++)
        {
            if (!IsTashkeelChar(input[i]))
            {
                charMap[normalizedIndex] = i;
                sb.Append(input[i]);
                normalizedIndex++;
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Normalizes Arabic text by replacing different forms of Alef and Hamza with plain Alef.
    /// Useful for more flexible text searching.
    /// </summary>
    /// <param name="input">The Arabic text to normalize.</param>
    /// <returns>Normalized Arabic text.</returns>
    public static string NormalizeAlef(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        // Replace Alef with Madda, Alef with Hamza Above, and Alef with Hamza Below with plain Alef
        return NormalizationRegex.Replace(input, "\u0627");
    }

    /// <summary>
    /// Normalizes Arabic text for searching by removing tashkeel and normalizing Alef forms.
    /// </summary>
    /// <param name="input">The Arabic text to normalize.</param>
    /// <returns>Normalized Arabic text suitable for comparison.</returns>
    public static string NormalizeForSearch(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return input.RemoveTashkeel().NormalizeAlef().Trim();
    }

    /// <summary>
    /// Highlights matched text within a string using a custom wrapper function.
    /// </summary>
    /// <param name="text">The full text to search within.</param>
    /// <param name="searchTerm">The term to highlight.</param>
    /// <param name="highlightWrapper">A function that wraps the matched text (e.g., s => $"&lt;b&gt;{s}&lt;/b&gt;").</param>
    /// <returns>The text with highlighted matches.</returns>
    public static string HighlightMatch(this string text, string searchTerm, Func<string, string> highlightWrapper)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(searchTerm) || highlightWrapper == null)
            return text;

        var normalizedText = text.NormalizeForSearch();
        var normalizedSearch = searchTerm.NormalizeForSearch();

        int index = normalizedText.IndexOf(normalizedSearch, StringComparison.OrdinalIgnoreCase);
        if (index < 0)
            return text;

        // Map back to original text positions
        var stripped = text.RemoveTashkeelWithMapping(out var charMap);

        if (charMap.ContainsKey(index) && charMap.ContainsKey(index + normalizedSearch.Length - 1))
        {
            int origStart = charMap[index];
            int origEnd = charMap[index + normalizedSearch.Length - 1];
            int origLength = origEnd - origStart + 1;

            var matched = text.Substring(origStart, origLength);
            return text.Substring(0, origStart) + highlightWrapper(matched) + text.Substring(origStart + origLength);
        }

        return text;
    }

    /// <summary>
    /// Checks whether a character is an Arabic tashkeel (diacritical mark) character.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is a tashkeel mark.</returns>
    public static bool IsTashkeelChar(char c)
    {
        return (c >= '\u064B' && c <= '\u0655') ||
               c == '\u0670' ||
               (c >= '\u06D6' && c <= '\u06ED') ||
               (c >= '\u0610' && c <= '\u061A') ||
               (c >= '\uFE70' && c <= '\uFE7F');
    }
}
