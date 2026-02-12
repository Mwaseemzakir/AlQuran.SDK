using AlQuran.SDK.Enums;
using AlQuran.SDK.Extensions;
using AlQuran.SDK.Models;

namespace AlQuran.SDK.Tests;

/// <summary>
/// Tests for model ToString() methods, model properties, and
/// SearchResult/Ayah/Juz/Surah/SajdaVerse behaviors.
/// </summary>
public class ModelTests
{
    #region Surah Model

    [Fact]
    public void Surah_ToString_Should_ContainAllKeyInfo()
    {
        var surah = Quran.GetSurah(1);
        var str = surah.ToString();
        Assert.Contains("1", str);
        Assert.Contains("Al-Fatiha", str);
        Assert.Contains("الفاتحة", str);
        Assert.Contains("7", str);
    }

    [Fact]
    public void Surah_ToString_ForAllSurahs_Should_NotBeNullOrEmpty()
    {
        for (int i = 1; i <= 114; i++)
        {
            var surah = Quran.GetSurah(i);
            Assert.False(string.IsNullOrWhiteSpace(surah.ToString()));
        }
    }

    [Fact]
    public void Surah_Properties_Should_BeReadOnly()
    {
        var surah = Quran.GetSurah(1);
        // Verify all public properties return consistent values
        Assert.Equal(1, surah.Number);
        Assert.Equal("الفاتحة", surah.ArabicName);
        Assert.Equal("Al-Fatiha", surah.EnglishName);
        Assert.Equal("The Opening", surah.EnglishMeaning);
        Assert.Equal(7, surah.AyahCount);
        Assert.Equal(RevelationType.Meccan, surah.RevelationType);
        Assert.Equal(5, surah.RevelationOrder);
        Assert.Equal(1, surah.RukuCount);
        Assert.Equal(1, surah.JuzStart);
        Assert.Equal(1, surah.JuzEnd);
        Assert.Equal(1, surah.PageStart);
        Assert.True(surah.HasBismillah);
        Assert.Equal(SurahName.AlFatiha, surah.Name);
    }

    [Fact]
    public void Surah_SameInstance_Should_BeReturned()
    {
        var s1 = Quran.GetSurah(1);
        var s2 = Quran.GetSurah(SurahName.AlFatiha);
        var s3 = Quran.GetSurah("Al-Fatiha");
        Assert.Same(s1, s2);
        Assert.Same(s2, s3);
    }

    #endregion

    #region Ayah Model

    [Fact]
    public void Ayah_ToString_Should_ContainReference()
    {
        var ayah = Quran.GetAyah(1, 1);
        var str = ayah.ToString();
        Assert.Contains("[1:1]", str);
    }

    [Fact]
    public void Ayah_ToString_Should_ContainPartialText()
    {
        var ayah = Quran.GetAyah(1, 1);
        var str = ayah.ToString();
        // Should contain some Arabic text
        Assert.Contains(str, s => s >= '\u0600' && s <= '\u06FF');
    }

    [Fact]
    public void Ayah_ToString_LongAyah_Should_BeTruncated()
    {
        // Al-Baqarah 2:282 is one of the longest ayahs
        var ayah = Quran.GetAyah(2, 282);
        var str = ayah.ToString();
        if (ayah.Text.Length > 50)
        {
            Assert.Contains("...", str);
        }
    }

    [Fact]
    public void Ayah_Properties_Should_BeConsistent()
    {
        var ayah = Quran.GetAyah(2, 255);
        Assert.Equal(2, ayah.SurahNumber);
        Assert.Equal(255, ayah.AyahNumber);
        Assert.False(string.IsNullOrWhiteSpace(ayah.Text));
        Assert.InRange(ayah.Juz, 1, 30);
        Assert.True(ayah.Page > 0);
        Assert.InRange(ayah.HizbQuarter, 1, 240);
    }

    [Fact]
    public void Ayah_SajdaMarking_Should_BeCorrectForSajdaVerse()
    {
        // 32:15 is an obligatory sajda
        var ayah = Quran.GetAyah(32, 15);
        Assert.True(ayah.HasSajda);

        // 1:1 is not a sajda
        var nonSajda = Quran.GetAyah(1, 1);
        Assert.False(nonSajda.HasSajda);
    }

    #endregion

    #region Juz Model

    [Fact]
    public void Juz_ToString_Should_ContainJuzNumber()
    {
        var juz = Quran.GetJuz(1);
        Assert.Contains("Juz 1", juz.ToString());
    }

    [Fact]
    public void Juz_ToString_Should_ContainBoundaries()
    {
        var juz = Quran.GetJuz(1);
        var str = juz.ToString();
        Assert.Contains("1:1", str); // Start reference
    }

