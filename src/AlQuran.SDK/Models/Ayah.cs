namespace AlQuran.SDK.Models;

/// <summary>
/// Represents an Ayah (verse) of the Holy Quran.
/// </summary>
public sealed class Ayah
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
    /// The Arabic text of the Ayah.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// The Juz (part) number this Ayah belongs to (1-30).
    /// </summary>
    public int Juz { get; }

    /// <summary>
    /// The page number in the standard Madani Mushaf.
    /// </summary>
    public int Page { get; }

    /// <summary>
    /// The Hizb quarter this Ayah belongs to (1-240).
    /// </summary>
    public int HizbQuarter { get; }

    /// <summary>
    /// Whether this Ayah contains a Sajda (prostration) mark.
    /// </summary>
    public bool HasSajda { get; internal set; }

    internal Ayah(int surahNumber, int ayahNumber, string text, int juz = 0, int page = 0, int hizbQuarter = 0)
    {
        SurahNumber = surahNumber;
        AyahNumber = ayahNumber;
        Text = text;
        Juz = juz;
        Page = page;
        HizbQuarter = hizbQuarter;
    }

    /// <inheritdoc />
    public override string ToString() => $"[{SurahNumber}:{AyahNumber}] {(Text.Length > 50 ? Text.Substring(0, 50) + "..." : Text)}";
}
