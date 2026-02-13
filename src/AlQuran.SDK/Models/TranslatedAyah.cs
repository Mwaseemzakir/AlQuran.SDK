using AlQuran.SDK.Enums;

namespace AlQuran.SDK.Models;

/// <summary>
/// Represents a translated Ayah (verse) from a specific translation edition.
/// </summary>
public sealed class TranslatedAyah
{
    /// <summary>
    /// The Surah (chapter) number this Ayah belongs to (1-114).
    /// </summary>
    public int SurahNumber { get; }

    /// <summary>
    /// The Ayah (verse) number within the Surah.
    /// </summary>
    public int AyahNumber { get; }

    /// <summary>
    /// The translated text of the Ayah.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// The translation edition this text comes from.
    /// </summary>
    public TranslationEdition Edition { get; }

    internal TranslatedAyah(int surahNumber, int ayahNumber, string text, TranslationEdition edition)
    {
        SurahNumber = surahNumber;
        AyahNumber = ayahNumber;
        Text = text;
        Edition = edition;
    }

    /// <inheritdoc />
    public override string ToString() => $"[{SurahNumber}:{AyahNumber}] {(Text.Length > 80 ? Text.Substring(0, 80) + "..." : Text)}";
}
