using AlQuran.SDK.Data;
using AlQuran.SDK.Enums;
using AlQuran.SDK.Exceptions;
using AlQuran.SDK.Extensions;
using AlQuran.SDK.Models;

namespace AlQuran.SDK;

/// <summary>
/// The main entry point for accessing the Holy Quran data.
/// Provides static methods for retrieving Surahs, Ayahs, Juz, Sajda positions, and performing searches.
/// </summary>
public static class Quran
{
    #region Lookup Maps

    private static readonly Lazy<Dictionary<int, Surah>> SurahByNumber = new(
        () => SurahMetadata.AllSurahs.ToDictionary(s => s.Number), isThreadSafe: true);

    private static readonly Lazy<Dictionary<string, Surah>> SurahByEnglishName = new(
        () => SurahMetadata.AllSurahs.ToDictionary(s => s.EnglishName.ToLowerInvariant()), isThreadSafe: true);

    private static readonly Lazy<Dictionary<string, Surah>> SurahByArabicName = new(
        () => SurahMetadata.AllSurahs.ToDictionary(s => s.ArabicName), isThreadSafe: true);

    private static readonly Lazy<Dictionary<int, Juz>> JuzByNumber = new(
        () => JuzMetadata.AllJuz.ToDictionary(j => j.Number), isThreadSafe: true);

    private static readonly Lazy<Dictionary<string, SajdaVerse>> SajdaByKey = new(
        () => SajdaMetadata.AllSajdas.ToDictionary(s => $"{s.SurahNumber}:{s.AyahNumber}"), isThreadSafe: true);

    #endregion

    #region Constants

    /// <summary>
    /// The total number of Surahs (chapters) in the Quran: 114.
    /// </summary>
    public const int TotalSurahs = 114;

    /// <summary>
    /// The total number of Ayahs (verses) in the Quran: 6236.
    /// </summary>
    public const int TotalAyahs = 6236;

    /// <summary>
    /// The total number of Juz (parts) in the Quran: 30.
    /// </summary>
    public const int TotalJuz = 30;

    /// <summary>
    /// The total number of Sajda (prostration) positions in the Quran: 15.
    /// </summary>
    public const int TotalSajdas = 15;

    #endregion

    #region Bismillah

    /// <summary>
    /// Gets the Bismillah text ("In the name of Allah, the Most Gracious, the Most Merciful") 
    /// in the specified script type.
    /// </summary>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>The Bismillah text.</returns>
    public static string GetBismillah(ScriptType scriptType = ScriptType.Uthmani)
    {
        return scriptType == ScriptType.Uthmani
            ? SurahMetadata.BismillahUthmani
            : SurahMetadata.BismillahSimple;
    }

    #endregion

    #region Surah Methods

    /// <summary>
    /// Gets a Surah by its number (1-114).
    /// </summary>
    /// <param name="number">The Surah number.</param>
    /// <returns>The Surah metadata.</returns>
    /// <exception cref="SurahNotFoundException">Thrown when the Surah number is invalid.</exception>
    public static Surah GetSurah(int number)
    {
        if (SurahByNumber.Value.TryGetValue(number, out var surah))
            return surah;

        throw new SurahNotFoundException($"Surah with number {number} was not found. Valid range is 1-114.");
    }

    /// <summary>
    /// Gets a Surah by its <see cref="SurahName"/> enum value.
    /// </summary>
    /// <param name="name">The Surah name enum.</param>
    /// <returns>The Surah metadata.</returns>
    /// <exception cref="SurahNotFoundException">Thrown when the Surah name is invalid.</exception>
    public static Surah GetSurah(SurahName name)
    {
        return GetSurah((int)name);
    }

    /// <summary>
    /// Gets a Surah by its English (transliterated) name. Case-insensitive.
    /// </summary>
    /// <param name="englishName">The English name of the Surah (e.g., "Al-Fatiha").</param>
    /// <returns>The Surah metadata.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the name is null or whitespace.</exception>
    /// <exception cref="SurahNotFoundException">Thrown when no Surah with the given name is found.</exception>
    public static Surah GetSurah(string englishName)
    {
        if (string.IsNullOrWhiteSpace(englishName))
            throw new ArgumentNullException(nameof(englishName));

        if (SurahByEnglishName.Value.TryGetValue(englishName.Trim().ToLowerInvariant(), out var surah))
            return surah;

        throw new SurahNotFoundException($"Surah with name '{englishName}' was not found.");
    }

