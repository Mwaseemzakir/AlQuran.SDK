namespace AlQuran.SDK.Models;

/// <summary>
/// Represents a Manzil (one of 7 equal divisions) of the Holy Quran.
/// Reading one Manzil per day allows completion of the Quran in one week.
/// </summary>
public sealed class Manzil
{
    /// <summary>
    /// The Manzil number (1-7).
    /// </summary>
    public int Number { get; }

    /// <summary>
    /// The Surah number where this Manzil starts.
    /// </summary>
    public int StartSurah { get; }

    /// <summary>
    /// The Ayah number within the start Surah where this Manzil starts.
    /// </summary>
    public int StartAyah { get; }

    /// <summary>
    /// The Surah number where this Manzil ends.
    /// </summary>
    public int EndSurah { get; }

    /// <summary>
    /// The Ayah number within the end Surah where this Manzil ends.
    /// </summary>
    public int EndAyah { get; }

    internal Manzil(int number, int startSurah, int startAyah, int endSurah, int endAyah)
    {
        Number = number;
        StartSurah = startSurah;
        StartAyah = startAyah;
        EndSurah = endSurah;
        EndAyah = endAyah;
    }

    /// <inheritdoc />
    public override string ToString() => $"Manzil {Number}: ({StartSurah}:{StartAyah} - {EndSurah}:{EndAyah})";
}
