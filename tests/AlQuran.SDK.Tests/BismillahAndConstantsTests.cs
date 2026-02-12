using AlQuran.SDK.Enums;
using Xunit;

namespace AlQuran.SDK.Tests;

public class BismillahAndConstantsTests
{
    [Fact]
    public void TotalSurahs_Should_Be114()
    {
        Assert.Equal(114, Quran.TotalSurahs);
    }

    [Fact]
    public void TotalAyahs_Should_Be6236()
    {
        Assert.Equal(6236, Quran.TotalAyahs);
    }

    [Fact]
    public void TotalJuz_Should_Be30()
    {
        Assert.Equal(30, Quran.TotalJuz);
    }

    [Fact]
    public void TotalSajdas_Should_Be15()
    {
        Assert.Equal(15, Quran.TotalSajdas);
    }

    [Fact]
    public void GetBismillah_Uthmani_Should_ReturnArabicText()
    {
        var bismillah = Quran.GetBismillah(ScriptType.Uthmani);
        Assert.False(string.IsNullOrWhiteSpace(bismillah));
        Assert.Contains("ٱللَّهِ", bismillah);
    }

    [Fact]
    public void GetBismillah_Simple_Should_ReturnArabicText()
    {
        var bismillah = Quran.GetBismillah(ScriptType.Simple);
        Assert.False(string.IsNullOrWhiteSpace(bismillah));
        Assert.Contains("الله", bismillah);
    }

    [Fact]
    public void GetBismillah_Default_Should_ReturnUthmani()
    {
        var bismillah = Quran.GetBismillah();
        var uthmani = Quran.GetBismillah(ScriptType.Uthmani);
        Assert.Equal(uthmani, bismillah);
    }

    [Fact]
    public void GetSurahNames_Should_ReturnCorrectRange()
    {
        var names = Quran.GetSurahNames(1, 5);
        Assert.Equal(5, names.Count);
        Assert.Equal("Al-Fatiha", names[0].EnglishName);
        Assert.Equal("Al-Ma'idah", names[4].EnglishName);
    }

    [Fact]
    public void GetSurahNames_Default_Should_Return114()
    {
        var names = Quran.GetSurahNames();
        Assert.Equal(114, names.Count);
    }

    [Fact]
    public void GetAyahCount_Should_ReturnCorrectCount()
    {
        Assert.Equal(7, Quran.GetAyahCount(1));
        Assert.Equal(286, Quran.GetAyahCount(2));
    }

    [Fact]
    public void GetAyahCount_BySurahName_Should_ReturnCorrectCount()
    {
        Assert.Equal(7, Quran.GetAyahCount(SurahName.AlFatiha));
        Assert.Equal(4, Quran.GetAyahCount(SurahName.AlIkhlas));
    }
}
