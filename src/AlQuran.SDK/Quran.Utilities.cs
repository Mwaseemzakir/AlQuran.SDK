using AlQuran.SDK.Data;
using AlQuran.SDK.Enums;
using AlQuran.SDK.Exceptions;
using AlQuran.SDK.Extensions;
using AlQuran.SDK.Models;

namespace AlQuran.SDK;

public static partial class Quran
{
    #region Private Helpers

    private static readonly object RandomLock = new object();
    private static readonly Random SharedRandom = new Random();

    private static int NextRandom(int maxExclusive)
    {
        lock (RandomLock)
        {
            return SharedRandom.Next(maxExclusive);
        }
    }

    #endregion

    #region Verse Reference Methods

    /// <summary>
    /// Parses a verse reference string into a <see cref="VerseReference"/>.
    /// Supported formats: "2:255" (single verse), "2:1-5" (verse range).
    /// </summary>
    /// <param name="reference">The verse reference string.</param>
    /// <returns>A parsed <see cref="VerseReference"/>.</returns>
    /// <exception cref="FormatException">Thrown when the reference format is invalid.</exception>
    public static VerseReference ParseVerseReference(string reference)
    {
        return VerseReference.Parse(reference);
    }

    /// <summary>
    /// Gets a specific Ayah by verse reference string (e.g., "2:255").
    /// </summary>
    /// <param name="verseReference">The verse reference in "surah:ayah" format.</param>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>The Ayah.</returns>
    /// <exception cref="FormatException">Thrown when the reference format is invalid.</exception>
    /// <exception cref="SurahNotFoundException">Thrown when the Surah number is invalid.</exception>
    /// <exception cref="AyahNotFoundException">Thrown when the Ayah number is invalid.</exception>
    public static Ayah GetAyah(string verseReference, ScriptType scriptType = ScriptType.Uthmani)
    {
        var parsed = VerseReference.Parse(verseReference);
        return GetAyah(parsed.SurahNumber, parsed.AyahNumber, scriptType);
    }

    /// <summary>
    /// Gets a range of Ayahs by verse reference string (e.g., "2:1-5").
    /// Also supports single-verse references like "2:255" (returns a list with one Ayah).
    /// </summary>
    /// <param name="verseReference">The verse reference in "surah:ayah" or "surah:start-end" format.</param>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>A list of Ayahs.</returns>
    public static List<Ayah> GetAyahRange(string verseReference, ScriptType scriptType = ScriptType.Uthmani)
    {
        var parsed = VerseReference.Parse(verseReference);
        if (parsed.IsRange)
        {
            return GetAyahs(parsed.SurahNumber, parsed.AyahNumber, parsed.EndAyahNumber!.Value, scriptType);
        }
        return new List<Ayah> { GetAyah(parsed.SurahNumber, parsed.AyahNumber, scriptType) };
    }

    #endregion

    #region Random & Daily Ayah Methods

    /// <summary>
    /// Gets a random Ayah from the entire Quran.
    /// </summary>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>A random Ayah.</returns>
    public static Ayah GetRandomAyah(ScriptType scriptType = ScriptType.Uthmani)
    {
        var allAyahs = QuranTextProvider.GetAllAyahs(scriptType);
        int index = NextRandom(allAyahs.Count);
        return allAyahs[index];
    }

    /// <summary>
    /// Gets a random Ayah from a specific Surah.
    /// </summary>
    /// <param name="surahNumber">The Surah number (1-114).</param>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>A random Ayah from the specified Surah.</returns>
    /// <exception cref="SurahNotFoundException">Thrown when the Surah number is invalid.</exception>
    public static Ayah GetRandomAyah(int surahNumber, ScriptType scriptType = ScriptType.Uthmani)
    {
        if (!IsValidSurah(surahNumber))
            throw new SurahNotFoundException($"Surah with number {surahNumber} was not found. Valid range is 1-114.");

        var ayahs = QuranTextProvider.GetAyahsForSurah(surahNumber, scriptType);
        int index = NextRandom(ayahs.Count);
        return ayahs[index];
    }

    /// <summary>
    /// Gets the "Ayah of the Day" — a deterministic Ayah based on the current UTC date.
    /// The same date always returns the same Ayah, making it suitable for daily reflection features.
    /// </summary>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>The Ayah of the day.</returns>
    public static Ayah GetAyahOfTheDay(ScriptType scriptType = ScriptType.Uthmani)
    {
        var allAyahs = QuranTextProvider.GetAllAyahs(scriptType);
        var today = DateTime.UtcNow.Date;
        int seed = today.Year * 10000 + today.Month * 100 + today.Day;
        var rng = new Random(seed);
        int index = rng.Next(allAyahs.Count);
        return allAyahs[index];
    }

    /// <summary>
    /// Gets the "Ayah of the Day" for a specific date.
    /// </summary>
    /// <param name="date">The date to get the Ayah for.</param>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>The Ayah of the specified day.</returns>
    public static Ayah GetAyahOfTheDay(DateTime date, ScriptType scriptType = ScriptType.Uthmani)
    {
        var allAyahs = QuranTextProvider.GetAllAyahs(scriptType);
        int seed = date.Year * 10000 + date.Month * 100 + date.Day;
        var rng = new Random(seed);
        int index = rng.Next(allAyahs.Count);
        return allAyahs[index];
    }