    [Fact]
    public void Juz_ToString_ForAllJuz_Should_NotBeNullOrEmpty()
    {
        for (int j = 1; j <= 30; j++)
        {
            var juz = Quran.GetJuz(j);
            Assert.False(string.IsNullOrWhiteSpace(juz.ToString()));
        }
    }

    [Fact]
    public void Juz_SameInstance_Should_BeReturned()
    {
        var j1 = Quran.GetJuz(1);
        var j2 = Quran.GetJuz(1);
        Assert.Same(j1, j2);
    }

    [Fact]
    public void Juz_Properties_Should_BeConsistent()
    {
        var juz = Quran.GetJuz(1);
        Assert.Equal(1, juz.Number);
        Assert.Equal(1, juz.StartSurah);
        Assert.Equal(1, juz.StartAyah);
        Assert.True(juz.EndSurah >= juz.StartSurah);
        Assert.False(string.IsNullOrWhiteSpace(juz.ArabicName));
    }

    #endregion

    #region SajdaVerse Model

    [Fact]
    public void SajdaVerse_ToString_Should_ContainReference()
    {
        var sajda = Quran.GetSajda(32, 15);
        Assert.NotNull(sajda);
        var str = sajda!.ToString();
        Assert.Contains("32:15", str);
    }

    [Fact]
    public void SajdaVerse_ToString_Should_ContainType()
    {
        var sajda = Quran.GetSajda(32, 15);
        Assert.NotNull(sajda);
        Assert.Contains("Obligatory", sajda!.ToString());

        var recommended = Quran.GetSajda(7, 206);
        Assert.NotNull(recommended);
        Assert.Contains("Recommended", recommended!.ToString());
    }

    [Fact]
    public void SajdaVerse_Properties_Should_BeConsistent()
    {
        var sajda = Quran.GetAllSajdas().First();
        Assert.Equal(1, sajda.Number);
        Assert.InRange(sajda.SurahNumber, 1, 114);
        Assert.True(sajda.AyahNumber > 0);
        Assert.Contains(sajda.Type, new[] { SajdaType.Obligatory, SajdaType.Recommended });
    }

    #endregion

    #region SearchResult Model

    [Fact]
    public void SearchResult_Should_HaveAllPropertiesPopulated()
    {
        var results = Quran.Search("الله", 1, ScriptType.Simple);
        Assert.NotEmpty(results);

        var result = results[0];
        Assert.Equal(1, result.SurahNumber);
        Assert.True(result.AyahNumber > 0);
        Assert.False(string.IsNullOrWhiteSpace(result.Text));
        Assert.False(string.IsNullOrWhiteSpace(result.SurahName));
        Assert.False(string.IsNullOrWhiteSpace(result.MatchedText));
    }

    [Fact]
    public void SearchResult_ToString_Should_ContainReference()
    {
        var results = Quran.Search("الله", 1, ScriptType.Simple);
        Assert.NotEmpty(results);

        var str = results[0].ToString();
        Assert.Contains("[1:", str);
    }

    [Fact]
    public void SearchResult_SurahName_Should_MatchSurahEnglishName()
    {
        var results = Quran.Search("الله", 2, ScriptType.Simple);
        Assert.NotEmpty(results);
        Assert.All(results, r => Assert.Equal("Al-Baqarah", r.SurahName));
    }

    [Fact]
    public void SearchResult_MatchedText_Should_ContainSearchTerm()
    {
        var results = Quran.Search("الله", 1, ScriptType.Simple);
        Assert.NotEmpty(results);
        Assert.All(results, r => Assert.Equal("الله", r.MatchedText));
    }

    #endregion

    #region Enum Values

    [Fact]
    public void ScriptType_Should_Have2Values()
    {
        var values = Enum.GetValues(typeof(ScriptType));
        Assert.Equal(2, values.Length);
    }

    [Fact]
    public void RevelationType_Should_Have2Values()
    {
        var values = Enum.GetValues(typeof(RevelationType));
        Assert.Equal(2, values.Length);
    }

    [Fact]
    public void SajdaType_Should_Have2Values()
    {
        var values = Enum.GetValues(typeof(SajdaType));
        Assert.Equal(2, values.Length);
    }

    [Fact]
    public void ScriptType_Values_Should_HaveCorrectInts()
    {
        Assert.Equal(0, (int)ScriptType.Simple);
        Assert.Equal(1, (int)ScriptType.Uthmani);
    }

    [Fact]
    public void RevelationType_Values_Should_HaveCorrectInts()
    {
        Assert.Equal(0, (int)RevelationType.Meccan);
        Assert.Equal(1, (int)RevelationType.Medinan);
    }

    [Fact]
    public void SajdaType_Values_Should_HaveCorrectInts()
    {
        Assert.Equal(0, (int)SajdaType.Obligatory);
        Assert.Equal(1, (int)SajdaType.Recommended);
    }

    #endregion
}
