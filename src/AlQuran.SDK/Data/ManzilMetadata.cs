using AlQuran.SDK.Models;

namespace AlQuran.SDK.Data;

/// <summary>
/// Contains metadata for all 7 Manzils (weekly divisions) of the Holy Quran.
/// Reading one Manzil per day allows completion of the entire Quran in one week.
/// </summary>
internal static class ManzilMetadata
{
    // Manzil boundaries based on traditional scholarly division
    // Format: number, startSurah, startAyah, endSurah, endAyah

    internal static readonly Manzil[] AllManzils =
    {
        new(1, 1,  1,   4,  126),  // Al-Fatiha 1:1   to An-Nisa 4:126
        new(2, 4,  127, 9,  92),   // An-Nisa 4:127   to At-Tawbah 9:92
        new(3, 9,  93,  16, 128),  // At-Tawbah 9:93  to An-Nahl 16:128
        new(4, 17, 1,   25, 20),   // Al-Isra 17:1    to Al-Furqan 25:20
        new(5, 25, 21,  36, 27),   // Al-Furqan 25:21 to Ya-Sin 36:27
        new(6, 36, 28,  48, 29),   // Ya-Sin 36:28    to Al-Fath 48:29
        new(7, 49, 1,   114, 6),   // Al-Hujurat 49:1 to An-Nas 114:6
    };

    /// <summary>
    /// Total number of Manzils in the Quran.
    /// </summary>
    internal const int TotalManzils = 7;

    private static readonly Dictionary<int, Manzil> Lookup =
        AllManzils.ToDictionary(m => m.Number);

    internal static Manzil? GetByNumber(int number)
    {
        Lookup.TryGetValue(number, out var result);
        return result;
    }
}
