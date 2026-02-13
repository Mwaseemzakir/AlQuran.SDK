using AlQuran.SDK;
using AlQuran.SDK.Enums;
using AlQuran.SDK.Exceptions;
using AlQuran.SDK.Models;
using FluentAssertions;

namespace AlQuran.SDK.Tests;

public class TranslationTests
{
    #region GetAvailableTranslations

    [Fact]
    public void GetAvailableTranslations_Returns_All_Editions()
    {
        var translations = Quran.GetAvailableTranslations();
        translations.Should().HaveCount(9);
    }

    [Fact]
    public void GetAvailableTranslations_Contains_English_Editions()
    {
        var translations = Quran.GetAvailableTranslations("English");
        translations.Should().HaveCountGreaterThanOrEqualTo(6);
        translations.Should().Contain(t => t.Edition == TranslationEdition.EnglishSaheehInternational);
        translations.Should().Contain(t => t.Edition == TranslationEdition.EnglishTransliteration);
    }

    [Fact]
    public void GetAvailableTranslations_Contains_Urdu_Editions()
    {
        var translations = Quran.GetAvailableTranslations("Urdu");
        translations.Should().HaveCount(3);
        translations.Should().Contain(t => t.Edition == TranslationEdition.UrduJalandhry);
        translations.Should().Contain(t => t.Edition == TranslationEdition.UrduJunagarhi);
        translations.Should().Contain(t => t.Edition == TranslationEdition.UrduMaududi);
    }

    [Fact]
    public void GetAvailableTranslations_CaseInsensitive_Language_Filter()
    {
        var lower = Quran.GetAvailableTranslations("english");
        var upper = Quran.GetAvailableTranslations("ENGLISH");
        lower.Should().HaveCount(upper.Count);
    }

    [Fact]
    public void GetAvailableTranslations_Empty_Language_Returns_Empty()
    {
        Quran.GetAvailableTranslations("").Should().BeEmpty();
        Quran.GetAvailableTranslations("   ").Should().BeEmpty();
    }

    [Fact]
    public void GetAvailableTranslations_Unknown_Language_Returns_Empty()
    {
        Quran.GetAvailableTranslations("Klingon").Should().BeEmpty();
    }

    #endregion

    #region TranslationInfo Properties

    [Fact]
    public void TranslationInfo_Has_Correct_Properties()
    {
        var translations = Quran.GetAvailableTranslations();
        var sahih = translations.First(t => t.Edition == TranslationEdition.EnglishSaheehInternational);

        sahih.Name.Should().Be("Saheeh International");
        sahih.Language.Should().Be("English");
        sahih.Direction.Should().Be("ltr");
        sahih.Type.Should().Be("translation");
    }

    [Fact]
    public void TranslationInfo_Transliteration_Has_Correct_Type()
    {
        var translations = Quran.GetAvailableTranslations();
        var translit = translations.First(t => t.Edition == TranslationEdition.EnglishTransliteration);

        translit.Type.Should().Be("transliteration");
        translit.Name.Should().Contain("Transliteration");
    }

    [Fact]
    public void TranslationInfo_Urdu_Has_RTL_Direction()
    {
        var urduTranslations = Quran.GetAvailableTranslations("Urdu");
        urduTranslations.Should().AllSatisfy(t => t.Direction.Should().Be("rtl"));
    }

    #endregion

    #region IsTranslationAvailable

    [Theory]
    [InlineData(TranslationEdition.EnglishSaheehInternational)]
    [InlineData(TranslationEdition.EnglishYusufAli)]
    [InlineData(TranslationEdition.EnglishPickthall)]
    [InlineData(TranslationEdition.EnglishClearQuran)]
    [InlineData(TranslationEdition.EnglishMaududi)]
    [InlineData(TranslationEdition.EnglishTransliteration)]
    [InlineData(TranslationEdition.UrduJalandhry)]
    [InlineData(TranslationEdition.UrduJunagarhi)]
    [InlineData(TranslationEdition.UrduMaududi)]
    public void IsTranslationAvailable_Returns_True_For_All_Editions(TranslationEdition edition)
    {
        Quran.IsTranslationAvailable(edition).Should().BeTrue();
    }

