using AlQuran.SDK.Models;

namespace AlQuran.SDK.Data;

/// <summary>
/// Contains metadata for all 30 Juz (parts) of the Holy Quran.
/// </summary>
internal static class JuzMetadata
{
    // Juz boundaries based on the standard Madani Mushaf
    // Format: number, startSurah, startAyah, endSurah, endAyah, arabicName

    internal static readonly Juz[] AllJuz =
    {
        new(1,  1,  1,   2,  141, "الم"),
        new(2,  2,  142, 2,  252, "سيقول"),
        new(3,  2,  253, 3,  92,  "تلك الرسل"),
        new(4,  3,  93,  4,  23,  "لن تنالوا"),
        new(5,  4,  24,  4,  147, "والمحصنات"),
        new(6,  4,  148, 5,  81,  "لا يحب الله"),
        new(7,  5,  82,  6,  110, "وإذا سمعوا"),
        new(8,  6,  111, 7,  87,  "ولو أننا"),
        new(9,  7,  88,  8,  40,  "قال الملأ"),
        new(10, 8,  41,  9,  92,  "واعلموا"),
        new(11, 9,  93,  11, 5,   "يعتذرون"),
        new(12, 11, 6,   12, 52,  "وما من دابة"),
        new(13, 12, 53,  14, 52,  "وما أبرئ"),
        new(14, 15, 1,   16, 128, "ربما"),
        new(15, 17, 1,   18, 74,  "سبحان الذي"),
        new(16, 18, 75,  20, 135, "قال ألم"),
        new(17, 21, 1,   22, 78,  "اقترب للناس"),
        new(18, 23, 1,   25, 20,  "قد أفلح"),
        new(19, 25, 21,  27, 55,  "وقال الذين"),
        new(20, 27, 56,  29, 45,  "أمن خلق"),
        new(21, 29, 46,  33, 30,  "اتل ما أوحي"),
        new(22, 33, 31,  36, 27,  "ومن يقنت"),
        new(23, 36, 28,  39, 31,  "وما لي"),
        new(24, 39, 32,  41, 46,  "فمن أظلم"),
        new(25, 41, 47,  45, 37,  "إليه يرد"),
        new(26, 46, 1,   51, 30,  "حم"),
        new(27, 51, 31,  57, 29,  "قال فما خطبكم"),
        new(28, 58, 1,   66, 12,  "قد سمع"),
        new(29, 67, 1,   77, 50,  "تبارك الذي"),
        new(30, 78, 1,   114, 6,  "عم"),
    };

    /// <summary>
    /// Total number of Juz in the Quran.
    /// </summary>
    internal const int TotalJuz = 30;
}
