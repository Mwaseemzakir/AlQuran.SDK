using AlQuran.Exceptions;

namespace AlQuran.Tests;

public class JuzTests
{
    #region GetAllJuz

    [Fact]
    public void GetAllJuz_Should_Return30Juz()
    {
        var juz = Quran.GetAllJuz();
        Assert.Equal(30, juz.Count);
    }

    [Fact]
    public void GetAllJuz_Should_ReturnNewListEachTime()
    {
        var list1 = Quran.GetAllJuz();
        var list2 = Quran.GetAllJuz();
        Assert.NotSame(list1, list2);
    }

    [Fact]
    public void GetAllJuz_Should_BeOrderedByNumber()
    {
        var juz = Quran.GetAllJuz();
        for (int i = 0; i < juz.Count; i++)
        {
            Assert.Equal(i + 1, juz[i].Number);
        }
    }

    #endregion

    #region GetJuz

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(30, 78, 1)]
    public void GetJuz_Should_ReturnCorrectJuz(int number, int expectedStartSurah, int expectedStartAyah)
    {
        var juz = Quran.GetJuz(number);
        Assert.Equal(number, juz.Number);
        Assert.Equal(expectedStartSurah, juz.StartSurah);
        Assert.Equal(expectedStartAyah, juz.StartAyah);
    }

    [Fact]
    public void GetJuz_ByNumber_Should_ReturnSameInstance()
    {
        var juz1 = Quran.GetJuz(1);
        var juz2 = Quran.GetJuz(1);
        Assert.Same(juz1, juz2);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(31)]
    public void GetJuz_InvalidNumber_Should_ThrowJuzNotFoundException(int number)
    {
        Assert.Throws<JuzNotFoundException>(() => Quran.GetJuz(number));
    }

    #endregion

    #region GetJuzOrDefault

    [Fact]
    public void GetJuzOrDefault_ValidNumber_Should_ReturnJuz()
    {
        var juz = Quran.GetJuzOrDefault(1);
        Assert.NotNull(juz);
    }

    [Fact]
    public void GetJuzOrDefault_InvalidNumber_Should_ReturnNull()
    {
        var juz = Quran.GetJuzOrDefault(0);
        Assert.Null(juz);
    }

    #endregion

    #region IsValidJuz

    [Theory]
    [InlineData(1, true)]
    [InlineData(15, true)]
    [InlineData(30, true)]
    [InlineData(0, false)]
    [InlineData(31, false)]
    [InlineData(-1, false)]
    public void IsValidJuz_Should_ReturnCorrectResult(int number, bool expected)
    {
        Assert.Equal(expected, Quran.IsValidJuz(number));
    }

    #endregion

    #region GetJuzNumber

    [Theory]
    [InlineData(1, 1, 1)]     // Al-Fatiha is in Juz 1
    [InlineData(2, 142, 2)]   // Al-Baqarah:142 is in Juz 2
    [InlineData(114, 1, 30)]  // An-Nas is in Juz 30
    public void GetJuzNumber_Should_ReturnCorrectJuz(int surah, int ayah, int expectedJuz)
    {
        Assert.Equal(expectedJuz, Quran.GetJuzNumber(surah, ayah));
    }

    #endregion

    #region GetSurahsByJuz

    [Fact]
    public void GetSurahsByJuz_Juz1_Should_ContainAlFatiha()
    {
        var surahs = Quran.GetSurahsByJuz(1);
        Assert.Contains(surahs, s => s.Number == 1);
    }

    [Fact]
    public void GetSurahsByJuz_Juz30_Should_ContainAnNas()
    {
        var surahs = Quran.GetSurahsByJuz(30);
        Assert.Contains(surahs, s => s.Number == 114);
    }

    [Fact]
    public void GetSurahsByJuz_InvalidJuz_Should_ThrowJuzNotFoundException()
    {
        Assert.Throws<JuzNotFoundException>(() => Quran.GetSurahsByJuz(0));
    }

    #endregion

    #region Data Integrity

    [Fact]
    public void AllJuz_Should_HaveNonEmptyArabicName()
    {
        var juz = Quran.GetAllJuz();
        Assert.All(juz, j => Assert.False(string.IsNullOrWhiteSpace(j.ArabicName)));
    }

    [Fact]
    public void AllJuz_Should_HaveValidSurahReferences()
    {
        var juz = Quran.GetAllJuz();
        Assert.All(juz, j =>
        {
            Assert.InRange(j.StartSurah, 1, 114);
            Assert.InRange(j.EndSurah, 1, 114);
            Assert.True(j.EndSurah >= j.StartSurah || j.EndSurah == j.StartSurah);
        });
    }

    [Fact]
    public void Juz_ToString_Should_ContainJuzNumber()
    {
        var juz = Quran.GetJuz(1);
        Assert.Contains("Juz 1", juz.ToString());
    }

    #endregion
}
