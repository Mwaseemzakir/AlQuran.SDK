using AlQuran.SDK.Enums;

namespace AlQuran.SDK.Tests;

public class SearchTests
{
    [Fact]
    public void Search_WithEmptyTerm_Should_ReturnEmptyList()
    {
        var results = Quran.Search("");
        Assert.Empty(results);
    }

    [Fact]
    public void Search_WithNullTerm_Should_ReturnEmptyList()
    {
        var results = Quran.Search(null!);
        Assert.Empty(results);
    }

    [Fact]
    public void Search_WithWhitespaceTerm_Should_ReturnEmptyList()
    {
        var results = Quran.Search("   ");
        Assert.Empty(results);
    }

    [Fact]
    public void Search_WithValidArabicText_Should_ReturnResults()
    {
        // Search for "الله" (Allah) which appears throughout the Quran
        var results = Quran.Search("الله", ScriptType.Simple);
        Assert.True(results.Count > 0);
    }

    [Fact]
    public void Search_InSpecificSurah_Should_ReturnResultsFromThatSurah()
    {
        // Search within Al-Fatiha only
        var results = Quran.Search("الله", 1, ScriptType.Simple);
        Assert.All(results, r => Assert.Equal(1, r.SurahNumber));
    }

    [Fact]
    public void Search_InSpecificSurah_BySurahName_Should_Work()
    {
        var results = Quran.Search("الله", SurahName.AlFatiha, ScriptType.Simple);
        Assert.All(results, r => Assert.Equal(1, r.SurahNumber));
    }

    [Fact]
    public void Search_InInvalidSurah_Should_ThrowSurahNotFoundException()
    {
        Assert.Throws<Exceptions.SurahNotFoundException>(
            () => Quran.Search("test", 0));
    }

    [Fact]
    public void SearchResult_Should_HaveCorrectProperties()
    {
        var results = Quran.Search("الله", 1, ScriptType.Simple);

        if (results.Count > 0)
        {
            var result = results[0];
            Assert.Equal(1, result.SurahNumber);
            Assert.True(result.AyahNumber > 0);
            Assert.False(string.IsNullOrWhiteSpace(result.Text));
            Assert.False(string.IsNullOrWhiteSpace(result.SurahName));
            Assert.False(string.IsNullOrWhiteSpace(result.MatchedText));
        }
    }

    [Fact]
    public void SearchResult_ToString_Should_ContainReference()
    {
        var results = Quran.Search("الله", 1, ScriptType.Simple);

        if (results.Count > 0)
        {
            var str = results[0].ToString();
            Assert.Contains("[1:", str);
        }
    }
}
