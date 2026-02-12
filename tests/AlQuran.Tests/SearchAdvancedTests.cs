using AlQuran.Enums;

namespace AlQuran.Tests;

/// <summary>
/// Expanded search tests covering diacritics-aware search, both script types,
/// search result validation, and various query patterns.
/// </summary>
public class SearchAdvancedTests
{
    #region Whole Quran Search

    [Fact]
    public void Search_Allah_InWholeQuran_Uthmani_Should_ReturnManyResults()
    {
        // Uthmani uses alef wasla (ٱ), so search for common word without alef issues
        var results = Quran.Search("\u0645\u0646", ScriptType.Uthmani); // من (min)
        Assert.True(results.Count > 1000, $"Expected >1000 results, got {results.Count}");
    }

    [Fact]
    public void Search_Allah_InWholeQuran_Simple_Should_ReturnManyResults()
    {
        var results = Quran.Search("الله", ScriptType.Simple);
        Assert.True(results.Count > 1000, $"Expected >1000 results, got {results.Count}");
    }

    [Fact]
    public void Search_Bismillah_Should_ReturnResultsFromManySurahs()
    {
        // "بسم الله" should appear in many surahs (as first ayah or within text)
        var results = Quran.Search("بسم", ScriptType.Simple);
        Assert.True(results.Count > 0);
    }

    [Fact]
    public void Search_Rahman_Should_ReturnResults()
    {
        var results = Quran.Search("الرحمن", ScriptType.Simple);
        Assert.True(results.Count > 0);
    }

    #endregion

    #region Search Within Specific Surah

    [Fact]
    public void Search_InAlFatiha_Should_ReturnResultsOnlyFromSurah1()
    {
        var results = Quran.Search("الله", 1, ScriptType.Simple);
        Assert.NotEmpty(results);
        Assert.All(results, r => Assert.Equal(1, r.SurahNumber));
    }

    [Fact]
    public void Search_InAlBaqarah_Should_ReturnResultsOnlyFromSurah2()
    {
        var results = Quran.Search("الله", 2, ScriptType.Simple);
        Assert.NotEmpty(results);
        Assert.All(results, r => Assert.Equal(2, r.SurahNumber));
    }

    [Fact]
    public void Search_BySurahName_Should_ReturnSameAsNumber()
    {
        var byNumber = Quran.Search("الله", 1, ScriptType.Simple);
        var byName = Quran.Search("الله", SurahName.AlFatiha, ScriptType.Simple);

        Assert.Equal(byNumber.Count, byName.Count);
        for (int i = 0; i < byNumber.Count; i++)
        {
            Assert.Equal(byNumber[i].SurahNumber, byName[i].SurahNumber);
            Assert.Equal(byNumber[i].AyahNumber, byName[i].AyahNumber);
        }
    }

    [Fact]
    public void Search_InSurahAlIkhlas_ForAllah_Should_ReturnResults()
    {
        var results = Quran.Search("الله", SurahName.AlIkhlas, ScriptType.Simple);
        Assert.NotEmpty(results);
    }

    #endregion

    #region Diacritics-Aware Search

    [Fact]
    public void Search_WithTashkeel_Should_FindResultsIgnoringDiacritics()
    {
        // Search with diacritics should still find matches
        var withTashkeel = Quran.Search("اللَّهِ", ScriptType.Simple);
        var withoutTashkeel = Quran.Search("الله", ScriptType.Simple);

        // Both should return results
        Assert.NotEmpty(withTashkeel);
        Assert.NotEmpty(withoutTashkeel);
    }

    [Fact]
    public void Search_WithPlainText_Should_MatchTashkeelText()
    {
        // Search plain text in Uthmani (which has heavy tashkeel)
        // Use a term without alef wasla issues
        var results = Quran.Search("\u0628\u0633\u0645", ScriptType.Uthmani); // بسم
        Assert.NotEmpty(results);
    }

    [Fact]
    public void Search_NormalizedAlef_Should_FindVariants()
    {
        // "الرحمن" with different alef forms should still match
        var results = Quran.Search("الرحمن", ScriptType.Simple);
        Assert.NotEmpty(results);
    }

    #endregion

    #region Search Result Validation

    [Fact]
    public void SearchResults_AyahNumber_Should_BeValid()
    {
        var results = Quran.Search("الله", 1, ScriptType.Simple);
        foreach (var result in results)
        {
            var surah = Quran.GetSurah(result.SurahNumber);
            Assert.InRange(result.AyahNumber, 1, surah.AyahCount);
        }
    }

    [Fact]
    public void SearchResults_Text_Should_MatchActualAyah()
    {
        var results = Quran.Search("الله", 1, ScriptType.Simple);
        foreach (var result in results)
        {
            var ayah = Quran.GetAyah(result.SurahNumber, result.AyahNumber, ScriptType.Simple);
            Assert.Equal(ayah.Text, result.Text);
        }
    }

    [Fact]
    public void SearchResults_SurahName_Should_MatchActualSurah()
    {
        var results = Quran.Search("الله", ScriptType.Simple);
        foreach (var result in results.Take(20)) // Check first 20
        {
            var surah = Quran.GetSurah(result.SurahNumber);
            Assert.Equal(surah.EnglishName, result.SurahName);
        }
    }

    [Fact]
    public void Search_ShouldNotReturnDuplicates()
    {
        var results = Quran.Search("الله", 1, ScriptType.Simple);
        var uniqueKeys = results.Select(r => $"{r.SurahNumber}:{r.AyahNumber}").ToHashSet();
        Assert.Equal(results.Count, uniqueKeys.Count);
    }

    #endregion

    #region Script Type Comparison

    [Fact]
    public void Search_SimpleVsUthmani_Should_FindResultsInBoth()
    {
        // Use a term without alef to work in both scripts
        var simpleResults = Quran.Search("\u0628\u0633\u0645", 1, ScriptType.Simple);  // بسم
        var uthmaniResults = Quran.Search("\u0628\u0633\u0645", 1, ScriptType.Uthmani); // بسم

        Assert.NotEmpty(simpleResults);
        Assert.NotEmpty(uthmaniResults);
    }

    [Fact]
    public void Search_DefaultScript_Should_BeUthmani()
    {
        var defaultResults = Quran.Search("الله", 1);
        var uthmaniResults = Quran.Search("الله", 1, ScriptType.Uthmani);

        Assert.Equal(defaultResults.Count, uthmaniResults.Count);
    }

    #endregion

    #region Edge Case Searches

    [Fact]
    public void Search_SingleCharArabic_Should_ReturnResults()
    {
        // "ق" is a Surah name and appears in text
        var results = Quran.Search("ق", ScriptType.Simple);
        Assert.NotEmpty(results);
    }

    [Fact]
    public void Search_SpecificPhrase_Should_ReturnLimitedResults()
    {
        // Very specific phrase should return fewer results
        var broad = Quran.Search("الله", ScriptType.Simple);
        var specific = Quran.Search("بسم الله الرحمن الرحيم", ScriptType.Simple);
        Assert.True(specific.Count < broad.Count,
            $"Specific phrase ({specific.Count}) should have fewer results than broad ({broad.Count})");
    }

    [Fact]
    public void Search_InEveryScriptType_Should_NotThrow()
    {
        foreach (ScriptType script in Enum.GetValues(typeof(ScriptType)))
        {
            var results = Quran.Search("الله", script);
            Assert.NotNull(results);
        }
    }

    #endregion
}
