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

The primary API for accessing all Quran data. All methods are static — no instantiation needed.

### Constants

| Constant | Value | Description |
|----------|-------|-------------|
| `Quran.TotalSurahs` | 114 | Total number of Surahs |
| `Quran.TotalAyahs` | 6236 | Total number of Ayahs |
| `Quran.TotalJuz` | 30 | Total number of Juz |
| `Quran.TotalSajdas` | 15 | Total Sajda positions |
| `Quran.TotalPages` | 604 | Total pages in the Madani Mushaf |
| `Quran.TotalHizbQuarters` | 240 | Total Hizb quarters |
| `Quran.TotalManzils` | 7 | Total Manzil (weekly) divisions |

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
| `GetAyah(string, ScriptType)` | `Ayah` | Get Ayah by reference string (e.g., "2:255") |
| `GetAyahs(int, ScriptType)` | `List<Ayah>` | Get all Ayahs in a Surah |
| `GetAyahs(SurahName, ScriptType)` | `List<Ayah>` | Get all Ayahs by Surah name |
| `GetAyahs(int, int, int, ScriptType)` | `List<Ayah>` | Get Ayah range |
| `GetAyahRange(string, ScriptType)` | `List<Ayah>` | Get Ayahs by reference ("2:1-5") |
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

### Search Methods (Arabic)

| Method | Returns | Description |
|--------|---------|-------------|
| `Search(string, ScriptType)` | `List<SearchResult>` | Search entire Quran |
| `Search(string, int, ScriptType)` | `List<SearchResult>` | Search within a Surah |
| `Search(string, SurahName, ScriptType)` | `List<SearchResult>` | Search by Surah name |

### Translation Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `GetTranslation(int, int, TranslationEdition)` | `TranslatedAyah` | Get translated Ayah |
| `GetTranslation(SurahName, int, TranslationEdition)` | `TranslatedAyah` | Get translated Ayah by name |
| `GetTranslations(int, TranslationEdition)` | `List<TranslatedAyah>` | Get all translations for a Surah |
| `GetTranslations(SurahName, TranslationEdition)` | `List<TranslatedAyah>` | Get translations by Surah name |
| `GetTranslations(int, int, int, TranslationEdition)` | `List<TranslatedAyah>` | Get translation range |
| `GetAvailableTranslations()` | `List<TranslationInfo>` | List all translation editions |
| `GetAvailableTranslations(string)` | `List<TranslationInfo>` | Filter editions by language |
| `IsTranslationAvailable(TranslationEdition)` | `bool` | Check if edition is available |

### Translation Search Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `SearchTranslation(string, TranslationEdition)` | `List<TranslationSearchResult>` | Search entire Quran in translation |
| `SearchTranslation(string, int, TranslationEdition)` | `List<TranslationSearchResult>` | Search within a Surah |
| `SearchTranslation(string, SurahName, TranslationEdition)` | `List<TranslationSearchResult>` | Search by Surah name |

### Page & Hizb Navigation Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `GetAyahsByPage(int, ScriptType)` | `List<Ayah>` | Get Ayahs on a Mushaf page (1-604) |
| `GetPageNumber(int, int, ScriptType)` | `int` | Get page number for Surah:Ayah |
| `GetAyahsByHizbQuarter(int, ScriptType)` | `List<Ayah>` | Get Ayahs in a Hizb quarter (1-240) |
| `GetHizbQuarter(int, int, ScriptType)` | `int` | Get Hizb quarter for Surah:Ayah |

### Manzil Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `GetManzil(int)` | `Manzil` | Get Manzil by number (1-7) |
| `GetManzilOrDefault(int)` | `Manzil?` | Get Manzil or null |
| `GetAllManzils()` | `List<Manzil>` | Get all 7 Manzils |
| `GetManzilNumber(int, int)` | `int` | Get Manzil number for Surah:Ayah |
| `GetAyahsByManzil(int, ScriptType)` | `List<Ayah>` | Get all Ayahs in a Manzil |
| `IsValidManzil(int)` | `bool` | Check if Manzil number is valid |

### Verse Reference Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `ParseVerseReference(string)` | `VerseReference` | Parse "2:255" or "2:1-5" |

### Random & Daily Ayah Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `GetRandomAyah(ScriptType)` | `Ayah` | Get a random Ayah |
| `GetRandomAyah(int, ScriptType)` | `Ayah` | Get a random Ayah from a Surah |
| `GetAyahOfTheDay(ScriptType)` | `Ayah` | Get deterministic daily Ayah (UTC) |
| `GetAyahOfTheDay(DateTime, ScriptType)` | `Ayah` | Get Ayah of the Day for a date |

