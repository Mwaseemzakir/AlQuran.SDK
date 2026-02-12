# AlQuran

A comprehensive, high-performance .NET library providing complete read-only access to the Holy Quran.

[![NuGet](https://img.shields.io/nuget/v/AlQuran?style=flat-square)](https://www.nuget.org/packages/AlQuran)
[![NuGet Downloads](https://img.shields.io/nuget/dt/AlQuran?style=flat-square)](https://www.nuget.org/packages/AlQuran)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](LICENSE.txt)

---

## Features

- **Complete Quran Data** — All 114 Surahs, 6,236 Ayahs, 30 Juz, and 15 Sajda positions
- **Dual Script Support** — Both Uthmani (with tashkeel) and Simple Arabic scripts
- **Rich Metadata** — Arabic names, English names, meanings, revelation type & order, Ruku counts, page numbers
- **Juz/Para Support** — Full Juz boundary data with Arabic names
- **Sajda Positions** — All 15 Sajda positions with obligatory/recommended classification
- **Powerful Search** — Diacritics-aware Arabic text search across the entire Quran or specific Surahs
- **Arabic Text Utilities** — Remove tashkeel, normalize Alef, highlight matches
- **Static API** — No instance needed, just call `Quran.GetSurah(1)`
- **Multi-Target** — Supports .NET 8.0, .NET Standard 2.0, and .NET Framework 4.6.2
- **Thread-Safe** — Lazy-loaded data with thread-safe initialization
- **Immutable Models** — All data objects are read-only
- **Fully Documented** — XML documentation on every public API
- **160+ Unit Tests** — Comprehensive test coverage

## Installation

```
dotnet add package AlQuran
```

Or via the NuGet Package Manager:

```
Install-Package AlQuran
```

## Quick Start

```csharp
using AlQuran;
using AlQuran.Enums;

// Get Bismillah
string bismillah = Quran.GetBismillah();
// => "بِسْمِ ٱللَّهِ ٱلرَّحْمَٰنِ ٱلرَّحِيمِ"

// Get a Surah by number
var fatiha = Quran.GetSurah(1);
Console.WriteLine($"{fatiha.EnglishName} ({fatiha.ArabicName}) - {fatiha.AyahCount} Ayahs");
// => "Al-Fatiha (الفاتحة) - 7 Ayahs"

// Get a Surah by enum
var yasin = Quran.GetSurah(SurahName.YaSin);

// Get a Surah by name (case-insensitive)
var baqarah = Quran.GetSurah("Al-Baqarah");

// Get a specific Ayah
var ayah = Quran.GetAyah(1, 1);
Console.WriteLine(ayah.Text);

// Get all Ayahs of a Surah
var ayahs = Quran.GetAyahs(SurahName.AlFatiha);

// Get Ayahs in Simple script (without tashkeel)
var simpleAyahs = Quran.GetAyahs(1, ScriptType.Simple);

// Get an Ayah range
var firstFive = Quran.GetAyahs(2, 1, 5); // Al-Baqarah, Ayahs 1-5
```

## Juz (Para) Support

```csharp
// Get a specific Juz
var juz1 = Quran.GetJuz(1);
Console.WriteLine($"Juz {juz1.Number}: {juz1.ArabicName}");
// => "Juz 1: الم"

// Get all 30 Juz
var allJuz = Quran.GetAllJuz();

// Find which Juz an Ayah belongs to
int juzNumber = Quran.GetJuzNumber(2, 142); // Al-Baqarah:142
// => 2

// Get all Surahs in a Juz
var surahsInJuz30 = Quran.GetSurahsByJuz(30);
```

## Sajda Positions

```csharp
// Get all 15 Sajda positions
var allSajdas = Quran.GetAllSajdas();

// Get only obligatory Sajdas
var obligatory = Quran.GetObligatorySajdas();

// Check if a specific Ayah has a Sajda
bool isSajda = Quran.IsSajdaAyah(32, 15); // As-Sajdah:15
// => true

// Get Sajda info
var sajda = Quran.GetSajda(32, 15);
Console.WriteLine($"{sajda.Type}"); // => "Obligatory"
```

## Search

```csharp
// Search the entire Quran (diacritics-aware)
var results = Quran.Search("الله");

// Search within a specific Surah
var fatihaResults = Quran.Search("الله", 1);

// Search with Surah enum
var ihlasResults = Quran.Search("أحد", SurahName.AlIkhlas);

foreach (var result in results)
{
    Console.WriteLine($"[{result.SurahNumber}:{result.AyahNumber}] {result.SurahName}");
}
```

## Surah Metadata

```csharp
// Get revelation type
var surahs = Quran.GetMeccanSurahs();  // or GetMedinanSurahs()

// Get Surahs in revelation order
var chronological = Quran.GetSurahsByRevelationOrder();

// Get Surah names
var names = Quran.GetSurahNames(1, 10); // First 10 Surahs
foreach (var (number, english, arabic) in names)
{
    Console.WriteLine($"{number}. {english} ({arabic})");
}

// Check validation
bool isValid = Quran.IsValidSurah(1);  // true
bool isValid2 = Quran.IsValidSurah("Al-Fatiha");  // true
```

## Arabic Text Utilities

```csharp
using AlQuran.Extensions;

var text = "بِسْمِ ٱللَّهِ ٱلرَّحْمَٰنِ ٱلرَّحِيمِ";

// Remove tashkeel (diacritical marks)
string clean = text.RemoveTashkeel();

// Normalize for search
string normalized = text.NormalizeForSearch();

// Highlight matches (for Blazor, Console, MAUI, etc.)
string highlighted = text.HighlightMatch("الله", s => $"<b>{s}</b>");

// Check if character is tashkeel
bool isTashkeel = ArabicTextExtensions.IsTashkeelChar('\u064E'); // Fatha => true
```

## Supported Frameworks

| Framework | Version |
|-----------|---------|
| .NET | 8.0+ |
| .NET Standard | 2.0+ |
| .NET Framework | 4.6.2+ |

## Project Structure

```
src/AlQuran/           — Main library
tests/AlQuran.Tests/   — Unit tests (160+ tests)
tools/QuranDataFetcher/ — Data generation tool
docs/                  — Documentation
```

## Data Sources

The Quran text data is sourced from the [Al Quran Cloud API](https://alquran.cloud/api), which provides authentic Quran text in multiple editions. The data is embedded as JSON resources within the package.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

---

*This library provides the text of the Holy Quran for educational and reference purposes. Please treat the content with respect.*