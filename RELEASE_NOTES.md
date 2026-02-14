# Release Notes

## GZip Compressed Embedded Resources

- **Compressed JSON Resources** — All 11 embedded JSON files (Quran text + translations) replaced with GZip-compressed `.json.gz` equivalents, significantly reducing NuGet package size
- QuranTextProvider — Updated to decompress `.gz` streams at runtime using `GZipStream`
- TranslationProvider — Updated to load translations from `.gz` resources with on-the-fly decompression
- EmbeddedResource include pattern changed from `*.json` to `*.json.gz` in the project file
- Zero API changes — Fully backward-compatible, no consumer code changes required

## Initial Release

- **Complete Quran Data** — All 114 Surahs, 6,236 Ayahs, 30 Juz, and 15 Sajda positions
- **Dual Script Support** — Both Uthmani (with tashkeel) and Simple Arabic scripts
- **9 Built-in Translations** — 6 English + 3 Urdu translations embedded offline
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
- **1,000+ Unit Tests** — Comprehensive test coverage
