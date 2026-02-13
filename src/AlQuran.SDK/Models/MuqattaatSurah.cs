namespace AlQuran.SDK.Models;

/// <summary>
/// Represents a Surah that begins with Muqatta'at (disconnected/mysterious letters).
/// 29 Surahs in the Quran begin with these letter combinations.
/// </summary>
public sealed class MuqattaatSurah
{
    /// <summary>
    /// The Surah number (e.g., 2 for Al-Baqarah).
    /// </summary>
    public int SurahNumber { get; }

    /// <summary>
    /// The English transliteration of the letters (e.g., "Alif Lam Mim").
    /// </summary>
    public string Letters { get; }

    /// <summary>
    /// The Arabic representation of the letters (e.g., "الم").
    /// </summary>
    public string ArabicLetters { get; }

    internal MuqattaatSurah(int surahNumber, string letters, string arabicLetters)
    {
        SurahNumber = surahNumber;
        Letters = letters;
        ArabicLetters = arabicLetters;
    }

    /// <inheritdoc />
    public override string ToString() => $"Surah {SurahNumber}: {ArabicLetters} ({Letters})";
}
