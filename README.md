# AlQuran.SDK

A comprehensive, high-performance .NET library providing complete read-only access to the Holy Quran with built-in translations.

[![NuGet](https://img.shields.io/nuget/v/AlQuran.SDK?style=flat-square)](https://www.nuget.org/packages/AlQuran.SDK)
[![NuGet Downloads](https://img.shields.io/nuget/dt/AlQuran.SDK?style=flat-square)](https://www.nuget.org/packages/AlQuran.SDK)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](LICENSE.txt)

---

## Features

- **Complete Quran Data** — All 114 Surahs, 6,236 Ayahs, 30 Juz, and 15 Sajda positions
- **Dual Script Support** — Both Uthmani (with tashkeel) and Simple Arabic scripts
- **9 Built-in Translations** — 6 English (Sahih International, Yusuf Ali, Pickthall, Clear Quran, Maududi, Transliteration) + 3 Urdu (Jalandhry, Junagarhi, Maududi)
- **Rich Metadata** — Arabic names, English names, meanings, revelation type & order, Ruku counts, page numbers
- **Page & Hizb Navigation** — Navigate by Mushaf page (1-604) or Hizb quarter (1-240)
- **Manzil Support** — All 7 Manzil (weekly reading) divisions
- **Juz/Para Support** — Full Juz boundary data with Arabic names
- **Sajda Positions** — All 15 Sajda positions with obligatory/recommended classification
- **Muqatta'at Letters** — All 29 Surahs with disconnected/mysterious letters
- **Powerful Search** — Diacritics-aware Arabic text search + translation search
- **Arabic Text Utilities** — Remove tashkeel, normalize Alef, highlight matches
- **Verse Reference Parsing** — Parse "2:255" or "2:1-5" format references
- **Random & Daily Ayah** — Get random Ayahs or deterministic daily Ayah
- **Statistics** — Word counts, letter counts, unique word counts per Surah or entire Quran
- **100% Offline** — All data embedded in the package, no API calls at runtime
- **Static API** — No instance needed, just call `Quran.GetSurah(1)`
- **Multi-Target** — Supports .NET 8.0, .NET Standard 2.0, and .NET Framework 4.6.2
- **Thread-Safe** — Lazy-loaded data with thread-safe initialization
- **Immutable Models** — All data objects are read-only
- **Fully Documented** — XML documentation on every public API
- **1,000+ Unit Tests** — Comprehensive test coverage

## Installation

```
dotnet add package AlQuran.SDK
```

Or via the NuGet Package Manager:

```
Install-Package AlQuran.SDK
```

## Quick Start

```csharp
using AlQuran.SDK;
using AlQuran.SDK.Enums;

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

## Translations

The SDK ships with 9 built-in translations — all data is embedded offline, no API calls needed.

```csharp
using AlQuran.SDK.Enums;

// Get a single translated Ayah
var translated = Quran.GetTranslation(2, 255, TranslationEdition.EnglishSaheehInternational);
Console.WriteLine(translated.Text);

// Get all translations for a Surah
var surahTranslation = Quran.GetTranslations(1, TranslationEdition.EnglishYusufAli);

// Get a range of translated Ayahs
var range = Quran.GetTranslations(2, 1, 5, TranslationEdition.EnglishClearQuran);

// Get Urdu translation
var urdu = Quran.GetTranslation(1, 1, TranslationEdition.UrduJalandhry);

// List all available editions
var editions = Quran.GetAvailableTranslations();
var englishEditions = Quran.GetAvailableTranslations("English");

// Search within a translation
var results = Quran.SearchTranslation("mercy", TranslationEdition.EnglishSaheehInternational);
var surahResults = Quran.SearchTranslation("Lord", 1, TranslationEdition.EnglishPickthall);
```

### Available Translation Editions

| Edition | Language | Author |
|---------|----------|--------|
| `EnglishSaheehInternational` | English | Saheeh International |
| `EnglishYusufAli` | English | Abdullah Yusuf Ali |
| `EnglishPickthall` | English | Mohammed Marmaduke Pickthall |
| `EnglishClearQuran` | English | Talal Itani (Clear Quran) |
| `EnglishMaududi` | English | Abul Ala Maududi |
| `EnglishTransliteration` | English | Transliteration |
| `UrduJalandhry` | Urdu | Fateh Muhammad Jalandhry |
| `UrduJunagarhi` | Urdu | Muhammad Junagarhi |
| `UrduMaududi` | Urdu | Abul Ala Maududi |

## Page & Hizb Navigation

```csharp
// Get Ayahs on a specific Mushaf page (1-604)
var pageAyahs = Quran.GetAyahsByPage(1);

// Get the page number for a specific Ayah
int page = Quran.GetPageNumber(2, 255); // Ayat al-Kursi

// Get Ayahs in a Hizb quarter (1-240)
var hizbAyahs = Quran.GetAyahsByHizbQuarter(1);

// Get the Hizb quarter for a specific Ayah
int hizb = Quran.GetHizbQuarter(2, 255);
```

## Manzil (Weekly Reading Divisions)

```csharp
// Get a specific Manzil (1-7)
var manzil = Quran.GetManzil(1);
Console.WriteLine($"Manzil {manzil.Number}: {manzil.StartSurah}:{manzil.StartAyah} - {manzil.EndSurah}:{manzil.EndAyah}");

// Get all 7 Manzils
var allManzils = Quran.GetAllManzils();

// Find which Manzil an Ayah belongs to
int manzilNumber = Quran.GetManzilNumber(2, 255);

// Get all Ayahs in a Manzil
var manzilAyahs = Quran.GetAyahsByManzil(1);
```

## Verse Reference Parsing

```csharp
// Parse a verse reference
var reference = Quran.ParseVerseReference("2:255");
// reference.SurahNumber = 2, reference.AyahNumber = 255

// Parse a range reference
var range = VerseReference.Parse("2:1-5");
// range.IsRange = true, range.EndAyahNumber = 5

// Get Ayah directly by reference string
var ayah = Quran.GetAyah("2:255");

// Get a range of Ayahs
var ayahs = Quran.GetAyahRange("1:1-7"); // All of Al-Fatiha
```

## Random & Daily Ayah

```csharp
// Get a random Ayah
var randomAyah = Quran.GetRandomAyah();

// Get a random Ayah from a specific Surah
var randomFromBaqarah = Quran.GetRandomAyah(2);

// Get the Ayah of the Day (deterministic — same date = same Ayah)
var dailyAyah = Quran.GetAyahOfTheDay();

// Get Ayah of the Day for a specific date
var specificDay = Quran.GetAyahOfTheDay(new DateTime(2025, 1, 1));
```

## Muqatta'at (Disconnected Letters)

```csharp
// Get all 29 Surahs with Muqatta'at letters
var muqattaat = Quran.GetMuqattaat();

// Check if a Surah has Muqatta'at
bool hasLetters = Quran.HasMuqattaat(2); // true — Al-Baqarah starts with "Alif Lam Mim"

// Get Muqatta'at details for a specific Surah
var info = Quran.GetMuqattaatForSurah(2);
Console.WriteLine($"{info.Letters} ({info.ArabicLetters})");
// => "Alif Lam Mim (الم)"
```

## Statistics

```csharp
// Word count for a Surah
int words = Quran.GetWordCount(1); // Al-Fatiha

// Letter count for a Surah (excluding diacritics & whitespace)
int letters = Quran.GetLetterCount(2);

// Total word/letter count for the entire Quran
int totalWords = Quran.GetTotalWordCount();
int totalLetters = Quran.GetTotalLetterCount();

// Unique word count
int uniqueWords = Quran.GetUniqueWordCount();
int uniqueInSurah = Quran.GetUniqueWordCount(2);
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
using AlQuran.SDK.Extensions;

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
src/AlQuran.SDK/           — Main library
tests/AlQuran.SDK.Tests/   — Unit tests (1,000+ tests)
tools/QuranDataFetcher/    — Data generation tool
docs/                      — Documentation
```

## Data Sources

The Quran text data is sourced from the [Al Quran Cloud API](https://alquran.cloud/api), which provides authentic Quran text in multiple editions. The data is embedded as JSON resources within the package.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

---

*This library provides the text of the Holy Quran for educational and reference purposes. Please treat the content with respect.*