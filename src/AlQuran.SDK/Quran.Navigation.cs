using AlQuran.SDK.Data;
using AlQuran.SDK.Enums;
using AlQuran.SDK.Models;

namespace AlQuran.SDK;

public static partial class Quran
{
    #region Constants (Navigation)

    /// <summary>
    /// The total number of pages in the standard Madani Mushaf: 604.
    /// </summary>
    public const int TotalPages = 604;

    /// <summary>
    /// The total number of Hizb quarters in the Quran: 240.
    /// </summary>
    public const int TotalHizbQuarters = 240;

    /// <summary>
    /// The total number of Manzils (weekly divisions) in the Quran: 7.
    /// </summary>
    public const int TotalManzils = 7;

    #endregion

    #region Page Methods

    /// <summary>
    /// Gets all Ayahs on a specific page of the standard Madani Mushaf.
    /// </summary>
    /// <param name="page">The page number (1-604).</param>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>A list of Ayahs on the specified page.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the page number is invalid.</exception>
    public static List<Ayah> GetAyahsByPage(int page, ScriptType scriptType = ScriptType.Uthmani)
    {
        if (page < 1 || page > TotalPages)
            throw new ArgumentOutOfRangeException(nameof(page), $"Page number must be between 1 and {TotalPages}.");

        return QuranTextProvider.GetAllAyahs(scriptType)
            .Where(a => a.Page == page)
            .ToList();
    }

    /// <summary>
    /// Gets the page number for a specific Surah and Ayah.
    /// </summary>
    /// <param name="surahNumber">The Surah number (1-114).</param>
    /// <param name="ayahNumber">The Ayah number.</param>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>The page number (1-604), or 0 if not found.</returns>
    public static int GetPageNumber(int surahNumber, int ayahNumber, ScriptType scriptType = ScriptType.Uthmani)
    {
        var ayah = QuranTextProvider.GetAyah(surahNumber, ayahNumber, scriptType);
        return ayah?.Page ?? 0;
    }

    #endregion

    #region Hizb Quarter Methods

    /// <summary>
    /// Gets all Ayahs in a specific Hizb quarter.
    /// </summary>
    /// <param name="hizbQuarter">The Hizb quarter number (1-240).</param>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>A list of Ayahs in the specified Hizb quarter.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the Hizb quarter number is invalid.</exception>
    public static List<Ayah> GetAyahsByHizbQuarter(int hizbQuarter, ScriptType scriptType = ScriptType.Uthmani)
    {
        if (hizbQuarter < 1 || hizbQuarter > TotalHizbQuarters)
            throw new ArgumentOutOfRangeException(nameof(hizbQuarter), $"Hizb quarter must be between 1 and {TotalHizbQuarters}.");

        return QuranTextProvider.GetAllAyahs(scriptType)
            .Where(a => a.HizbQuarter == hizbQuarter)
            .ToList();
    }

    /// <summary>
    /// Gets the Hizb quarter number for a specific Surah and Ayah.
    /// </summary>
    /// <param name="surahNumber">The Surah number (1-114).</param>
    /// <param name="ayahNumber">The Ayah number.</param>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>The Hizb quarter number (1-240), or 0 if not found.</returns>
    public static int GetHizbQuarter(int surahNumber, int ayahNumber, ScriptType scriptType = ScriptType.Uthmani)
    {
        var ayah = QuranTextProvider.GetAyah(surahNumber, ayahNumber, scriptType);
        return ayah?.HizbQuarter ?? 0;
    }

    #endregion

    #region Manzil Methods

    /// <summary>
    /// Gets a Manzil by its number (1-7).
    /// </summary>
    /// <param name="number">The Manzil number.</param>
    /// <returns>The Manzil metadata.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the Manzil number is invalid.</exception>
    public static Manzil GetManzil(int number)
    {
        var manzil = ManzilMetadata.GetByNumber(number);
        if (manzil == null)
            throw new ArgumentOutOfRangeException(nameof(number), $"Manzil number must be between 1 and {TotalManzils}.");

        return manzil;
    }

    /// <summary>
    /// Gets a Manzil by its number, or null if not found.
    /// </summary>
    /// <param name="number">The Manzil number.</param>
    /// <returns>The Manzil metadata, or null if not found.</returns>
    public static Manzil? GetManzilOrDefault(int number)
    {
        return ManzilMetadata.GetByNumber(number);
    }

    /// <summary>
    /// Gets all 7 Manzils of the Quran.
    /// </summary>
    /// <returns>A list of all Manzils.</returns>
    public static List<Manzil> GetAllManzils()
    {
        return new List<Manzil>(ManzilMetadata.AllManzils);
    }

    /// <summary>
    /// Gets the Manzil number for a specific Surah and Ayah.
    /// </summary>
    /// <param name="surahNumber">The Surah number.</param>
    /// <param name="ayahNumber">The Ayah number.</param>
    /// <returns>The Manzil number (1-7), or 0 if not found.</returns>
    public static int GetManzilNumber(int surahNumber, int ayahNumber)
    {
        foreach (var manzil in ManzilMetadata.AllManzils)
        {
            bool afterStart = surahNumber > manzil.StartSurah ||
                              (surahNumber == manzil.StartSurah && ayahNumber >= manzil.StartAyah);
            bool beforeEnd = surahNumber < manzil.EndSurah ||
                             (surahNumber == manzil.EndSurah && ayahNumber <= manzil.EndAyah);

            if (afterStart && beforeEnd)
                return manzil.Number;
        }

        return 0;
    }

    /// <summary>
    /// Gets all Ayahs in a specific Manzil.
    /// </summary>
    /// <param name="manzilNumber">The Manzil number (1-7).</param>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>A list of all Ayahs in the specified Manzil.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the Manzil number is invalid.</exception>
    public static List<Ayah> GetAyahsByManzil(int manzilNumber, ScriptType scriptType = ScriptType.Uthmani)
    {
        var manzil = GetManzil(manzilNumber);
        var result = new List<Ayah>();

        for (int s = manzil.StartSurah; s <= manzil.EndSurah; s++)
        {
            var ayahs = QuranTextProvider.GetAyahsForSurah(s, scriptType);
            foreach (var ayah in ayahs)
            {
                bool afterStart = s > manzil.StartSurah ||
                                  (s == manzil.StartSurah && ayah.AyahNumber >= manzil.StartAyah);
                bool beforeEnd = s < manzil.EndSurah ||
                                 (s == manzil.EndSurah && ayah.AyahNumber <= manzil.EndAyah);

                if (afterStart && beforeEnd)
                    result.Add(ayah);
            }
        }

        return result;
    }

    /// <summary>
    /// Checks whether a Manzil number is valid (1-7).
    /// </summary>
    /// <param name="number">The Manzil number to validate.</param>
    /// <returns>True if the Manzil number is valid.</returns>
    public static bool IsValidManzil(int number)
    {
        return number >= 1 && number <= TotalManzils;
    }

    #endregion
}
