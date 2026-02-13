using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;

/// <summary>
/// Tool to fetch Quran text data and translations from the alquran.cloud API
/// and generate embedded JSON resource files for the AlQuran.SDK NuGet package.
/// 
/// Usage: dotnet run
/// Output: JSON files will be created in src/AlQuran.SDK/Data/Resources/
/// </summary>

var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://api.alquran.cloud/v1/"),
    Timeout = TimeSpan.FromMinutes(5)
};

var outputDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "..", "src", "AlQuran.SDK", "Data", "Resources"));
Directory.CreateDirectory(outputDir);

Console.WriteLine("=== AlQuran Data Fetcher ===");
Console.WriteLine($"Output directory: {outputDir}");
Console.WriteLine();

// --- Arabic Script Editions ---
Console.WriteLine("--- Arabic Script Editions ---");
await FetchArabicEdition("quran-simple", "quran_simple.json", "Simple Arabic (without tashkeel)");
await FetchArabicEdition("quran-uthmani", "quran_uthmani.json", "Uthmani (with tashkeel)");

// --- Translation Editions ---
Console.WriteLine();
Console.WriteLine("--- Translation Editions ---");

var translationEditions = new (string apiId, string filename, string description)[]
{
    // English
    ("en.sahih",           "en_sahih.json",           "English - Saheeh International"),
    ("en.yusufali",        "en_yusufali.json",        "English - Abdullah Yusuf Ali"),
    ("en.pickthall",       "en_pickthall.json",       "English - M. Pickthall"),
    ("en.itani",           "en_itani.json",            "English - Clear Quran (Talal Itani)"),
    ("en.maududi",         "en_maududi.json",         "English - Abul Ala Maududi"),
    ("en.transliteration", "en_transliteration.json", "English - Transliteration"),

    // Urdu
    ("ur.jalandhry",       "ur_jalandhry.json",       "Urdu - Fateh Muhammad Jalandhry"),
    ("ur.junagarhi",       "ur_junagarhi.json",       "Urdu - Muhammad Junagarhi"),
    ("ur.maududi",         "ur_maududi.json",         "Urdu - Abul A'ala Maududi"),
};

foreach (var (apiId, filename, description) in translationEditions)
{
    await FetchTranslation(apiId, filename, description);
}

Console.WriteLine();
Console.WriteLine("Done! All data files have been generated.");
Console.WriteLine("Rebuild the AlQuran.SDK project to embed the new resource files.");

// ====================
// Fetch Arabic edition (with Juz/Page/HizbQuarter metadata)
// ====================
async Task FetchArabicEdition(string edition, string filename, string description)
{
    Console.WriteLine($"Fetching {description}...");

    var outputPath = Path.Combine(outputDir, filename);
    var allAyahs = new List<AyahEntry>();

    try
    {
        var response = await httpClient.GetFromJsonAsync<ApiResponse>($"quran/{edition}");

        if (response?.Data?.Surahs == null)
        {
            Console.WriteLine($"  ERROR: Failed to fetch data for {edition}");
            return;
        }

        foreach (var surah in response.Data.Surahs)
        {
            foreach (var ayah in surah.Ayahs)
            {
                allAyahs.Add(new AyahEntry
                {
                    Surah = surah.Number,
                    Ayah = ayah.NumberInSurah,
                    Text = ayah.Text,
                    Juz = ayah.Juz,
                    Page = ayah.Page,
                    HizbQuarter = ayah.HizbQuarter
                });
            }

            Console.Write($"\r  Processed: {surah.Number}/114 surahs");
        }

        Console.WriteLine();
        await WriteJson(outputPath, filename, allAyahs, allAyahs.Count);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"  ERROR: {ex.Message}");
    }
}

// ====================
// Fetch translation edition (text only — surah, ayah, text)
// ====================
async Task FetchTranslation(string edition, string filename, string description)
{
    Console.WriteLine($"Fetching {description}...");

    var outputPath = Path.Combine(outputDir, filename);

    // Skip if already exists (use --force to override)
    if (File.Exists(outputPath) && !args.Contains("--force"))
    {
        var existing = new FileInfo(outputPath);
        Console.WriteLine($"  SKIPPED (exists: {existing.Length / 1024}KB). Use --force to re-download.");
        return;
    }

    var allTranslations = new List<TranslationEntry>();

    try
    {
        var response = await httpClient.GetFromJsonAsync<ApiResponse>($"quran/{edition}");

        if (response?.Data?.Surahs == null)
        {
            Console.WriteLine($"  ERROR: Failed to fetch data for {edition}");
            return;
        }

        foreach (var surah in response.Data.Surahs)
        {
            foreach (var ayah in surah.Ayahs)
            {
                allTranslations.Add(new TranslationEntry
                {
                    Surah = surah.Number,
                    Ayah = ayah.NumberInSurah,
                    Text = ayah.Text
                });
            }

            Console.Write($"\r  Processed: {surah.Number}/114 surahs");
        }

        Console.WriteLine();
        await WriteJson(outputPath, filename, allTranslations, allTranslations.Count);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"  ERROR: {ex.Message}");
    }
}

// ====================
// Shared helper: write JSON to file
// ====================
async Task WriteJson<T>(string outputPath, string filename, T data, int count)
{
    var jsonOptions = new JsonSerializerOptions
    {
        WriteIndented = false,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    var json = JsonSerializer.Serialize(data, jsonOptions);
    await File.WriteAllTextAsync(outputPath, json);

    var fileInfo = new FileInfo(outputPath);
    Console.WriteLine($"  Written: {filename} ({fileInfo.Length / 1024}KB, {count} entries)");
}

// ====================
// API response models
// ====================
record ApiResponse(ApiData Data);
record ApiData(List<ApiSurah> Surahs);
record ApiSurah(int Number, List<ApiAyah> Ayahs);
record ApiAyah(int NumberInSurah, string Text, int Juz, int Page, int HizbQuarter);

class AyahEntry
{
    public int Surah { get; set; }
    public int Ayah { get; set; }
    public string Text { get; set; } = "";
    public int Juz { get; set; }
    public int Page { get; set; }
    public int HizbQuarter { get; set; }
}

class TranslationEntry
{
    public int Surah { get; set; }
    public int Ayah { get; set; }
    public string Text { get; set; } = "";
}