    /// <summary>
    /// Gets a Surah by its number, or null if not found.
    /// </summary>
    /// <param name="number">The Surah number.</param>
    /// <returns>The Surah metadata, or null if not found.</returns>
    public static Surah? GetSurahOrDefault(int number)
    {
        SurahByNumber.Value.TryGetValue(number, out var surah);
        return surah;
    }

    /// <summary>
    /// Gets a Surah by its English name, or null if not found.
    /// </summary>
    /// <param name="englishName">The English name of the Surah.</param>
    /// <returns>The Surah metadata, or null if not found.</returns>
    public static Surah? GetSurahOrDefault(string englishName)
    {
        if (string.IsNullOrWhiteSpace(englishName))
            return null;

        SurahByEnglishName.Value.TryGetValue(englishName.Trim().ToLowerInvariant(), out var surah);
        return surah;
    }

    /// <summary>
    /// Gets a Surah by its Arabic name.
    /// </summary>
    /// <param name="arabicName">The Arabic name of the Surah (e.g., "الفاتحة").</param>
    /// <returns>The Surah metadata.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the name is null or whitespace.</exception>
    /// <exception cref="SurahNotFoundException">Thrown when no Surah with the given Arabic name is found.</exception>
    public static Surah GetSurahByArabicName(string arabicName)
    {
        if (string.IsNullOrWhiteSpace(arabicName))
            throw new ArgumentNullException(nameof(arabicName));

        if (SurahByArabicName.Value.TryGetValue(arabicName.Trim(), out var surah))
            return surah;

        throw new SurahNotFoundException($"Surah with Arabic name '{arabicName}' was not found.");
    }

    /// <summary>
    /// Gets all 114 Surahs of the Quran.
    /// </summary>
    /// <returns>A list of all Surahs.</returns>
    public static List<Surah> GetAllSurahs()
    {
        return new List<Surah>(SurahMetadata.AllSurahs);
    }

    /// <summary>
    /// Gets all Meccan Surahs.
    /// </summary>
    /// <returns>A list of Meccan Surahs.</returns>
    public static List<Surah> GetMeccanSurahs()
    {
        return SurahMetadata.AllSurahs.Where(s => s.RevelationType == RevelationType.Meccan).ToList();
    }

    /// <summary>
    /// Gets all Medinan Surahs.
    /// </summary>
    /// <returns>A list of Medinan Surahs.</returns>
    public static List<Surah> GetMedinanSurahs()
    {
        return SurahMetadata.AllSurahs.Where(s => s.RevelationType == RevelationType.Medinan).ToList();
    }

    /// <summary>
    /// Gets all Surahs in chronological order of revelation.
    /// </summary>
    /// <returns>A list of Surahs ordered by revelation order.</returns>
    public static List<Surah> GetSurahsByRevelationOrder()
    {
        return SurahMetadata.AllSurahs.OrderBy(s => s.RevelationOrder).ToList();
    }

    /// <summary>
    /// Gets all Surahs that belong to a specific Juz.
    /// </summary>
    /// <param name="juzNumber">The Juz number (1-30).</param>
    /// <returns>A list of Surahs in the specified Juz.</returns>
    /// <exception cref="JuzNotFoundException">Thrown when the Juz number is invalid.</exception>
    public static List<Surah> GetSurahsByJuz(int juzNumber)
    {
        if (juzNumber < 1 || juzNumber > TotalJuz)
            throw new JuzNotFoundException($"Juz number {juzNumber} is invalid. Valid range is 1-30.");

        return SurahMetadata.AllSurahs
            .Where(s => s.JuzStart <= juzNumber && s.JuzEnd >= juzNumber)
            .ToList();
    }

    /// <summary>
    /// Checks whether a Surah number is valid (1-114).
    /// </summary>
    /// <param name="number">The Surah number to validate.</param>
    /// <returns>True if the Surah number is valid.</returns>
    public static bool IsValidSurah(int number)
    {
        return SurahByNumber.Value.ContainsKey(number);
    }

    /// <summary>
    /// Checks whether a Surah with the given English name exists.
    /// </summary>
    /// <param name="englishName">The English name to check.</param>
    /// <returns>True if a Surah with the given name exists.</returns>
    public static bool IsValidSurah(string englishName)
    {
        if (string.IsNullOrWhiteSpace(englishName))
            return false;

        return SurahByEnglishName.Value.ContainsKey(englishName.Trim().ToLowerInvariant());
    }

