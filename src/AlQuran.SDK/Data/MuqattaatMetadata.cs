using AlQuran.SDK.Models;

namespace AlQuran.SDK.Data;

/// <summary>
/// Contains metadata for the 29 Surahs that begin with Muqatta'at (disconnected/mysterious letters).
/// These are letter combinations at the beginning of certain Surahs whose exact meaning is known only to Allah.
/// </summary>
internal static class MuqattaatMetadata
{
    internal static readonly MuqattaatSurah[] AllMuqattaat =
    {
        new(2,  "Alif Lam Mim",           "الم"),
        new(3,  "Alif Lam Mim",           "الم"),
        new(7,  "Alif Lam Mim Sad",       "المص"),
        new(10, "Alif Lam Ra",            "الر"),
        new(11, "Alif Lam Ra",            "الر"),
        new(12, "Alif Lam Ra",            "الر"),
        new(13, "Alif Lam Mim Ra",        "المر"),
        new(14, "Alif Lam Ra",            "الر"),
        new(15, "Alif Lam Ra",            "الر"),
        new(19, "Kaf Ha Ya Ain Sad",      "كهيعص"),
        new(20, "Ta Ha",                  "طه"),
        new(26, "Ta Sin Mim",             "طسم"),
        new(27, "Ta Sin",                 "طس"),
        new(28, "Ta Sin Mim",             "طسم"),
        new(29, "Alif Lam Mim",           "الم"),
        new(30, "Alif Lam Mim",           "الم"),
        new(31, "Alif Lam Mim",           "الم"),
        new(32, "Alif Lam Mim",           "الم"),
        new(36, "Ya Sin",                 "يس"),
        new(38, "Sad",                    "ص"),
        new(40, "Ha Mim",                 "حم"),
        new(41, "Ha Mim",                 "حم"),
        new(42, "Ha Mim Ain Sin Qaf",     "حم عسق"),
        new(43, "Ha Mim",                 "حم"),
        new(44, "Ha Mim",                 "حم"),
        new(45, "Ha Mim",                 "حم"),
        new(46, "Ha Mim",                 "حم"),
        new(50, "Qaf",                    "ق"),
        new(68, "Nun",                    "ن"),
    };

    /// <summary>
    /// Total number of Surahs with Muqatta'at letters.
    /// </summary>
    internal const int TotalMuqattaatSurahs = 29;

    private static readonly Dictionary<int, MuqattaatSurah> Lookup =
        AllMuqattaat.ToDictionary(m => m.SurahNumber);

    internal static MuqattaatSurah? GetBySurah(int surahNumber)
    {
        Lookup.TryGetValue(surahNumber, out var result);
        return result;
    }

    internal static bool HasMuqattaat(int surahNumber) => Lookup.ContainsKey(surahNumber);
}
