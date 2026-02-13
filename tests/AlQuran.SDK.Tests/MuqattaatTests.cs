using AlQuran.SDK;
using AlQuran.SDK.Models;
using FluentAssertions;

namespace AlQuran.SDK.Tests;

public class MuqattaatTests
{
    #region GetMuqattaat

    [Fact]
    public void GetMuqattaat_Returns_29_Surahs()
    {
        var muqattaat = Quran.GetMuqattaat();
        muqattaat.Should().HaveCount(29);
    }

    [Fact]
    public void GetMuqattaat_Returns_New_List_Instance()
    {
        var list1 = Quran.GetMuqattaat();
        var list2 = Quran.GetMuqattaat();
        list1.Should().NotBeSameAs(list2);
    }

    [Fact]
    public void GetMuqattaat_Contains_All_Known_Surahs()
    {
        var muqattaat = Quran.GetMuqattaat();
        var expectedSurahs = new[] { 2, 3, 7, 10, 11, 12, 13, 14, 15, 19, 20, 26, 27, 28, 29, 30, 31, 32, 36, 38, 40, 41, 42, 43, 44, 45, 46, 50, 68 };
        muqattaat.Select(m => m.SurahNumber).Should().BeEquivalentTo(expectedSurahs);
    }

    [Fact]
    public void GetMuqattaat_All_Have_NonEmpty_Letters()
    {
        var muqattaat = Quran.GetMuqattaat();
        muqattaat.Should().AllSatisfy(m =>
        {
            m.Letters.Should().NotBeNullOrWhiteSpace();
            m.ArabicLetters.Should().NotBeNullOrWhiteSpace();
        });
    }

    [Fact]
    public void GetMuqattaat_Surah_Numbers_Are_In_Ascending_Order()
    {
        var muqattaat = Quran.GetMuqattaat();
        var numbers = muqattaat.Select(m => m.SurahNumber).ToList();
        numbers.Should().BeInAscendingOrder();
    }

    #endregion

    #region HasMuqattaat

    [Theory]
    [InlineData(2, true)]
    [InlineData(3, true)]
    [InlineData(7, true)]
    [InlineData(36, true)]  // Ya-Sin
    [InlineData(50, true)]  // Qaf
    [InlineData(68, true)]  // Nun
    public void HasMuqattaat_Known_Surahs_Returns_True(int surah, bool expected)
    {
        Quran.HasMuqattaat(surah).Should().Be(expected);
    }

    [Theory]
    [InlineData(1)]   // Al-Fatiha
    [InlineData(4)]   // An-Nisa
    [InlineData(5)]   // Al-Ma'idah
    [InlineData(6)]   // Al-An'am
    [InlineData(8)]   // Al-Anfal
    [InlineData(9)]   // At-Tawbah
    [InlineData(114)] // An-Nas
    public void HasMuqattaat_Non_Muqattaat_Surahs_Returns_False(int surah)
    {
        Quran.HasMuqattaat(surah).Should().BeFalse();
    }

    #endregion

    #region GetMuqattaatForSurah

    [Theory]
    [InlineData(2, "Alif Lam Mim", "الم")]
    [InlineData(7, "Alif Lam Mim Sad", "المص")]
    [InlineData(19, "Kaf Ha Ya Ain Sad", "كهيعص")]
    [InlineData(20, "Ta Ha", "طه")]
    [InlineData(36, "Ya Sin", "يس")]
    [InlineData(38, "Sad", "ص")]
    [InlineData(42, "Ha Mim Ain Sin Qaf", "حم عسق")]
    [InlineData(50, "Qaf", "ق")]
    [InlineData(68, "Nun", "ن")]
    public void GetMuqattaatForSurah_Returns_Correct_Data(int surah, string expectedLetters, string expectedArabic)
    {
        var muqattaat = Quran.GetMuqattaatForSurah(surah);
        muqattaat.Should().NotBeNull();
        muqattaat!.SurahNumber.Should().Be(surah);
        muqattaat.Letters.Should().Be(expectedLetters);
        muqattaat.ArabicLetters.Should().Be(expectedArabic);
    }

    [Fact]
    public void GetMuqattaatForSurah_Non_Muqattaat_Returns_Null()
    {
        Quran.GetMuqattaatForSurah(1).Should().BeNull();
        Quran.GetMuqattaatForSurah(114).Should().BeNull();
    }

    #endregion

    #region Specific Letter Groups

    [Fact]
    public void Alif_Lam_Mim_Group_Has_Six_Surahs()
    {
        // Surahs 2, 3, 29, 30, 31, 32 all start with Alif Lam Mim
        var almSurahs = new[] { 2, 3, 29, 30, 31, 32 };
        foreach (var surah in almSurahs)
        {
            var m = Quran.GetMuqattaatForSurah(surah);
            m.Should().NotBeNull();
            m!.Letters.Should().Be("Alif Lam Mim");
            m.ArabicLetters.Should().Be("الم");
        }
    }

    [Fact]
    public void Ha_Mim_Group_Has_Seven_Surahs()
    {
        // Surahs 40-46 all start with Ha Mim (or Ha Mim + extra)
        var hamimSurahs = new[] { 40, 41, 43, 44, 45, 46 };
        foreach (var surah in hamimSurahs)
        {
            var m = Quran.GetMuqattaatForSurah(surah);
            m.Should().NotBeNull();
            m!.Letters.Should().Be("Ha Mim");
            m.ArabicLetters.Should().Be("حم");
        }

        // Surah 42 is Ha Mim Ain Sin Qaf
        var s42 = Quran.GetMuqattaatForSurah(42);
        s42!.Letters.Should().Contain("Ha Mim");
    }

    [Fact]
    public void Alif_Lam_Ra_Group_Has_Five_Surahs()
    {
        var alrSurahs = new[] { 10, 11, 12, 14, 15 };
        foreach (var surah in alrSurahs)
        {
            var m = Quran.GetMuqattaatForSurah(surah);
            m.Should().NotBeNull();
            m!.Letters.Should().Be("Alif Lam Ra");
            m.ArabicLetters.Should().Be("الر");
        }
    }

    [Fact]
    public void Ta_Sin_Mim_Group_Has_Two_Surahs()
    {
        var tsmSurahs = new[] { 26, 28 };
        foreach (var surah in tsmSurahs)
        {
            var m = Quran.GetMuqattaatForSurah(surah);
            m.Should().NotBeNull();
            m!.Letters.Should().Be("Ta Sin Mim");
        }
    }

    #endregion

    #region MuqattaatSurah Model

    [Fact]
    public void MuqattaatSurah_ToString_Contains_Surah_And_Letters()
    {
        var m = Quran.GetMuqattaatForSurah(2);
        var str = m!.ToString();
        str.Should().Contain("2");
        str.Should().Contain("الم");
        str.Should().Contain("Alif Lam Mim");
    }

    #endregion
}
