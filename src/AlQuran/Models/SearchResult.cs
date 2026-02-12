namespace AlQuran.Models;

/// <summary>
/// Represents a search result when searching for text within the Quran.
/// </summary>
public sealed class SearchResult
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
    /// The full text of the matching Ayah.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// The Surah name (transliterated) where the match was found.
    /// </summary>
    public string SurahName { get; }

    /// <summary>
    /// The matched/highlighted portion of the text.
    /// </summary>
    public string MatchedText { get; }

    internal SearchResult(int surahNumber, int ayahNumber, string text, string surahName, string matchedText)
    {
        SurahNumber = surahNumber;
        AyahNumber = ayahNumber;
        Text = text;
        SurahName = surahName;
        MatchedText = matchedText;
    }

    /// <inheritdoc />
    public override string ToString() => $"[{SurahNumber}:{AyahNumber}] ({SurahName}) - {MatchedText}";
}
