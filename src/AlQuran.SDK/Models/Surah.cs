using AlQuran.SDK.Enums;

namespace AlQuran.SDK.Models;

/// <summary>
/// Represents a Surah (chapter) of the Holy Quran with complete metadata.
/// </summary>
public sealed class Surah
{
    /// <summary>
    /// The Surah number (1-114).
    /// </summary>
    public int Number { get; }

    /// <summary>
    /// The Arabic name of the Surah (e.g., "الفاتحة").
    /// </summary>
    public string ArabicName { get; }

    /// <summary>
    /// The transliterated name of the Surah (e.g., "Al-Fatiha").
    /// </summary>
    public string EnglishName { get; }

    /// <summary>
    /// The English meaning/translation of the Surah name (e.g., "The Opening").
    /// </summary>
    public string EnglishMeaning { get; }

    /// <summary>
    /// The total number of Ayahs (verses) in this Surah.
    /// </summary>
    public int AyahCount { get; }

    /// <summary>
    /// The place of revelation (Meccan or Medinan).
    /// </summary>
    public RevelationType RevelationType { get; }

    /// <summary>
    /// The chronological order of revelation (1-114).
    /// </summary>
    public int RevelationOrder { get; }

    /// <summary>
    /// The number of Rukus (sections) in this Surah.
    /// </summary>
    public int RukuCount { get; }

    /// <summary>
    /// The starting Juz number for this Surah.
    /// </summary>
    public int JuzStart { get; }

    /// <summary>
    /// The ending Juz number for this Surah.
    /// </summary>
    public int JuzEnd { get; }

    /// <summary>
    /// The page number in the standard Madani Mushaf where this Surah begins.
    /// </summary>
    public int PageStart { get; }

    /// <summary>
    /// Whether this Surah begins with Bismillah. All Surahs except At-Tawbah (9) begin with Bismillah.
    /// Note: Al-Fatiha's Bismillah is its first Ayah.
    /// </summary>
    public bool HasBismillah { get; }

    /// <summary>
    /// The corresponding <see cref="SurahName"/> enum value.
    /// </summary>
    public SurahName Name { get; }

    internal Surah(
        int number,
        string arabicName,
        string englishName,
        string englishMeaning,
        int ayahCount,
        RevelationType revelationType,
        int revelationOrder,
        int rukuCount,
        int juzStart,
        int juzEnd,
        int pageStart,
        bool hasBismillah)
    {
        Number = number;
        ArabicName = arabicName;
        EnglishName = englishName;
        EnglishMeaning = englishMeaning;
        AyahCount = ayahCount;
        RevelationType = revelationType;
        RevelationOrder = revelationOrder;
        RukuCount = rukuCount;
        JuzStart = juzStart;
        JuzEnd = juzEnd;
        PageStart = pageStart;
        HasBismillah = hasBismillah;
        Name = (SurahName)number;
    }

    /// <inheritdoc />
    public override string ToString() => $"{Number}. {EnglishName} ({ArabicName}) - {AyahCount} Ayahs";
}
