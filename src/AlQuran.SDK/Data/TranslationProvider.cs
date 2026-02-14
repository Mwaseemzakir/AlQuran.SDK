using AlQuran.SDK.Enums;
using AlQuran.SDK.Models;
using System.Collections.Concurrent;
using System.IO.Compression;
using System.Reflection;
using System.Text.Json;

namespace AlQuran.SDK.Data;

/// <summary>
/// Provides lazy-loaded translation text from embedded JSON resources.
/// Thread-safe with per-edition caching.
/// </summary>
internal static class TranslationProvider
{
    private static readonly ConcurrentDictionary<TranslationEdition, Lazy<Dictionary<string, List<TranslatedAyah>>>> Cache = new();

    /// <summary>
    /// Gets all translated Ayahs for a specific Surah and edition.
    /// </summary>
    internal static List<TranslatedAyah> GetAyahsForSurah(int surahNumber, TranslationEdition edition)
    {
        var store = GetStore(edition);
        var key = surahNumber.ToString();
        return store.TryGetValue(key, out var ayahs) ? ayahs : new List<TranslatedAyah>();
    }

    /// <summary>
    /// Gets a specific translated Ayah.
    /// </summary>
    internal static TranslatedAyah? GetAyah(int surahNumber, int ayahNumber, TranslationEdition edition)
    {
        var ayahs = GetAyahsForSurah(surahNumber, edition);
        return ayahs.Find(a => a.AyahNumber == ayahNumber);
    }

    /// <summary>
    /// Gets all translated Ayahs across the entire Quran for a specific edition.
    /// </summary>
    internal static List<TranslatedAyah> GetAllAyahs(TranslationEdition edition)
    {
        var store = GetStore(edition);
        var all = new List<TranslatedAyah>();
        for(int i = 1; i <= SurahMetadata.TotalSurahs; i++)
        {
            var key = i.ToString();
            if(store.TryGetValue(key, out var ayahs))
                all.AddRange(ayahs);
        }
        return all;
    }

    /// <summary>
    /// Checks whether translation data is loaded and available for the specified edition.
    /// </summary>
    internal static bool IsAvailable(TranslationEdition edition)
    {
        var store = GetStore(edition);
        return store.Count > 0;
    }

    private static Dictionary<string, List<TranslatedAyah>> GetStore(TranslationEdition edition)
    {
        var lazy = Cache.GetOrAdd(edition, e =>
            new Lazy<Dictionary<string, List<TranslatedAyah>>>(() => Load(e), true));
        return lazy.Value;
    }

    private static Dictionary<string, List<TranslatedAyah>> Load(TranslationEdition edition)
    {
        var result = new Dictionary<string, List<TranslatedAyah>>();

        try
        {
            var info = TranslationCatalog.GetInfo(edition);
            if(info == null)
                return result;

            var resourceName = $"AlQuran.SDK.Data.Resources.{info.ResourceName}.json";
            var assembly = Assembly.GetExecutingAssembly();
            var gzResourceName = $"AlQuran.SDK.Data.Resources.{info.ResourceName}.json.gz";
            using var stream = assembly.GetManifestResourceStream(gzResourceName);

            if(stream == null)
                return result;

            using var gzipStream = new GZipStream(stream, System.IO.Compression.CompressionMode.Decompress);
            using var reader = new StreamReader(gzipStream);
            var json = reader.ReadToEnd();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var entries = JsonSerializer.Deserialize<List<TranslationEntry>>(json, options);
            if(entries == null)
                return result;

            foreach(var entry in entries)
            {
                var key = entry.Surah.ToString();
                if(!result.ContainsKey(key))
                    result[key] = new List<TranslatedAyah>();

                result[key].Add(new TranslatedAyah(
                    entry.Surah,
                    entry.Ayah,
                    entry.Text ?? string.Empty,
                    edition));
            }
        }
        catch
        {
            // If translation data is not available, return empty — allows graceful degradation
        }

        return result;
    }

    /// <summary>
    /// Internal DTO for JSON deserialization of translation files.
    /// </summary>
    private class TranslationEntry
    {
        public int Surah { get; set; }
        public int Ayah { get; set; }
        public string? Text { get; set; }
    }
}