    #endregion

    #region Ayah Methods

    /// <summary>
    /// Gets a specific Ayah by Surah number and Ayah number.
    /// </summary>
    /// <param name="surahNumber">The Surah number (1-114).</param>
    /// <param name="ayahNumber">The Ayah number within the Surah.</param>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>The Ayah.</returns>
    /// <exception cref="SurahNotFoundException">Thrown when the Surah number is invalid.</exception>
    /// <exception cref="AyahNotFoundException">Thrown when the Ayah number is invalid.</exception>
    public static Ayah GetAyah(int surahNumber, int ayahNumber, ScriptType scriptType = ScriptType.Uthmani)
    {
        if (!IsValidSurah(surahNumber))
            throw new SurahNotFoundException($"Surah with number {surahNumber} was not found. Valid range is 1-114.");

        var ayah = QuranTextProvider.GetAyah(surahNumber, ayahNumber, scriptType);
        if (ayah == null)
            throw new AyahNotFoundException($"Ayah {ayahNumber} was not found in Surah {surahNumber}.");

        return ayah;
    }

    /// <summary>
    /// Gets a specific Ayah by Surah name and Ayah number.
    /// </summary>
    /// <param name="surahName">The Surah name enum.</param>
    /// <param name="ayahNumber">The Ayah number within the Surah.</param>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>The Ayah.</returns>
    public static Ayah GetAyah(SurahName surahName, int ayahNumber, ScriptType scriptType = ScriptType.Uthmani)
    {
        return GetAyah((int)surahName, ayahNumber, scriptType);
    }

    /// <summary>
    /// Gets all Ayahs for a specific Surah.
    /// </summary>
    /// <param name="surahNumber">The Surah number (1-114).</param>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>A list of all Ayahs in the Surah.</returns>
    /// <exception cref="SurahNotFoundException">Thrown when the Surah number is invalid.</exception>
    public static List<Ayah> GetAyahs(int surahNumber, ScriptType scriptType = ScriptType.Uthmani)
    {
        if (!IsValidSurah(surahNumber))
            throw new SurahNotFoundException($"Surah with number {surahNumber} was not found. Valid range is 1-114.");

        return new List<Ayah>(QuranTextProvider.GetAyahsForSurah(surahNumber, scriptType));
    }

    /// <summary>
    /// Gets all Ayahs for a specific Surah.
    /// </summary>
    /// <param name="surahName">The Surah name enum.</param>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>A list of all Ayahs in the Surah.</returns>
    public static List<Ayah> GetAyahs(SurahName surahName, ScriptType scriptType = ScriptType.Uthmani)
    {
        return GetAyahs((int)surahName, scriptType);
    }

    /// <summary>
    /// Gets a range of Ayahs from a specific Surah.
    /// </summary>
    /// <param name="surahNumber">The Surah number (1-114).</param>
    /// <param name="startAyah">The starting Ayah number (inclusive).</param>
    /// <param name="endAyah">The ending Ayah number (inclusive).</param>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>A list of Ayahs in the specified range.</returns>
    public static List<Ayah> GetAyahs(int surahNumber, int startAyah, int endAyah, ScriptType scriptType = ScriptType.Uthmani)
    {
        if (!IsValidSurah(surahNumber))
            throw new SurahNotFoundException($"Surah with number {surahNumber} was not found. Valid range is 1-114.");

        return QuranTextProvider.GetAyahsForSurah(surahNumber, scriptType)
            .Where(a => a.AyahNumber >= startAyah && a.AyahNumber <= endAyah)
            .ToList();
    }

    /// <summary>
    /// Checks whether Quran text data is loaded and available for the specified script type.
    /// </summary>
    /// <param name="scriptType">The script type to check.</param>
    /// <returns>True if data is available.</returns>
    public static bool IsTextDataAvailable(ScriptType scriptType = ScriptType.Uthmani)
    {
        return QuranTextProvider.IsDataAvailable(scriptType);
    }

    #endregion

    #region Juz Methods

    /// <summary>
    /// Gets a Juz by its number (1-30).
    /// </summary>
    /// <param name="number">The Juz number.</param>
    /// <returns>The Juz metadata.</returns>
    /// <exception cref="JuzNotFoundException">Thrown when the Juz number is invalid.</exception>
    public static Juz GetJuz(int number)
    {
        if (JuzByNumber.Value.TryGetValue(number, out var juz))
            return juz;

        throw new JuzNotFoundException($"Juz with number {number} was not found. Valid range is 1-30.");
    }

