using AlQuran.SDK.Enums;
using AlQuran.SDK.Exceptions;

namespace AlQuran.SDK.Tests;

public class AyahTests
{
    #region GetAyah

    [Fact]
    public void GetAyah_AlFatiha1_Should_ReturnFirstAyah()
    {
        var ayah = Quran.GetAyah(1, 1);
        Assert.Equal(1, ayah.SurahNumber);
        Assert.Equal(1, ayah.AyahNumber);
        Assert.False(string.IsNullOrWhiteSpace(ayah.Text));
    }

    [Fact]
    public void GetAyah_AlFatiha7_Should_ReturnLastAyah()
    {
        var ayah = Quran.GetAyah(1, 7);
        Assert.Equal(1, ayah.SurahNumber);
        Assert.Equal(7, ayah.AyahNumber);
        Assert.False(string.IsNullOrWhiteSpace(ayah.Text));
    }

    [Fact]
    public void GetAyah_BySurahName_Should_ReturnCorrectAyah()
    {
        var ayah = Quran.GetAyah(SurahName.AlFatiha, 1);
        Assert.Equal(1, ayah.SurahNumber);
        Assert.Equal(1, ayah.AyahNumber);
    }

    [Fact]
    public void GetAyah_InvalidSurah_Should_ThrowSurahNotFoundException()
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.GetAyah(0, 1));
    }

    [Fact]
    public void GetAyah_InvalidAyah_Should_ThrowAyahNotFoundException()
    {
        Assert.Throws<AyahNotFoundException>(() => Quran.GetAyah(1, 999));
    }

    [Fact]
    public void GetAyah_SimpleScript_Should_ReturnTextWithoutTashkeel()
    {
        var uthmani = Quran.GetAyah(1, 1, ScriptType.Uthmani);
        var simple = Quran.GetAyah(1, 1, ScriptType.Simple);

        Assert.NotEqual(uthmani.Text, simple.Text);
        Assert.True(uthmani.Text.Length >= simple.Text.Length);
    }

    #endregion

    #region GetAyahs

    [Fact]
    public void GetAyahs_AlFatiha_Should_Return7Ayahs()
    {
        var ayahs = Quran.GetAyahs(1);
        Assert.Equal(7, ayahs.Count);
    }

    [Fact]
    public void GetAyahs_AlBaqarah_Should_Return286Ayahs()
    {
        var ayahs = Quran.GetAyahs(2);
        Assert.Equal(286, ayahs.Count);
    }

    [Fact]
    public void GetAyahs_BySurahName_Should_ReturnCorrectCount()
    {
        var ayahs = Quran.GetAyahs(SurahName.AlIkhlas);
        Assert.Equal(4, ayahs.Count);
    }

    [Fact]
    public void GetAyahs_InvalidSurah_Should_ThrowSurahNotFoundException()
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.GetAyahs(0));
    }

    [Fact]
    public void GetAyahs_Range_Should_ReturnCorrectSubset()
    {
        var ayahs = Quran.GetAyahs(2, 1, 5);
        Assert.Equal(5, ayahs.Count);
        Assert.All(ayahs, a =>
        {
            Assert.Equal(2, a.SurahNumber);
            Assert.InRange(a.AyahNumber, 1, 5);
        });
    }

    [Fact]
    public void GetAyahs_Should_ReturnNewListEachTime()
    {
        var list1 = Quran.GetAyahs(1);
        var list2 = Quran.GetAyahs(1);
        Assert.NotSame(list1, list2);
    }

    #endregion

    #region Data Integrity

    [Fact]
    public void AllSurahs_AyahCount_Should_MatchMetadata()
    {
        for (int i = 1; i <= 114; i++)
        {
            var surah = Quran.GetSurah(i);
            var ayahs = Quran.GetAyahs(i);
            Assert.Equal(surah.AyahCount, ayahs.Count);
        }
    }

    [Fact]
    public void AllAyahs_Should_HaveNonEmptyText()
    {
        for (int i = 1; i <= 114; i++)
        {
            var ayahs = Quran.GetAyahs(i);
            Assert.All(ayahs, a => Assert.False(string.IsNullOrWhiteSpace(a.Text)));
        }
    }

    [Fact]
    public void AllAyahs_Should_HaveValidJuz()
    {
        var ayahs = Quran.GetAyahs(1);
        Assert.All(ayahs, a => Assert.InRange(a.Juz, 1, 30));
    }

    [Fact]
    public void AllAyahs_Should_HaveValidPage()
    {
        var ayahs = Quran.GetAyahs(1);
        Assert.All(ayahs, a => Assert.True(a.Page > 0));
    }

    [Fact]
    public void IsTextDataAvailable_Should_ReturnTrue()
    {
        Assert.True(Quran.IsTextDataAvailable(ScriptType.Uthmani));
        Assert.True(Quran.IsTextDataAvailable(ScriptType.Simple));
    }

    [Fact]
    public void Ayah_ToString_Should_ContainReference()
    {
        var ayah = Quran.GetAyah(1, 1);
        var str = ayah.ToString();
        Assert.Contains("[1:1]", str);
    }

    #endregion
}