### Muqatta'at Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `GetMuqattaat()` | `List<MuqattaatSurah>` | Get all 29 Muqatta'at Surahs |
| `HasMuqattaat(int)` | `bool` | Check if Surah has Muqatta'at |
| `GetMuqattaatForSurah(int)` | `MuqattaatSurah?` | Get Muqatta'at info for Surah |

### Statistics Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `GetWordCount(int, ScriptType)` | `int` | Word count for a Surah |
| `GetWordCount(SurahName, ScriptType)` | `int` | Word count by Surah name |
| `GetLetterCount(int, ScriptType)` | `int` | Letter count for a Surah |
| `GetTotalWordCount(ScriptType)` | `int` | Total words in the Quran |
| `GetTotalLetterCount(ScriptType)` | `int` | Total letters in the Quran |
| `GetUniqueWordCount(ScriptType)` | `int` | Unique words in the Quran |
| `GetUniqueWordCount(int, ScriptType)` | `int` | Unique words in a Surah |

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

### `TranslatedAyah`

| Property | Type | Description |
|----------|------|-------------|
| `SurahNumber` | `int` | Surah number |
| `AyahNumber` | `int` | Verse number |
| `Text` | `string` | Translated text |
| `Edition` | `TranslationEdition` | Translation edition |

### `TranslationInfo`

| Property | Type | Description |
|----------|------|-------------|
| `Edition` | `TranslationEdition` | Edition enum value |
| `Name` | `string` | Display name |
| `AuthorName` | `string` | Author/translator name |
| `Language` | `string` | Language (English, Urdu) |
| `Direction` | `string` | Text direction (ltr, rtl) |
| `Type` | `string` | Type (translation, transliteration) |

### `TranslationSearchResult`

| Property | Type | Description |
|----------|------|-------------|
| `SurahNumber` | `int` | Surah number |
| `AyahNumber` | `int` | Ayah number |
| `Text` | `string` | Full translated text |
| `SurahName` | `string` | Surah English name |
| `MatchedText` | `string` | The matched text |
| `Edition` | `TranslationEdition` | Translation edition |

### `Juz`

| Property | Type | Description |
|----------|------|-------------|
| `Number` | `int` | Juz number (1-30) |
| `StartSurah` | `int` | Starting Surah |
| `StartAyah` | `int` | Starting Ayah |
| `EndSurah` | `int` | Ending Surah |
| `EndAyah` | `int` | Ending Ayah |
| `ArabicName` | `string` | First words of Juz |

### `Manzil`

| Property | Type | Description |
|----------|------|-------------|
| `Number` | `int` | Manzil number (1-7) |
| `StartSurah` | `int` | Starting Surah |
| `StartAyah` | `int` | Starting Ayah |
| `EndSurah` | `int` | Ending Surah |
| `EndAyah` | `int` | Ending Ayah |

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

### `VerseReference`

| Property | Type | Description |
|----------|------|-------------|
| `SurahNumber` | `int` | Surah number (1-114) |
| `AyahNumber` | `int` | Starting Ayah number |
| `EndAyahNumber` | `int?` | Ending Ayah (null for single verse) |
| `IsRange` | `bool` | Whether it's a range reference |

| Static Method | Returns | Description |
|---------------|---------|-------------|
| `Parse(string)` | `VerseReference` | Parse "2:255" or "2:1-5" |
| `TryParse(string, out VerseReference?)` | `bool` | Try parse without exception |

### `MuqattaatSurah`

| Property | Type | Description |
|----------|------|-------------|
| `SurahNumber` | `int` | Surah number |
| `Letters` | `string` | English transliteration (e.g., "Alif Lam Mim") |
| `ArabicLetters` | `string` | Arabic letters (e.g., "الم") |

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

### `TranslationEdition`

| Value | Description |
|-------|-------------|
| `EnglishSaheehInternational` | Saheeh International (English) |
| `EnglishYusufAli` | Abdullah Yusuf Ali (English) |
| `EnglishPickthall` | Pickthall (English) |
| `EnglishClearQuran` | Talal Itani / Clear Quran (English) |
| `EnglishMaududi` | Abul Ala Maududi (English) |
| `EnglishTransliteration` | Transliteration (English) |
| `UrduJalandhry` | Fateh Muhammad Jalandhry (Urdu) |
| `UrduJunagarhi` | Muhammad Junagarhi (Urdu) |
| `UrduMaududi` | Abul Ala Maududi (Urdu) |

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
| `TranslationNotFoundException` | Translation data not available |
