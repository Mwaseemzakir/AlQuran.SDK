using AlQuran.Enums;

namespace AlQuran.Tests;

/// <summary>
/// Tests validating the SurahName enum has all 114 values
/// and each value correctly maps to the corresponding Surah.
/// </summary>
public class SurahNameEnumTests
{
    [Fact]
    public void SurahNameEnum_Should_Have114Values()
    {
        var values = Enum.GetValues(typeof(SurahName)).Cast<SurahName>().ToList();
        Assert.Equal(114, values.Count);
    }

    [Fact]
    public void SurahNameEnum_Values_Should_BeUnique()
    {
        var values = Enum.GetValues(typeof(SurahName)).Cast<SurahName>()
            .Select(v => (int)v).ToHashSet();
        Assert.Equal(114, values.Count);
    }

    [Fact]
    public void SurahNameEnum_Should_Cover1To114()
    {
        var values = Enum.GetValues(typeof(SurahName)).Cast<SurahName>()
            .Select(v => (int)v).OrderBy(v => v).ToList();

        for (int i = 0; i < 114; i++)
        {
            Assert.Equal(i + 1, values[i]);
        }
    }

    [Fact]
    public void EverySurahNameEnum_Should_MapToValidSurah()
    {
        foreach (SurahName name in Enum.GetValues(typeof(SurahName)))
        {
            var surah = Quran.GetSurah(name);
            Assert.Equal((int)name, surah.Number);
            Assert.Equal(name, surah.Name);
        }
    }

    [Fact]
    public void EverySurah_Name_Should_MatchSurahNameEnum()
    {
        for (int i = 1; i <= 114; i++)
        {
            var surah = Quran.GetSurah(i);
            Assert.Equal((SurahName)i, surah.Name);
        }
    }

    [Theory]
    [InlineData(SurahName.AlFatiha, 1, "Al-Fatiha")]
    [InlineData(SurahName.AlBaqarah, 2, "Al-Baqarah")]
    [InlineData(SurahName.AliImran, 3, "Ali 'Imran")]
    [InlineData(SurahName.AnNisa, 4, "An-Nisa")]
    [InlineData(SurahName.AlMaidah, 5, "Al-Ma'idah")]
    [InlineData(SurahName.AlAnam, 6, "Al-An'am")]
    [InlineData(SurahName.AlAraf, 7, "Al-A'raf")]
    [InlineData(SurahName.AlAnfal, 8, "Al-Anfal")]
    [InlineData(SurahName.AtTawbah, 9, "At-Tawbah")]
    [InlineData(SurahName.Yunus, 10, "Yunus")]
    [InlineData(SurahName.YaSin, 36, "Ya-Sin")]
    [InlineData(SurahName.ArRahman, 55, "Ar-Rahman")]
    [InlineData(SurahName.AlMulk, 67, "Al-Mulk")]
    [InlineData(SurahName.AlIkhlas, 112, "Al-Ikhlas")]
    [InlineData(SurahName.AlFalaq, 113, "Al-Falaq")]
    [InlineData(SurahName.AnNas, 114, "An-Nas")]
    public void SurahName_Should_MapToCorrectSurah(SurahName name, int expectedNumber, string expectedEnglish)
    {
        var surah = Quran.GetSurah(name);
        Assert.Equal(expectedNumber, surah.Number);
        Assert.Equal(expectedEnglish, surah.EnglishName);
    }

    [Fact]
    public void GetSurah_BySurahName_And_ByNumber_Should_ReturnSameInstance()
    {
        foreach (SurahName name in Enum.GetValues(typeof(SurahName)))
        {
            var byName = Quran.GetSurah(name);
            var byNumber = Quran.GetSurah((int)name);
            Assert.Same(byName, byNumber);
        }
    }

    [Fact]
    public void GetAyahs_BySurahName_Should_ReturnSameData_AsGetAyahsByNumber()
    {
        var byName = Quran.GetAyahs(SurahName.AlFatiha);
        var byNumber = Quran.GetAyahs(1);

        Assert.Equal(byNumber.Count, byName.Count);
        for (int i = 0; i < byName.Count; i++)
        {
            Assert.Equal(byNumber[i].Text, byName[i].Text);
        }
    }

    [Fact]
    public void GetAyahCount_BySurahName_Should_MatchByNumber()
    {
        foreach (SurahName name in Enum.GetValues(typeof(SurahName)))
        {
            Assert.Equal(
                Quran.GetAyahCount((int)name),
                Quran.GetAyahCount(name));
        }
    }

    [Fact]
    public void GetAyah_BySurahName_Should_MatchByNumber()
    {
        var byName = Quran.GetAyah(SurahName.AlFatiha, 1);
        var byNumber = Quran.GetAyah(1, 1);
        Assert.Equal(byNumber.Text, byName.Text);
        Assert.Equal(byNumber.SurahNumber, byName.SurahNumber);
        Assert.Equal(byNumber.AyahNumber, byName.AyahNumber);
    }
}
