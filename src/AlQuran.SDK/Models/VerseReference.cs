namespace AlQuran.SDK.Models;

/// <summary>
/// Represents a parsed Quran verse reference such as "2:255" or "2:1-5".
/// </summary>
public sealed class VerseReference
{
    /// <summary>
    /// The Surah (chapter) number (1-114).
    /// </summary>
    public int SurahNumber { get; }

    /// <summary>
    /// The starting Ayah (verse) number.
    /// </summary>
    public int AyahNumber { get; }

    /// <summary>
    /// The ending Ayah number for range references (e.g., "2:1-5" → EndAyahNumber = 5).
    /// Null for single-verse references.
    /// </summary>
    public int? EndAyahNumber { get; }

    /// <summary>
    /// Whether this reference is a range (e.g., "2:1-5") rather than a single verse.
    /// </summary>
    public bool IsRange => EndAyahNumber.HasValue;

    internal VerseReference(int surahNumber, int ayahNumber, int? endAyahNumber = null)
    {
        SurahNumber = surahNumber;
        AyahNumber = ayahNumber;
        EndAyahNumber = endAyahNumber;
    }

    /// <summary>
    /// Parses a string verse reference into a <see cref="VerseReference"/>.
    /// Supported formats: "2:255" (single verse), "2:1-5" (verse range).
    /// </summary>
    /// <param name="reference">The verse reference string.</param>
    /// <returns>A parsed <see cref="VerseReference"/>.</returns>
    /// <exception cref="FormatException">Thrown when the reference format is invalid.</exception>
    public static VerseReference Parse(string reference)
    {
        if (string.IsNullOrWhiteSpace(reference))
            throw new FormatException("Verse reference cannot be null or empty.");

        var parts = reference.Trim().Split(':');
        if (parts.Length != 2)
            throw new FormatException($"Invalid verse reference format: '{reference}'. Expected format: 'surah:ayah' or 'surah:start-end'.");

        if (!int.TryParse(parts[0], out int surah) || surah < 1 || surah > 114)
            throw new FormatException($"Invalid Surah number in reference: '{parts[0]}'. Valid range is 1-114.");

        var ayahPart = parts[1];
        if (ayahPart.IndexOf("-", StringComparison.Ordinal) >= 0)
        {
            var range = ayahPart.Split('-');
            if (range.Length != 2)
                throw new FormatException($"Invalid Ayah range: '{ayahPart}'. Expected format: 'start-end'.");

            if (!int.TryParse(range[0], out int start) || start < 1)
                throw new FormatException($"Invalid start Ayah number: '{range[0]}'.");

            if (!int.TryParse(range[1], out int end) || end < start)
                throw new FormatException($"Invalid end Ayah number: '{range[1]}'. Must be >= start Ayah.");

            return new VerseReference(surah, start, end);
        }

        if (!int.TryParse(ayahPart, out int ayah) || ayah < 1)
            throw new FormatException($"Invalid Ayah number: '{ayahPart}'.");

        return new VerseReference(surah, ayah);
    }

    /// <summary>
    /// Attempts to parse a string verse reference. Returns false if the format is invalid.
    /// </summary>
    /// <param name="reference">The verse reference string.</param>
    /// <param name="result">The parsed reference, or null if parsing failed.</param>
    /// <returns>True if parsing succeeded.</returns>
    public static bool TryParse(string reference, out VerseReference? result)
    {
        result = null;
        try
        {
            result = Parse(reference);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <inheritdoc />
    public override string ToString() =>
        IsRange ? $"{SurahNumber}:{AyahNumber}-{EndAyahNumber}" : $"{SurahNumber}:{AyahNumber}";
}