    /// <summary>
    /// Gets a Juz by its number, or null if not found.
    /// </summary>
    /// <param name="number">The Juz number.</param>
    /// <returns>The Juz metadata, or null if not found.</returns>
    public static Juz? GetJuzOrDefault(int number)
    {
        JuzByNumber.Value.TryGetValue(number, out var juz);
        return juz;
    }

    /// <summary>
    /// Gets all 30 Juz of the Quran.
    /// </summary>
    /// <returns>A list of all Juz.</returns>
    public static List<Juz> GetAllJuz()
    {
        return new List<Juz>(JuzMetadata.AllJuz);
    }

    /// <summary>
    /// Gets the Juz number for a specific Surah and Ayah.
    /// </summary>
    /// <param name="surahNumber">The Surah number.</param>
    /// <param name="ayahNumber">The Ayah number.</param>
    /// <returns>The Juz number (1-30).</returns>
    public static int GetJuzNumber(int surahNumber, int ayahNumber)
    {
        foreach (var juz in JuzMetadata.AllJuz)
        {
            bool afterStart = surahNumber > juz.StartSurah ||
                              (surahNumber == juz.StartSurah && ayahNumber >= juz.StartAyah);
            bool beforeEnd = surahNumber < juz.EndSurah ||
                             (surahNumber == juz.EndSurah && ayahNumber <= juz.EndAyah);

            if (afterStart && beforeEnd)
                return juz.Number;
        }

        return 0;
    }

    /// <summary>
    /// Checks whether a Juz number is valid (1-30).
    /// </summary>
    /// <param name="number">The Juz number to validate.</param>
    /// <returns>True if the Juz number is valid.</returns>
    public static bool IsValidJuz(int number)
    {
        return number >= 1 && number <= TotalJuz;
    }

    #endregion

    #region Sajda Methods

    /// <summary>
    /// Gets all Sajda (prostration) positions in the Quran.
    /// </summary>
    /// <returns>A list of all 15 Sajda positions.</returns>
    public static List<SajdaVerse> GetAllSajdas()
    {
        return new List<SajdaVerse>(SajdaMetadata.AllSajdas);
    }

    /// <summary>
    /// Gets all obligatory Sajda positions.
    /// </summary>
    /// <returns>A list of obligatory Sajda positions.</returns>
    public static List<SajdaVerse> GetObligatorySajdas()
    {
        return SajdaMetadata.AllSajdas.Where(s => s.Type == SajdaType.Obligatory).ToList();
    }

    /// <summary>
    /// Gets all recommended Sajda positions.
    /// </summary>
    /// <returns>A list of recommended Sajda positions.</returns>
    public static List<SajdaVerse> GetRecommendedSajdas()
    {
        return SajdaMetadata.AllSajdas.Where(s => s.Type == SajdaType.Recommended).ToList();
    }

    /// <summary>
    /// Checks whether a specific Ayah has a Sajda mark.
    /// </summary>
    /// <param name="surahNumber">The Surah number.</param>
    /// <param name="ayahNumber">The Ayah number.</param>
    /// <returns>True if the Ayah has a Sajda mark.</returns>
    public static bool IsSajdaAyah(int surahNumber, int ayahNumber)
    {
        return SajdaByKey.Value.ContainsKey($"{surahNumber}:{ayahNumber}");
    }

    /// <summary>
    /// Gets the Sajda information for a specific Ayah, or null if it's not a Sajda Ayah.
    /// </summary>
    /// <param name="surahNumber">The Surah number.</param>
    /// <param name="ayahNumber">The Ayah number.</param>
    /// <returns>The Sajda information, or null if not a Sajda Ayah.</returns>
    public static SajdaVerse? GetSajda(int surahNumber, int ayahNumber)
    {
        SajdaByKey.Value.TryGetValue($"{surahNumber}:{ayahNumber}", out var sajda);
        return sajda;
    }

    #endregion

    #region Search Methods