    #endregion

    #region Muqatta'at Methods

    /// <summary>
    /// Gets all 29 Surahs that begin with Muqatta'at (disconnected/mysterious letters).
    /// </summary>
    /// <returns>A list of all Muqatta'at Surahs.</returns>
    public static List<MuqattaatSurah> GetMuqattaat()
    {
        return new List<MuqattaatSurah>(MuqattaatMetadata.AllMuqattaat);
    }

    /// <summary>
    /// Checks whether a Surah begins with Muqatta'at letters.
    /// </summary>
    /// <param name="surahNumber">The Surah number.</param>
    /// <returns>True if the Surah has Muqatta'at letters.</returns>
    public static bool HasMuqattaat(int surahNumber)
    {
        return MuqattaatMetadata.HasMuqattaat(surahNumber);
    }

    /// <summary>
    /// Gets the Muqatta'at information for a specific Surah, or null if it doesn't have Muqatta'at.
    /// </summary>
    /// <param name="surahNumber">The Surah number.</param>
    /// <returns>The Muqatta'at info, or null.</returns>
    public static MuqattaatSurah? GetMuqattaatForSurah(int surahNumber)
    {
        return MuqattaatMetadata.GetBySurah(surahNumber);
    }

    #endregion

    #region Statistics Methods

    /// <summary>
    /// Gets the total word count for a specific Surah.
    /// </summary>
    /// <param name="surahNumber">The Surah number (1-114).</param>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>The number of words in the Surah.</returns>
    /// <exception cref="SurahNotFoundException">Thrown when the Surah number is invalid.</exception>
    public static int GetWordCount(int surahNumber, ScriptType scriptType = ScriptType.Uthmani)
    {
        var ayahs = GetAyahs(surahNumber, scriptType);
        return ayahs.Sum(a => CountWords(a.Text));
    }

    /// <summary>
    /// Gets the total word count for a specific Surah.
    /// </summary>
    /// <param name="surahName">The Surah name enum.</param>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>The number of words in the Surah.</returns>
    public static int GetWordCount(SurahName surahName, ScriptType scriptType = ScriptType.Uthmani)
    {
        return GetWordCount((int)surahName, scriptType);
    }

    /// <summary>
    /// Gets the total letter count for a specific Surah (excluding diacritics and whitespace).
    /// </summary>
    /// <param name="surahNumber">The Surah number (1-114).</param>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>The number of letters in the Surah.</returns>
    /// <exception cref="SurahNotFoundException">Thrown when the Surah number is invalid.</exception>
    public static int GetLetterCount(int surahNumber, ScriptType scriptType = ScriptType.Uthmani)
    {
        var ayahs = GetAyahs(surahNumber, scriptType);
        return ayahs.Sum(a => CountLetters(a.Text));
    }

    /// <summary>
    /// Gets the total word count across the entire Quran.
    /// </summary>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>The total number of words in the Quran.</returns>
    public static int GetTotalWordCount(ScriptType scriptType = ScriptType.Uthmani)
    {
        var allAyahs = QuranTextProvider.GetAllAyahs(scriptType);
        return allAyahs.Sum(a => CountWords(a.Text));
    }

    /// <summary>
    /// Gets the total letter count across the entire Quran (excluding diacritics and whitespace).
    /// </summary>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>The total number of letters in the Quran.</returns>
    public static int GetTotalLetterCount(ScriptType scriptType = ScriptType.Uthmani)
    {
        var allAyahs = QuranTextProvider.GetAllAyahs(scriptType);
        return allAyahs.Sum(a => CountLetters(a.Text));
    }

    /// <summary>
    /// Gets the count of unique words in the entire Quran (normalized — tashkeel removed, alef forms unified).
    /// </summary>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>The number of unique words.</returns>
    public static int GetUniqueWordCount(ScriptType scriptType = ScriptType.Uthmani)
    {
        var allAyahs = QuranTextProvider.GetAllAyahs(scriptType);
        var words = new HashSet<string>();

        foreach (var ayah in allAyahs)
        {
            foreach (var word in ayah.Text.NormalizeForSearch()
                         .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                words.Add(word);
            }
        }

        return words.Count;
    }

    /// <summary>
    /// Gets the count of unique words in a specific Surah (normalized — tashkeel removed, alef forms unified).
    /// </summary>
    /// <param name="surahNumber">The Surah number (1-114).</param>
    /// <param name="scriptType">The script type (default: Uthmani).</param>
    /// <returns>The number of unique words in the Surah.</returns>
    public static int GetUniqueWordCount(int surahNumber, ScriptType scriptType = ScriptType.Uthmani)
    {
        var ayahs = GetAyahs(surahNumber, scriptType);
        var words = new HashSet<string>();

        foreach (var ayah in ayahs)
        {
            foreach (var word in ayah.Text.NormalizeForSearch()
                         .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                words.Add(word);
            }
        }

        return words.Count;
    }

    private static int CountWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return 0;
        return text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    private static int CountLetters(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return 0;
        var clean = text.RemoveTashkeel();
        int count = 0;
        for (int i = 0; i < clean.Length; i++)
        {
            if (!char.IsWhiteSpace(clean[i]))
                count++;
        }
        return count;
    }

    #endregion
}
