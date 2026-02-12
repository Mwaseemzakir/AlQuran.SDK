using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;

/// <summary>
/// Tool to fetch Quran text data from the alquran.cloud API and generate embedded JSON resource files.
/// Run this tool once to populate the data files used by the AlQuran NuGet package.
/// 
/// Usage: dotnet run
/// Output: Two JSON files will be created in src/AlQuran/Data/Resources/
/// </summary>

var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://api.alquran.cloud/v1/"),
    Timeout = TimeSpan.FromMinutes(5)
};

var outputDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "..", "src", "AlQuran", "Data", "Resources"));
Directory.CreateDirectory(outputDir);

Console.WriteLine("=== AlQuran Data Fetcher ===");
Console.WriteLine($"Output directory: {outputDir}");
Console.WriteLine();

await FetchEdition("quran-simple", "quran_simple.json", "Simple Arabic (without tashkeel)");
await FetchEdition("quran-uthmani", "quran_uthmani.json", "Uthmani (with tashkeel)");

Console.WriteLine();
Console.WriteLine("Done! Data files have been generated.");
Console.WriteLine("Rebuild the AlQuran project to embed the new resource files.");

async Task FetchEdition(string edition, string filename, string description)
{
    Console.WriteLine($"Fetching {description}...");

    var outputPath = Path.Combine(outputDir, filename);
    var allAyahs = new List<AyahEntry>();

    try
    {
        // Fetch the entire Quran at once
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

        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = false, // Compact for smaller file size
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Preserve Arabic characters
        };

        var json = JsonSerializer.Serialize(allAyahs, jsonOptions);
        await File.WriteAllTextAsync(outputPath, json);

        var fileInfo = new FileInfo(outputPath);
        Console.WriteLine($"  Written: {filename} ({fileInfo.Length / 1024}KB, {allAyahs.Count} ayahs)");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"  ERROR: {ex.Message}");
    }
}

// API response models
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
