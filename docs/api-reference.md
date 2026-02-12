# AlQuran.SDK - API Reference

## Table of Contents

- [Main Entry Point: `Quran` Static Class](#main-entry-point)
- [Models](#models)
- [Enums](#enums)
- [Extensions](#extensions)
- [Exceptions](#exceptions)

---

## Main Entry Point

### `AlQuran.SDK.Quran` (Static Class)

The primary API for accessing all Quran data.

### Constants

| Constant | Value | Description |
|----------|-------|-------------|
| `Quran.TotalSurahs` | 114 | Total number of Surahs |
| `Quran.TotalAyahs` | 6236 | Total number of Ayahs |
| `Quran.TotalJuz` | 30 | Total number of Juz |
| `Quran.TotalSajdas` | 15 | Total Sajda positions |

### Bismillah

| Method | Returns | Description |
|--------|---------|-------------|
| `GetBismillah(ScriptType)` | `string` | Gets Bismillah text in specified script |

### Surah Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `GetSurah(int)` | `Surah` | Get Surah by number (1-114). Throws `SurahNotFoundException` |
| `GetSurah(SurahName)` | `Surah` | Get Surah by enum value |
| `GetSurah(string)` | `Surah` | Get Surah by English name (case-insensitive) |
| `GetSurahOrDefault(int)` | `Surah?` | Get Surah or null |
| `GetSurahOrDefault(string)` | `Surah?` | Get Surah by name or null |
| `GetSurahByArabicName(string)` | `Surah` | Get Surah by Arabic name |
| `GetAllSurahs()` | `List<Surah>` | Get all 114 Surahs |
| `GetMeccanSurahs()` | `List<Surah>` | Get all Meccan Surahs |
| `GetMedinanSurahs()` | `List<Surah>` | Get all Medinan Surahs |
| `GetSurahsByRevelationOrder()` | `List<Surah>` | Surahs in revelation order |
| `GetSurahsByJuz(int)` | `List<Surah>` | Surahs in a specific Juz |
| `IsValidSurah(int)` | `bool` | Check if Surah number is valid |
| `IsValidSurah(string)` | `bool` | Check if Surah name is valid |

### Ayah Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `GetAyah(int, int, ScriptType)` | `Ayah` | Get specific Ayah |
| `GetAyah(SurahName, int, ScriptType)` | `Ayah` | Get Ayah by Surah name |
| `GetAyahs(int, ScriptType)` | `List<Ayah>` | Get all Ayahs in a Surah |
| `GetAyahs(SurahName, ScriptType)` | `List<Ayah>` | Get all Ayahs by Surah name |
| `GetAyahs(int, int, int, ScriptType)` | `List<Ayah>` | Get Ayah range |
| `IsTextDataAvailable(ScriptType)` | `bool` | Check if text data is loaded |
| `GetAyahCount(int)` | `int` | Get Ayah count for Surah |
| `GetAyahCount(SurahName)` | `int` | Get Ayah count by name |

### Juz Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `GetJuz(int)` | `Juz` | Get Juz by number (1-30) |
| `GetJuzOrDefault(int)` | `Juz?` | Get Juz or null |
| `GetAllJuz()` | `List<Juz>` | Get all 30 Juz |
| `GetJuzNumber(int, int)` | `int` | Get Juz number for a Surah:Ayah |
| `IsValidJuz(int)` | `bool` | Check if Juz number is valid |

### Sajda Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `GetAllSajdas()` | `List<SajdaVerse>` | Get all 15 Sajda positions |
| `GetObligatorySajdas()` | `List<SajdaVerse>` | Get obligatory Sajdas |
| `GetRecommendedSajdas()` | `List<SajdaVerse>` | Get recommended Sajdas |
| `IsSajdaAyah(int, int)` | `bool` | Check if Ayah has Sajda |
| `GetSajda(int, int)` | `SajdaVerse?` | Get Sajda info for Ayah |

### Search Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `Search(string, ScriptType)` | `List<SearchResult>` | Search entire Quran |
| `Search(string, int, ScriptType)` | `List<SearchResult>` | Search within a Surah |
| `Search(string, SurahName, ScriptType)` | `List<SearchResult>` | Search by Surah name |

---

## Models

### `Surah`

| Property | Type | Description |
|----------|------|-------------|
| `Number` | `int` | Surah number (1-114) |
| `ArabicName` | `string` | Arabic name |
| `EnglishName` | `string` | Transliterated name |
| `EnglishMeaning` | `string` | English meaning |
| `AyahCount` | `int` | Number of verses |
| `RevelationType` | `RevelationType` | Meccan or Medinan |
| `RevelationOrder` | `int` | Chronological order |
| `RukuCount` | `int` | Number of Rukus |
| `JuzStart` | `int` | Starting Juz |
| `JuzEnd` | `int` | Ending Juz |
| `PageStart` | `int` | Page in Madani Mushaf |
| `HasBismillah` | `bool` | Has Bismillah prefix |
| `Name` | `SurahName` | Enum value |

### `Ayah`

| Property | Type | Description |
|----------|------|-------------|
| `SurahNumber` | `int` | Surah number |
| `AyahNumber` | `int` | Verse number |
| `Text` | `string` | Arabic text |
| `Juz` | `int` | Juz number |
| `Page` | `int` | Page number |
| `HizbQuarter` | `int` | Hizb quarter |
| `HasSajda` | `bool` | Has Sajda mark |

### `Juz`

| Property | Type | Description |
|----------|------|-------------|
| `Number` | `int` | Juz number (1-30) |
| `StartSurah` | `int` | Starting Surah |
| `StartAyah` | `int` | Starting Ayah |
| `EndSurah` | `int` | Ending Surah |
| `EndAyah` | `int` | Ending Ayah |
| `ArabicName` | `string` | First words of Juz |

### `SajdaVerse`

| Property | Type | Description |
|----------|------|-------------|
| `Number` | `int` | Sajda number (1-15) |
| `SurahNumber` | `int` | Surah number |
| `AyahNumber` | `int` | Ayah number |
| `Type` | `SajdaType` | Obligatory or Recommended |

### `SearchResult`

| Property | Type | Description |
|----------|------|-------------|
| `SurahNumber` | `int` | Surah number |
| `AyahNumber` | `int` | Ayah number |
| `Text` | `string` | Full Ayah text |
| `SurahName` | `string` | Surah English name |
| `MatchedText` | `string` | The matched text |

---

## Enums

### `ScriptType`

| Value | Description |
|-------|-------------|
| `Simple` | Plain Arabic without diacritics |
| `Uthmani` | Full Uthmani script with tashkeel |

### `RevelationType`

| Value | Description |
|-------|-------------|
| `Meccan` | Revealed in Mecca |
| `Medinan` | Revealed in Medina |

### `SajdaType`

| Value | Description |
|-------|-------------|
| `Obligatory` | Mandatory prostration |
| `Recommended` | Recommended prostration |

### `SurahName`

Enum with all 114 Surah names (e.g., `SurahName.AlFatiha`, `SurahName.AlBaqarah`, ..., `SurahName.AnNas`).

---

## Extensions

### `ArabicTextExtensions`

| Method | Description |
|--------|-------------|
| `RemoveTashkeel(this string)` | Remove diacritical marks |
| `RemoveTashkeelWithMapping(this string, out Dictionary)` | Remove tashkeel with index mapping |
| `NormalizeAlef(this string)` | Normalize Alef forms |
| `NormalizeForSearch(this string)` | Full normalization for search |
| `HighlightMatch(this string, string, Func)` | Highlight matches |
| `IsTashkeelChar(char)` | Check if char is tashkeel |

---

## Exceptions

| Exception | Thrown When |
|-----------|------------|
| `SurahNotFoundException` | Invalid Surah number or name |
| `AyahNotFoundException` | Invalid Ayah number |
| `JuzNotFoundException` | Invalid Juz number |
