using AlQuran.Enums;
using AlQuran.Models;

namespace AlQuran.Data;

/// <summary>
/// Contains metadata for all Sajda (prostration) positions in the Holy Quran.
/// </summary>
internal static class SajdaMetadata
{
    // 15 Sajda positions in the Quran
    // Scholars differ on some classifications; this uses the majority scholarly opinion.
    // Format: number, surahNumber, ayahNumber, type

    internal static readonly SajdaVerse[] AllSajdas =
    {
        new(1,  7,  206, SajdaType.Recommended),   // Al-A'raf
        new(2,  13, 15,  SajdaType.Recommended),   // Ar-Ra'd
        new(3,  16, 50,  SajdaType.Recommended),   // An-Nahl
        new(4,  17, 109, SajdaType.Recommended),   // Al-Isra
        new(5,  19, 58,  SajdaType.Recommended),   // Maryam
        new(6,  22, 18,  SajdaType.Recommended),   // Al-Hajj
        new(7,  22, 77,  SajdaType.Recommended),   // Al-Hajj (second sajda)
        new(8,  25, 60,  SajdaType.Recommended),   // Al-Furqan
        new(9,  27, 26,  SajdaType.Recommended),   // An-Naml
        new(10, 32, 15,  SajdaType.Obligatory),    // As-Sajdah
        new(11, 38, 24,  SajdaType.Recommended),   // Sad
        new(12, 41, 38,  SajdaType.Recommended),   // Fussilat
        new(13, 53, 62,  SajdaType.Recommended),   // An-Najm
        new(14, 84, 21,  SajdaType.Recommended),   // Al-Inshiqaq
        new(15, 96, 19,  SajdaType.Recommended),   // Al-'Alaq
    };

    /// <summary>
    /// Total number of Sajda positions in the Quran.
    /// </summary>
    internal const int TotalSajdas = 15;
}
