namespace AlQuran.Models;

/// <summary>
/// Represents a Juz (part/para) of the Holy Quran. The Quran is divided into 30 Juz.
/// </summary>
public sealed class Juz
{
    /// <summary>
    /// The Juz number (1-30).
    /// </summary>
    public int Number { get; }

    /// <summary>
    /// The Surah number where this Juz starts.
    /// </summary>
    public int StartSurah { get; }

    /// <summary>
    /// The Ayah number within the start Surah where this Juz starts.
    /// </summary>
    public int StartAyah { get; }

    /// <summary>
    /// The Surah number where this Juz ends.
    /// </summary>
    public int EndSurah { get; }

    /// <summary>
    /// The Ayah number within the end Surah where this Juz ends.
    /// </summary>
    public int EndAyah { get; }

    /// <summary>
    /// The Arabic name of this Juz (first words of the Juz).
    /// </summary>
    public string ArabicName { get; }

    internal Juz(int number, int startSurah, int startAyah, int endSurah, int endAyah, string arabicName)
    {
        Number = number;
        StartSurah = startSurah;
        StartAyah = startAyah;
        EndSurah = endSurah;
        EndAyah = endAyah;
        ArabicName = arabicName;
    }

    /// <inheritdoc />
    public override string ToString() => $"Juz {Number}: {ArabicName} ({StartSurah}:{StartAyah} - {EndSurah}:{EndAyah})";
}