    #endregion

    #region GetTranslation (single ayah)

    [Theory]
    [InlineData(TranslationEdition.EnglishSaheehInternational)]
    [InlineData(TranslationEdition.EnglishYusufAli)]
    [InlineData(TranslationEdition.EnglishPickthall)]
    [InlineData(TranslationEdition.EnglishClearQuran)]
    [InlineData(TranslationEdition.EnglishMaududi)]
    public void GetTranslation_Fatiha_FirstAyah_Returns_NonEmpty_English(TranslationEdition edition)
    {
        var ayah = Quran.GetTranslation(1, 1, edition);
        ayah.SurahNumber.Should().Be(1);
        ayah.AyahNumber.Should().Be(1);
        ayah.Text.Should().NotBeNullOrWhiteSpace();
        ayah.Edition.Should().Be(edition);
    }

    [Fact]
    public void GetTranslation_Saheeh_Fatiha_Contains_Bismillah_Reference()
    {
        var ayah = Quran.GetTranslation(1, 1, TranslationEdition.EnglishSaheehInternational);
        ayah.Text.Should().ContainAny("name", "Allah", "God", "Merciful", "Gracious");
    }

    [Fact]
    public void GetTranslation_AyatulKursi_Has_Content()
    {
        var ayah = Quran.GetTranslation(2, 255, TranslationEdition.EnglishSaheehInternational);
        ayah.SurahNumber.Should().Be(2);
        ayah.AyahNumber.Should().Be(255);
        ayah.Text.Should().NotBeNullOrWhiteSpace();
        ayah.Text.Length.Should().BeGreaterThan(50); // Ayat al-Kursi is long
    }

