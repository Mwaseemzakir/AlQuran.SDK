using AlQuran.SDK.Data;
using AlQuran.SDK.Enums;
using AlQuran.SDK.Exceptions;
using AlQuran.SDK.Models;

namespace AlQuran.SDK;

public static partial class Quran
{
    #region Translation Methods

    /// <summary>
    /// Gets a translated Ayah by Surah number, Ayah number, and translation edition.
    /// </summary>
    /// <param name="surahNumber">The Surah number (1-114).</param>
    /// <param name="ayahNumber">The Ayah number within the Surah.</param>
    /// <param name="edition">The translation edition to use.</param>
    /// <returns>The translated Ayah.</returns>
    /// <exception cref="SurahNotFoundException">Thrown when the Surah number is invalid.</exception>
    /// <exception cref="AyahNotFoundException">Thrown when the Ayah number is invalid.</exception>
    /// <exception cref="TranslationNotFoundException">Thrown when the translation data is not available.</exception>
    public static TranslatedAyah GetTranslation(int surahNumber, int ayahNumber, TranslationEdition edition)
    {
        if (!IsValidSurah(surahNumber))
            throw new SurahNotFoundException($"Surah with number {surahNumber} was not found. Valid range is 1-114.");

        if (!TranslationProvider.IsAvailable(edition))
            throw new TranslationNotFoundException($"Translation '{edition}' is not available. Ensure the translation data has been embedded.");

        var ayah = TranslationProvider.GetAyah(surahNumber, ayahNumber, edition);
        if (ayah == null)
            throw new AyahNotFoundException($"Ayah {ayahNumber} was not found in Surah {surahNumber} for edition '{edition}'.");

        return ayah;
    }

    /// <summary>
    /// Gets a translated Ayah by Surah name and Ayah number.
    /// </summary>
    /// <param name="surahName">The Surah name enum.</param>
    /// <param name="ayahNumber">The Ayah number within the Surah.</param>
    /// <param name="edition">The translation edition to use.</param>
    /// <returns>The translated Ayah.</returns>
    public static TranslatedAyah GetTranslation(SurahName surahName, int ayahNumber, TranslationEdition edition)
    {
        return GetTranslation((int)surahName, ayahNumber, edition);
    }

    /// <summary>
    /// Gets all translated Ayahs for a specific Surah.
    /// </summary>
    /// <param name="surahNumber">The Surah number (1-114).</param>
    /// <param name="edition">The translation edition to use.</param>
    /// <returns>A list of all translated Ayahs in the Surah.</returns>
    /// <exception cref="SurahNotFoundException">Thrown when the Surah number is invalid.</exception>
    /// <exception cref="TranslationNotFoundException">Thrown when the translation data is not available.</exception>
    public static List<TranslatedAyah> GetTranslations(int surahNumber, TranslationEdition edition)
    {
        if (!IsValidSurah(surahNumber))
            throw new SurahNotFoundException($"Surah with number {surahNumber} was not found. Valid range is 1-114.");

        if (!TranslationProvider.IsAvailable(edition))
            throw new TranslationNotFoundException($"Translation '{edition}' is not available.");

        return new List<TranslatedAyah>(TranslationProvider.GetAyahsForSurah(surahNumber, edition));
    }

    /// <summary>
    /// Gets all translated Ayahs for a specific Surah.
    /// </summary>
    /// <param name="surahName">The Surah name enum.</param>
    /// <param name="edition">The translation edition to use.</param>
    /// <returns>A list of all translated Ayahs in the Surah.</returns>
    public static List<TranslatedAyah> GetTranslations(SurahName surahName, TranslationEdition edition)
    {
        return GetTranslations((int)surahName, edition);
    }

    /// <summary>
    /// Gets a range of translated Ayahs from a specific Surah.
    /// </summary>
    /// <param name="surahNumber">The Surah number (1-114).</param>
    /// <param name="startAyah">The starting Ayah number (inclusive).</param>
    /// <param name="endAyah">The ending Ayah number (inclusive).</param>
    /// <param name="edition">The translation edition to use.</param>
    /// <returns>A list of translated Ayahs in the specified range.</returns>
    public static List<TranslatedAyah> GetTranslations(int surahNumber, int startAyah, int endAyah, TranslationEdition edition)
    {
        if (!IsValidSurah(surahNumber))
            throw new SurahNotFoundException($"Surah with number {surahNumber} was not found. Valid range is 1-114.");

        if (!TranslationProvider.IsAvailable(edition))
            throw new TranslationNotFoundException($"Translation '{edition}' is not available.");

        return TranslationProvider.GetAyahsForSurah(surahNumber, edition)
            .Where(a => a.AyahNumber >= startAyah && a.AyahNumber <= endAyah)
            .ToList();
    }

