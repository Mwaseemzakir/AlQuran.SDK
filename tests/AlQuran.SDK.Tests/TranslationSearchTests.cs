using AlQuran.SDK;
using AlQuran.SDK.Enums;
using AlQuran.SDK.Models;
using FluentAssertions;

namespace AlQuran.SDK.Tests;

public class TranslationSearchTests
{
    #region SearchTranslation (full Quran)

    [Fact]
    public void SearchTranslation_God_Returns_Results()
    {
        var results = Quran.SearchTranslation("God", TranslationEdition.EnglishSaheehInternational);
        results.Should().NotBeEmpty();
        results.Should().AllSatisfy(r =>
        {
            r.Text.Should().ContainEquivalentOf("God", "result text should contain search term");
            r.Edition.Should().Be(TranslationEdition.EnglishSaheehInternational);
            r.SurahNumber.Should().BeInRange(1, 114);
            r.AyahNumber.Should().BeGreaterThan(0);
            r.SurahName.Should().NotBeNullOrWhiteSpace();
        });
    }

    [Fact]
    public void SearchTranslation_CaseInsensitive()
    {
        var lower = Quran.SearchTranslation("mercy", TranslationEdition.EnglishSaheehInternational);
        var upper = Quran.SearchTranslation("MERCY", TranslationEdition.EnglishSaheehInternational);
        lower.Count.Should().Be(upper.Count);
    }

    [Fact]
    public void SearchTranslation_Empty_Returns_Empty()
    {
        Quran.SearchTranslation("", TranslationEdition.EnglishSaheehInternational).Should().BeEmpty();
        Quran.SearchTranslation("   ", TranslationEdition.EnglishSaheehInternational).Should().BeEmpty();
    }

    [Fact]
    public void SearchTranslation_NonExistent_Returns_Empty()
    {
        var results = Quran.SearchTranslation("xyznonexistent123", TranslationEdition.EnglishSaheehInternational);
        results.Should().BeEmpty();
    }

    #endregion

    #region SearchTranslation (specific Surah)

    [Fact]
    public void SearchTranslation_InSurah_Returns_Results()
    {
        var results = Quran.SearchTranslation("Lord", 1, TranslationEdition.EnglishSaheehInternational);
        results.Should().NotBeEmpty();
        results.Should().AllSatisfy(r => r.SurahNumber.Should().Be(1));
    }

    [Fact]
    public void SearchTranslation_InSurah_BySurahName()
    {
        var results = Quran.SearchTranslation("Lord", SurahName.AlFatiha, TranslationEdition.EnglishSaheehInternational);
        results.Should().NotBeEmpty();
        results.Should().AllSatisfy(r => r.SurahNumber.Should().Be(1));
    }

    [Fact]
    public void SearchTranslation_InSurah_InvalidSurah_Throws()
    {
        var act = () => Quran.SearchTranslation("test", 0, TranslationEdition.EnglishSaheehInternational);
        act.Should().Throw<Exception>();
    }

    #endregion

    #region SearchTranslation (Urdu)

    [Theory]
    [InlineData(TranslationEdition.UrduJalandhry)]
    [InlineData(TranslationEdition.UrduJunagarhi)]
    [InlineData(TranslationEdition.UrduMaududi)]
    public void SearchTranslation_Urdu_Returns_Results(TranslationEdition edition)
    {
        // Get the first ayah text and use a substring from it to search
        var firstAyah = Quran.GetTranslation(1, 1, edition);
        var searchTerm = firstAyah.Text.Length > 5 ? firstAyah.Text.Substring(0, 5) : firstAyah.Text;
        var results = Quran.SearchTranslation(searchTerm, edition);
        results.Should().NotBeEmpty($"edition {edition} should have results when searching its own text");
    }

    #endregion

    #region TranslationSearchResult Properties

    [Fact]
    public void TranslationSearchResult_Has_Correct_Properties()
    {
        var results = Quran.SearchTranslation("praise", 1, TranslationEdition.EnglishSaheehInternational);
        results.Should().NotBeEmpty();

        var first = results[0];
        first.SurahNumber.Should().Be(1);
        first.SurahName.Should().NotBeNullOrWhiteSpace();
        first.MatchedText.Should().Be("praise");
        first.Edition.Should().Be(TranslationEdition.EnglishSaheehInternational);
    }

    [Fact]
    public void TranslationSearchResult_ToString_Contains_Reference()
    {
        var results = Quran.SearchTranslation("praise", 1, TranslationEdition.EnglishSaheehInternational);
        results.Should().NotBeEmpty();
        results[0].ToString().Should().Contain("[1:");
    }

    #endregion
}
