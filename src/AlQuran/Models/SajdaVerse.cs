using AlQuran.Enums;

namespace AlQuran.Models;

/// <summary>
/// Represents a Sajda (prostration) position in the Holy Quran.
/// </summary>
public sealed class SajdaVerse
{
    /// <summary>
    /// The ordinal number of this Sajda (1-15).
    /// </summary>
    public int Number { get; }

    /// <summary>
    /// The Surah number containing this Sajda.
    /// </summary>
    public int SurahNumber { get; }

    /// <summary>
    /// The Ayah number within the Surah where the Sajda occurs.
    /// </summary>
    public int AyahNumber { get; }

    /// <summary>
    /// The type of Sajda (Obligatory or Recommended).
    /// </summary>
    public SajdaType Type { get; }

    internal SajdaVerse(int number, int surahNumber, int ayahNumber, SajdaType type)
    {
        Number = number;
        SurahNumber = surahNumber;
        AyahNumber = ayahNumber;
        Type = type;
    }

    /// <inheritdoc />
    public override string ToString() => $"Sajda {Number}: [{SurahNumber}:{AyahNumber}] ({Type})";
}
