using System.Reflection;
using System.Text.Json;
using AlQuran.Enums;
using AlQuran.Models;

namespace AlQuran.Data;

/// <summary>
/// Provides lazy-loaded Quran text from embedded JSON resources.
/// </summary>
internal static class QuranTextProvider
{
    private static readonly Lazy<Dictionary<string, List<Ayah>>> UthmaniAyahs = new(
        () => LoadAyahs("AlQuran.Data.Resources.quran_uthmani.json"), isThreadSafe: true);

    private static readonly Lazy<Dictionary<string, List<Ayah>>> SimpleAyahs = new(
        () => LoadAyahs("AlQuran.Data.Resources.quran_simple.json"), isThreadSafe: true);

    /// <summary>
    /// Gets all Ayahs for the specified script type, grouped by "surah:ayah" key.
    /// </summary>
    internal static Dictionary<string, List<Ayah>> GetAyahStore(ScriptType scriptType)
    {
        return scriptType == ScriptType.Uthmani ? UthmaniAyahs.Value : SimpleAyahs.Value;
    }

    /// <summary>
    /// Gets all Ayahs for a specific Surah.
    /// </summary>
    internal static List<Ayah> GetAyahsForSurah(int surahNumber, ScriptType scriptType)
    {
        var store = GetAyahStore(scriptType);
        var key = surahNumber.ToString();

        if (store.TryGetValue(key, out var ayahs))
            return ayahs;

        return new List<Ayah>();
    }

    /// <summary>
    /// Gets a specific Ayah.
    /// </summary>
    internal static Ayah? GetAyah(int surahNumber, int ayahNumber, ScriptType scriptType)
    {
        var ayahs = GetAyahsForSurah(surahNumber, scriptType);
        return ayahs.Find(a => a.AyahNumber == ayahNumber);
    }

    /// <summary>
    /// Gets all Ayahs across the entire Quran.
    /// </summary>
    internal static List<Ayah> GetAllAyahs(ScriptType scriptType)
    {
        var store = GetAyahStore(scriptType);
        var all = new List<Ayah>();
        for (int i = 1; i <= SurahMetadata.TotalSurahs; i++)
        {
            var key = i.ToString();
            if (store.TryGetValue(key, out var ayahs))
                all.AddRange(ayahs);
        }
        return all;
    }

    /// <summary>
    /// Checks whether Quran text data is loaded and available.
    /// </summary>
    internal static bool IsDataAvailable(ScriptType scriptType)
    {
        var store = GetAyahStore(scriptType);
        return store.Count > 0;
    }

    private static Dictionary<string, List<Ayah>> LoadAyahs(string resourceName)
    {
        var result = new Dictionary<string, List<Ayah>>();

        try
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(resourceName);

            if (stream == null)
                return result;

            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var entries = JsonSerializer.Deserialize<List<AyahEntry>>(json, options);
            if (entries == null)
                return result;

            foreach (var entry in entries)
            {
                var key = entry.Surah.ToString();
                if (!result.ContainsKey(key))
                    result[key] = new List<Ayah>();

                var ayah = new Ayah(
                    entry.Surah,
                    entry.Ayah,
                    entry.Text ?? string.Empty,
                    entry.Juz,
                    entry.Page,
                    entry.HizbQuarter);

                result[key].Add(ayah);
            }

            // Mark sajda ayahs
            foreach (var sajda in SajdaMetadata.AllSajdas)
            {
                var sKey = sajda.SurahNumber.ToString();
                if (result.TryGetValue(sKey, out var surahAyahs))
                {
                    var sajdaAyah = surahAyahs.Find(a => a.AyahNumber == sajda.AyahNumber);
                    if (sajdaAyah != null)
                        sajdaAyah.HasSajda = true;
                }
            }
        }
        catch
        {
            // If data files are not present, return empty - allows metadata-only usage
        }

        return result;
    }

    /// <summary>
    /// Internal DTO for JSON deserialization.
    /// </summary>
    private class AyahEntry
    {
        public int Surah { get; set; }
        public int Ayah { get; set; }
        public string? Text { get; set; }
        public int Juz { get; set; }
        public int Page { get; set; }
        public int HizbQuarter { get; set; }
    }
}