    /// <summary>
    /// Gets all available translation editions with metadata.
    /// </summary>
    /// <returns>A list of all translation editions.</returns>
    public static List<TranslationInfo> GetAvailableTranslations()
    {
        return new List<TranslationInfo>(TranslationCatalog.AllTranslations);
    }

    /// <summary>
    /// Gets available translation editions filtered by language.
    /// </summary>
    /// <param name="language">The language to filter by (e.g., "English", "Urdu"). Case-insensitive.</param>
    /// <returns>A list of matching translation editions.</returns>
    public static List<TranslationInfo> GetAvailableTranslations(string language)
    {
        if (string.IsNullOrWhiteSpace(language))
            return new List<TranslationInfo>();

        return TranslationCatalog.AllTranslations
            .Where(t => t.Language.IndexOf(language.Trim(), StringComparison.OrdinalIgnoreCase) >= 0)
            .ToList();
    }

    /// <summary>
    /// Checks whether translation data is loaded and available for the specified edition.
    /// </summary>
    /// <param name="edition">The translation edition to check.</param>
    /// <returns>True if the translation data is available.</returns>
    public static bool IsTranslationAvailable(TranslationEdition edition)
    {
        return TranslationProvider.IsAvailable(edition);
    }

    #endregion

    #region Translation Search Methods

    /// <summary>
    /// Searches the entire Quran for translated Ayahs containing the specified search term.
    /// Case-insensitive search.
    /// </summary>
    /// <param name="searchTerm">The text to search for.</param>
    /// <param name="edition">The translation edition to search in.</param>
    /// <returns>A list of translation search results.</returns>
    /// <exception cref="TranslationNotFoundException">Thrown when the translation data is not available.</exception>
    public static List<TranslationSearchResult> SearchTranslation(string searchTerm, TranslationEdition edition)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new List<TranslationSearchResult>();

        if (!TranslationProvider.IsAvailable(edition))
            throw new TranslationNotFoundException($"Translation '{edition}' is not available.");

        var results = new List<TranslationSearchResult>();

        for (int i = 1; i <= TotalSurahs; i++)
        {
            var ayahs = TranslationProvider.GetAyahsForSurah(i, edition);
            var surah = GetSurah(i);

            foreach (var ayah in ayahs)
            {
                if (ayah.Text.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    results.Add(new TranslationSearchResult(
                        ayah.SurahNumber,
                        ayah.AyahNumber,
                        ayah.Text,
                        surah.EnglishName,
                        searchTerm,
                        edition));
                }
            }
        }

        return results;
    }

    /// <summary>
    /// Searches within a specific Surah for translated Ayahs containing the specified search term.
    /// </summary>
    /// <param name="searchTerm">The text to search for.</param>
    /// <param name="surahNumber">The Surah number to search within.</param>
    /// <param name="edition">The translation edition to search in.</param>
    /// <returns>A list of translation search results.</returns>
    public static List<TranslationSearchResult> SearchTranslation(string searchTerm, int surahNumber, TranslationEdition edition)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new List<TranslationSearchResult>();

        if (!IsValidSurah(surahNumber))
            throw new SurahNotFoundException($"Surah with number {surahNumber} was not found. Valid range is 1-114.");

        if (!TranslationProvider.IsAvailable(edition))
            throw new TranslationNotFoundException($"Translation '{edition}' is not available.");

        var results = new List<TranslationSearchResult>();
        var ayahs = TranslationProvider.GetAyahsForSurah(surahNumber, edition);
        var surah = GetSurah(surahNumber);

        foreach (var ayah in ayahs)
        {
            if (ayah.Text.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                results.Add(new TranslationSearchResult(
                    ayah.SurahNumber,
                    ayah.AyahNumber,
                    ayah.Text,
                    surah.EnglishName,
                    searchTerm,
                    edition));
            }
        }

        return results;
    }

    /// <summary>
    /// Searches within a specific Surah for translated Ayahs containing the specified search term.
    /// </summary>
    /// <param name="searchTerm">The text to search for.</param>
    /// <param name="surahName">The Surah to search within.</param>
    /// <param name="edition">The translation edition to search in.</param>
    /// <returns>A list of translation search results.</returns>
    public static List<TranslationSearchResult> SearchTranslation(string searchTerm, SurahName surahName, TranslationEdition edition)
    {
        return SearchTranslation(searchTerm, (int)surahName, edition);
    }

    #endregion
}