    [Fact]
    public void GetTranslation_By_SurahName_Enum()
    {
        var ayah = Quran.GetTranslation(SurahName.AlIkhlas, 1, TranslationEdition.EnglishSaheehInternational);
        ayah.SurahNumber.Should().Be(112);
        ayah.AyahNumber.Should().Be(1);
        ayah.Text.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void GetTranslation_InvalidSurah_Throws()
    {
        var act = () => Quran.GetTranslation(0, 1, TranslationEdition.EnglishSaheehInternational);
        act.Should().Throw<SurahNotFoundException>();
    }

    [Fact]
    public void GetTranslation_InvalidAyah_Throws()
    {
        var act = () => Quran.GetTranslation(1, 999, TranslationEdition.EnglishSaheehInternational);
        act.Should().Throw<AyahNotFoundException>();
    }

    #endregion

    #region GetTranslation (Urdu)

    [Theory]
    [InlineData(TranslationEdition.UrduJalandhry)]
    [InlineData(TranslationEdition.UrduJunagarhi)]
    [InlineData(TranslationEdition.UrduMaududi)]
    public void GetTranslation_Urdu_Fatiha_Returns_NonEmpty(TranslationEdition edition)
    {
        var ayah = Quran.GetTranslation(1, 1, edition);
        ayah.Text.Should().NotBeNullOrWhiteSpace();
        ayah.Edition.Should().Be(edition);
    }

    #endregion

    #region GetTranslation (Transliteration)

    [Fact]
    public void GetTranslation_Transliteration_Contains_Latin_Script()
    {
        var ayah = Quran.GetTranslation(1, 1, TranslationEdition.EnglishTransliteration);
        ayah.Text.Should().NotBeNullOrWhiteSpace();
        // Transliteration should contain Latin characters
        ayah.Text.Should().MatchRegex("[a-zA-Z]");
    }

    [Fact]
    public void GetTranslation_Transliteration_AllSurahs_Have_Data()
    {
        for (int i = 1; i <= 114; i++)
        {
            var ayahs = Quran.GetTranslations(i, TranslationEdition.EnglishTransliteration);
            ayahs.Should().NotBeEmpty($"Surah {i} should have transliteration data");
        }
    }

    #endregion

    #region GetTranslations (all ayahs for surah)

    [Fact]
    public void GetTranslations_Fatiha_Returns_7_Ayahs()
    {
        var ayahs = Quran.GetTranslations(1, TranslationEdition.EnglishSaheehInternational);
        ayahs.Should().HaveCount(7);
        ayahs.Should().AllSatisfy(a =>
        {
            a.SurahNumber.Should().Be(1);
            a.Text.Should().NotBeNullOrWhiteSpace();
            a.Edition.Should().Be(TranslationEdition.EnglishSaheehInternational);
        });
    }

    [Fact]
    public void GetTranslations_Baqarah_Returns_286_Ayahs()
    {
        var ayahs = Quran.GetTranslations(2, TranslationEdition.EnglishSaheehInternational);
        ayahs.Should().HaveCount(286);
    }

    [Fact]
    public void GetTranslations_By_SurahName_Enum()
    {
        var ayahs = Quran.GetTranslations(SurahName.AlFatiha, TranslationEdition.EnglishSaheehInternational);
        ayahs.Should().HaveCount(7);
    }

    [Fact]
    public void GetTranslations_InvalidSurah_Throws()
    {
        var act = () => Quran.GetTranslations(200, TranslationEdition.EnglishSaheehInternational);
        act.Should().Throw<SurahNotFoundException>();
    }

    #endregion

    #region GetTranslations (range)

    [Fact]
    public void GetTranslations_Range_Returns_Correct_Subset()
    {
        var ayahs = Quran.GetTranslations(2, 1, 5, TranslationEdition.EnglishSaheehInternational);
        ayahs.Should().HaveCount(5);
        ayahs.Select(a => a.AyahNumber).Should().ContainInOrder(1, 2, 3, 4, 5);
    }

    [Fact]
    public void GetTranslations_Range_Single_Ayah()
    {
        var ayahs = Quran.GetTranslations(2, 255, 255, TranslationEdition.EnglishSaheehInternational);
        ayahs.Should().HaveCount(1);
        ayahs[0].AyahNumber.Should().Be(255);
    }

    #endregion

    #region All Editions Have Complete Data

    [Theory]
    [InlineData(TranslationEdition.EnglishSaheehInternational)]
    [InlineData(TranslationEdition.EnglishYusufAli)]
    [InlineData(TranslationEdition.EnglishPickthall)]
    [InlineData(TranslationEdition.EnglishClearQuran)]
    [InlineData(TranslationEdition.EnglishMaududi)]
    [InlineData(TranslationEdition.EnglishTransliteration)]
    [InlineData(TranslationEdition.UrduJalandhry)]
    [InlineData(TranslationEdition.UrduJunagarhi)]
    [InlineData(TranslationEdition.UrduMaududi)]
    public void AllEditions_Have_6236_Total_Ayahs(TranslationEdition edition)
    {
        int total = 0;
        for (int s = 1; s <= 114; s++)
        {
            var ayahs = Quran.GetTranslations(s, edition);
            total += ayahs.Count;
        }
        total.Should().Be(6236);
    }

    [Theory]
    [InlineData(TranslationEdition.EnglishSaheehInternational)]
    [InlineData(TranslationEdition.EnglishYusufAli)]
    [InlineData(TranslationEdition.UrduJalandhry)]
    public void AllEditions_Last_Ayah_Is_AnNas_6(TranslationEdition edition)
    {
        var ayahs = Quran.GetTranslations(114, edition);
        ayahs.Should().HaveCount(6);
        ayahs.Last().AyahNumber.Should().Be(6);
        ayahs.Last().Text.Should().NotBeNullOrWhiteSpace();
    }

    #endregion

    #region TranslatedAyah ToString

    [Fact]
    public void TranslatedAyah_ToString_Contains_Reference()
    {
        var ayah = Quran.GetTranslation(1, 1, TranslationEdition.EnglishSaheehInternational);
        var str = ayah.ToString();
        str.Should().StartWith("[1:1]");
    }

    #endregion
}