    /// <summary>
    /// Searches the entire Quran for Ayahs containing the specified search term.
    /// The search is diacritics-aware (tashkeel is ignored during matching).
    /// </summary>
    /// <param name="searchTerm">The text to search for.</param>
    /// <param name="scriptType">The script type to search in (default: Uthmani).</param>
    /// <returns>A list of search results.</returns>
    public static List<SearchResult> Search(string searchTerm, ScriptType scriptType = ScriptType.Uthmani)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new List<SearchResult>();

        var normalizedSearch = searchTerm.NormalizeForSearch();
        var results = new List<SearchResult>();

        for (int i = 1; i <= TotalSurahs; i++)
        {
            var ayahs = QuranTextProvider.GetAyahsForSurah(i, scriptType);
            var surah = GetSurah(i);

            foreach (var ayah in ayahs)
            {
                var normalizedText = ayah.Text.NormalizeForSearch();
                if (normalizedText.Contains(normalizedSearch))
                {
                    results.Add(new SearchResult(
                        ayah.SurahNumber,
                        ayah.AyahNumber,
                        ayah.Text,
                        surah.EnglishName,
                        searchTerm));
                }
            }
        }

        return results;
    }

    /// <summary>
    /// Searches within a specific Surah for Ayahs containing the specified search term.
    /// </summary>
    /// <param name="searchTerm">The text to search for.</param>
    /// <param name="surahNumber">The Surah number to search within.</param>
    /// <param name="scriptType">The script type to search in (default: Uthmani).</param>
    /// <returns>A list of search results.</returns>
    /// <exception cref="SurahNotFoundException">Thrown when the Surah number is invalid.</exception>
    public static List<SearchResult> Search(string searchTerm, int surahNumber, ScriptType scriptType = ScriptType.Uthmani)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new List<SearchResult>();

        if (!IsValidSurah(surahNumber))
            throw new SurahNotFoundException($"Surah with number {surahNumber} was not found. Valid range is 1-114.");

        var normalizedSearch = searchTerm.NormalizeForSearch();
        var results = new List<SearchResult>();
        var ayahs = QuranTextProvider.GetAyahsForSurah(surahNumber, scriptType);
        var surah = GetSurah(surahNumber);

        foreach (var ayah in ayahs)
        {
            var normalizedText = ayah.Text.NormalizeForSearch();
            if (normalizedText.Contains(normalizedSearch))
            {
                results.Add(new SearchResult(
                    ayah.SurahNumber,
                    ayah.AyahNumber,
                    ayah.Text,
                    surah.EnglishName,
                    searchTerm));
            }
        }

        return results;
    }

    /// <summary>
    /// Searches within a specific Surah for Ayahs containing the specified search term.
    /// </summary>
    /// <param name="searchTerm">The text to search for.</param>
    /// <param name="surahName">The Surah to search within.</param>
    /// <param name="scriptType">The script type to search in (default: Uthmani).</param>
    /// <returns>A list of search results.</returns>
    public static List<SearchResult> Search(string searchTerm, SurahName surahName, ScriptType scriptType = ScriptType.Uthmani)
    {
        return Search(searchTerm, (int)surahName, scriptType);
    }

    #endregion

    #region Utility Methods

    /// <summary>
    /// Gets the Surah names for a range of Surahs.
    /// </summary>
    /// <param name="start">The starting Surah number (default: 1).</param>
    /// <param name="end">The ending Surah number (default: 114).</param>
    /// <returns>A list of tuples containing (Number, EnglishName, ArabicName).</returns>
    public static List<(int Number, string EnglishName, string ArabicName)> GetSurahNames(int start = 1, int end = 114)
    {
        return SurahMetadata.AllSurahs
            .Where(s => s.Number >= start && s.Number <= end)
            .Select(s => (s.Number, s.EnglishName, s.ArabicName))
            .ToList();
    }

    /// <summary>
    /// Gets the total number of Ayahs in a specific Surah.
    /// </summary>
    /// <param name="surahNumber">The Surah number.</param>
    /// <returns>The total number of Ayahs.</returns>
    /// <exception cref="SurahNotFoundException">Thrown when the Surah number is invalid.</exception>
    public static int GetAyahCount(int surahNumber)
    {
        return GetSurah(surahNumber).AyahCount;
    }

    /// <summary>
    /// Gets the total number of Ayahs in a specific Surah.
    /// </summary>
    /// <param name="surahName">The Surah name enum.</param>
    /// <returns>The total number of Ayahs.</returns>
    public static int GetAyahCount(SurahName surahName)
    {
        return GetSurah(surahName).AyahCount;
    }

    #endregion
}
