using AlQuran.SDK.Enums;

namespace AlQuran.SDK.Models;

/// <summary>
/// Represents a search result when searching text within a Quran translation.
/// </summary>
public sealed class TranslationSearchResult
{
    /// <summary>
    /// The Surah number where the match was found.
    /// </summary>
    public int SurahNumber { get; }

    /// <summary>
    /// The Ayah number where the match was found.
    /// </summary>
    public int AyahNumber { get; }

    /// <summary>
    /// The full translated text of the matching Ayah.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// The Surah name (transliterated) where the match was found.
    /// </summary>
    public string SurahName { get; }

    /// <summary>
    /// The search term that was matched.
    /// </summary>
    public string MatchedText { get; }

    /// <summary>
    /// The translation edition the result came from.
    /// </summary>
    public TranslationEdition Edition { get; }

    internal TranslationSearchResult(int surahNumber, int ayahNumber, string text, string surahName, string matchedText, TranslationEdition edition)
    {
        SurahNumber = surahNumber;
        AyahNumber = ayahNumber;
        Text = text;
        SurahName = surahName;
        MatchedText = matchedText;
        Edition = edition;
    }

    /// <inheritdoc />
    public override string ToString() => $"[{SurahNumber}:{AyahNumber}] ({SurahName}) - {MatchedText} [{Edition}]";
}
