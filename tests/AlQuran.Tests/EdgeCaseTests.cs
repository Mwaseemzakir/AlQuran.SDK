using AlQuran.Enums;
using AlQuran.Exceptions;

namespace AlQuran.Tests;

/// <summary>
/// Tests for edge cases, boundary conditions, and invalid input handling
/// across all API methods.
/// </summary>
public class EdgeCaseTests
{
    #region GetSurah Edge Cases

    [Fact]
    public void GetSurah_MinValid_Should_Return()
    {
        var surah = Quran.GetSurah(1);
        Assert.Equal(1, surah.Number);
    }

    [Fact]
    public void GetSurah_MaxValid_Should_Return()
    {
        var surah = Quran.GetSurah(114);
        Assert.Equal(114, surah.Number);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    [InlineData(115)]
    [InlineData(999)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void GetSurah_OutOfRange_Should_Throw(int number)
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.GetSurah(number));
    }

    [Fact]
    public void GetSurah_WhitespaceOnlyName_Should_ThrowArgumentNull()
    {
        Assert.Throws<ArgumentNullException>(() => Quran.GetSurah("   "));
    }

    [Fact]
    public void GetSurah_TabName_Should_ThrowArgumentNull()
    {
        Assert.Throws<ArgumentNullException>(() => Quran.GetSurah("\t"));
    }

    [Fact]
    public void GetSurah_NewlineName_Should_ThrowArgumentNull()
    {
        Assert.Throws<ArgumentNullException>(() => Quran.GetSurah("\n"));
    }

    [Fact]
    public void GetSurah_NullName_Should_ThrowArgumentNull()
    {
        Assert.Throws<ArgumentNullException>(() => Quran.GetSurah((string)null!));
    }

    [Fact]
    public void GetSurah_EmptyName_Should_ThrowArgumentNull()
    {
        Assert.Throws<ArgumentNullException>(() => Quran.GetSurah(""));
    }

    [Fact]
    public void GetSurah_InvalidEnumValue_Should_Throw()
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.GetSurah((SurahName)999));
    }

    [Fact]
    public void GetSurah_ZeroEnumValue_Should_Throw()
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.GetSurah((SurahName)0));
    }

    #endregion

    #region GetSurahOrDefault Edge Cases

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(115)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void GetSurahOrDefault_OutOfRange_Should_ReturnNull(int number)
    {
        Assert.Null(Quran.GetSurahOrDefault(number));
    }

    [Fact]
    public void GetSurahOrDefault_NullName_Should_ReturnNull()
    {
        Assert.Null(Quran.GetSurahOrDefault((string)null!));
    }

    [Fact]
    public void GetSurahOrDefault_EmptyName_Should_ReturnNull()
    {
        Assert.Null(Quran.GetSurahOrDefault(""));
    }

    [Fact]
    public void GetSurahOrDefault_WhitespaceName_Should_ReturnNull()
    {
        Assert.Null(Quran.GetSurahOrDefault("   "));
    }

    [Fact]
    public void GetSurahOrDefault_InvalidName_Should_ReturnNull()
    {
        Assert.Null(Quran.GetSurahOrDefault("NonExistentSurah"));
    }

    [Fact]
    public void GetSurahOrDefault_PartialName_Should_ReturnNull()
    {
        Assert.Null(Quran.GetSurahOrDefault("Al-Fat")); // Partial, not full
    }

    [Fact]
    public void GetSurahOrDefault_ValidName_Should_ReturnSurah()
    {
        var surah = Quran.GetSurahOrDefault("Al-Fatiha");
        Assert.NotNull(surah);
        Assert.Equal(1, surah!.Number);
    }

    [Fact]
    public void GetSurahOrDefault_ValidNameCaseInsensitive_Should_ReturnSurah()
    {
        var surah = Quran.GetSurahOrDefault("al-fatiha");
        Assert.NotNull(surah);
    }

    #endregion

    #region GetSurahByArabicName Edge Cases

    [Fact]
    public void GetSurahByArabicName_NullName_Should_ThrowArgumentNull()
    {
        Assert.Throws<ArgumentNullException>(() => Quran.GetSurahByArabicName(null!));
    }

    [Fact]
    public void GetSurahByArabicName_EmptyName_Should_ThrowArgumentNull()
    {
        Assert.Throws<ArgumentNullException>(() => Quran.GetSurahByArabicName(""));
    }

    [Fact]
    public void GetSurahByArabicName_WhitespaceName_Should_ThrowArgumentNull()
    {
        Assert.Throws<ArgumentNullException>(() => Quran.GetSurahByArabicName("   "));
    }

    [Fact]
    public void GetSurahByArabicName_InvalidName_Should_ThrowSurahNotFound()
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.GetSurahByArabicName("غير موجود"));
    }

    [Fact]
    public void GetSurahByArabicName_LatinName_Should_ThrowSurahNotFound()
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.GetSurahByArabicName("Al-Fatiha"));
    }

    #endregion

    #region GetAyah Edge Cases

    [Fact]
    public void GetAyah_InvalidSurah_Should_ThrowSurahNotFound()
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.GetAyah(0, 1));
    }

    [Fact]
    public void GetAyah_InvalidSurah115_Should_ThrowSurahNotFound()
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.GetAyah(115, 1));
    }

    [Fact]
    public void GetAyah_InvalidAyah0_Should_ThrowAyahNotFound()
    {
        Assert.Throws<AyahNotFoundException>(() => Quran.GetAyah(1, 0));
    }

    [Fact]
    public void GetAyah_NegativeAyah_Should_ThrowAyahNotFound()
    {
        Assert.Throws<AyahNotFoundException>(() => Quran.GetAyah(1, -1));
    }

    [Fact]
    public void GetAyah_AyahExceedingSurah_Should_ThrowAyahNotFound()
    {
        Assert.Throws<AyahNotFoundException>(() => Quran.GetAyah(1, 8)); // Al-Fatiha has 7
    }

    [Fact]
    public void GetAyah_AyahExceedingSurah_AlBaqarah_Should_ThrowAyahNotFound()
    {
        Assert.Throws<AyahNotFoundException>(() => Quran.GetAyah(2, 287)); // Al-Baqarah has 286
    }

    [Fact]
    public void GetAyah_MaxAyahInSurah_Should_Work()
    {
        // Every surah: get the last valid ayah
        for (int s = 1; s <= 114; s++)
        {
            var surah = Quran.GetSurah(s);
            var ayah = Quran.GetAyah(s, surah.AyahCount);
            Assert.Equal(surah.AyahCount, ayah.AyahNumber);
        }
    }

    [Fact]
    public void GetAyah_FirstAyahInSurah_Should_Work()
    {
        for (int s = 1; s <= 114; s++)
        {
            var ayah = Quran.GetAyah(s, 1);
            Assert.Equal(1, ayah.AyahNumber);
            Assert.Equal(s, ayah.SurahNumber);
        }
    }

    #endregion

    #region GetAyahs Range Edge Cases

    [Fact]
    public void GetAyahs_Range_StartGreaterThanEnd_Should_ReturnEmpty()
    {
        var ayahs = Quran.GetAyahs(1, 5, 3);
        Assert.Empty(ayahs);
    }

    [Fact]
    public void GetAyahs_Range_StartEqualsEnd_Should_ReturnOne()
    {
        var ayahs = Quran.GetAyahs(1, 3, 3);
        Assert.Single(ayahs);
        Assert.Equal(3, ayahs[0].AyahNumber);
    }

    [Fact]
    public void GetAyahs_Range_ExceedingLength_Should_ReturnAvailable()
    {
        var ayahs = Quran.GetAyahs(1, 1, 999);
        Assert.Equal(7, ayahs.Count); // Al-Fatiha has 7
    }

    [Fact]
    public void GetAyahs_Range_NegativeStart_Should_ReturnEmpty()
    {
        var ayahs = Quran.GetAyahs(1, -5, -1);
        Assert.Empty(ayahs);
    }

    [Fact]
    public void GetAyahs_Range_ZeroStart_Should_ReturnEmpty()
    {
        var ayahs = Quran.GetAyahs(1, 0, 0);
        Assert.Empty(ayahs);
    }

    [Fact]
    public void GetAyahs_Range_EntireSurah_Should_ReturnAll()
    {
        var ayahs = Quran.GetAyahs(1, 1, 7);
        Assert.Equal(7, ayahs.Count);
    }

    [Fact]
    public void GetAyahs_Range_InvalidSurah_Should_Throw()
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.GetAyahs(0, 1, 5));
    }

    [Fact]
    public void GetAyahs_Range_WithSimpleScript_Should_Work()
    {
        var ayahs = Quran.GetAyahs(2, 1, 5, ScriptType.Simple);
        Assert.Equal(5, ayahs.Count);
        Assert.All(ayahs, a => Assert.Equal(2, a.SurahNumber));
    }

    [Fact]
    public void GetAyahs_InvalidSurah_Should_Throw()
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.GetAyahs(0));
    }

    [Fact]
    public void GetAyahs_InvalidSurah115_Should_Throw()
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.GetAyahs(115));
    }

    #endregion

    #region GetJuz Edge Cases

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(31)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void GetJuz_Invalid_Should_Throw(int number)
    {
        Assert.Throws<JuzNotFoundException>(() => Quran.GetJuz(number));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(31)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void GetJuzOrDefault_Invalid_Should_ReturnNull(int number)
    {
        Assert.Null(Quran.GetJuzOrDefault(number));
    }

    #endregion

    #region GetJuzNumber Edge Cases

    [Fact]
    public void GetJuzNumber_FirstAyahOfJuz1_Should_Return1()
    {
        Assert.Equal(1, Quran.GetJuzNumber(1, 1));
    }

    [Fact]
    public void GetJuzNumber_LastAyahOfQuran_Should_Return30()
    {
        Assert.Equal(30, Quran.GetJuzNumber(114, 6));
    }

    [Fact]
    public void GetJuzNumber_InvalidSurah_Should_Return0()
    {
        Assert.Equal(0, Quran.GetJuzNumber(999, 1));
    }

    [Fact]
    public void GetJuzNumber_InvalidAyah_Should_Return0()
    {
        Assert.Equal(0, Quran.GetJuzNumber(1, 99999));
    }

    [Fact]
    public void GetJuzNumber_FirstAyahOfEachJuz_Should_MatchJuzNumber()
    {
        var allJuz = Quran.GetAllJuz();
        foreach (var juz in allJuz)
        {
            Assert.Equal(juz.Number, Quran.GetJuzNumber(juz.StartSurah, juz.StartAyah));
        }
    }

    [Fact]
    public void GetJuzNumber_LastAyahOfEachJuz_Should_MatchJuzNumber()
    {
        var allJuz = Quran.GetAllJuz();
        foreach (var juz in allJuz)
        {
            Assert.Equal(juz.Number, Quran.GetJuzNumber(juz.EndSurah, juz.EndAyah));
        }
    }

    #endregion

    #region GetSurahsByJuz Edge Cases

    [Fact]
    public void GetSurahsByJuz_InvalidJuz0_Should_Throw()
    {
        Assert.Throws<JuzNotFoundException>(() => Quran.GetSurahsByJuz(0));
    }

    [Fact]
    public void GetSurahsByJuz_InvalidJuz31_Should_Throw()
    {
        Assert.Throws<JuzNotFoundException>(() => Quran.GetSurahsByJuz(31));
    }

    [Fact]
    public void GetSurahsByJuz_Each1To30_Should_ReturnNonEmpty()
    {
        for (int j = 1; j <= 30; j++)
        {
            var surahs = Quran.GetSurahsByJuz(j);
            Assert.True(surahs.Count > 0, $"Juz {j} returned no surahs");
        }
    }

    [Fact]
    public void GetSurahsByJuz_Juz30_Should_ContainManyShortSurahs()
    {
        var surahs = Quran.GetSurahsByJuz(30);
        Assert.True(surahs.Count >= 30, $"Juz 30 should have many surahs, but got {surahs.Count}");
    }

    #endregion

    #region Search Edge Cases

    [Fact]
    public void Search_NullTerm_Should_ReturnEmpty()
    {
        Assert.Empty(Quran.Search(null!));
    }

    [Fact]
    public void Search_EmptyTerm_Should_ReturnEmpty()
    {
        Assert.Empty(Quran.Search(""));
    }

    [Fact]
    public void Search_WhitespaceTerm_Should_ReturnEmpty()
    {
        Assert.Empty(Quran.Search("   "));
    }

    [Fact]
    public void Search_InSurah_NullTerm_Should_ReturnEmpty()
    {
        Assert.Empty(Quran.Search(null!, 1));
    }

    [Fact]
    public void Search_InSurah_EmptyTerm_Should_ReturnEmpty()
    {
        Assert.Empty(Quran.Search("", 1));
    }

    [Fact]
    public void Search_InInvalidSurah_Should_ThrowSurahNotFound()
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.Search("الله", 0));
    }

    [Fact]
    public void Search_InInvalidSurah115_Should_ThrowSurahNotFound()
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.Search("الله", 115));
    }

    [Fact]
    public void Search_NonArabicText_Should_ReturnEmpty()
    {
        var results = Quran.Search("Hello World");
        Assert.Empty(results);
    }

    [Fact]
    public void Search_LatinCharacters_Should_ReturnEmpty()
    {
        var results = Quran.Search("xyz");
        Assert.Empty(results);
    }

    [Fact]
    public void Search_VeryLongTerm_Should_NotThrow()
    {
        var longTerm = new string('ب', 1000);
        var results = Quran.Search(longTerm);
        // Should not throw, just return empty or results
        Assert.NotNull(results);
    }

    #endregion

    #region GetSurahNames Edge Cases

    [Fact]
    public void GetSurahNames_Default_Should_Return114()
    {
        var names = Quran.GetSurahNames();
        Assert.Equal(114, names.Count);
    }

    [Fact]
    public void GetSurahNames_SingleSurah_Should_ReturnOne()
    {
        var names = Quran.GetSurahNames(1, 1);
        Assert.Single(names);
        Assert.Equal(1, names[0].Number);
    }

    [Fact]
    public void GetSurahNames_ReverseRange_Should_ReturnEmpty()
    {
        var names = Quran.GetSurahNames(114, 1);
        Assert.Empty(names);
    }

    [Fact]
    public void GetSurahNames_OutOfRange_Should_ReturnEmpty()
    {
        var names = Quran.GetSurahNames(200, 300);
        Assert.Empty(names);
    }

    [Fact]
    public void GetSurahNames_Should_ReturnTupleWithCorrectFields()
    {
        var names = Quran.GetSurahNames(1, 1);
        var name = names[0];
        Assert.Equal(1, name.Number);
        Assert.Equal("Al-Fatiha", name.EnglishName);
        Assert.Equal("الفاتحة", name.ArabicName);
    }

    #endregion

    #region GetAyahCount Edge Cases

    [Fact]
    public void GetAyahCount_InvalidSurah_Should_Throw()
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.GetAyahCount(0));
    }

    [Fact]
    public void GetAyahCount_InvalidSurah115_Should_Throw()
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.GetAyahCount(115));
    }

    [Fact]
    public void GetAyahCount_AllSurahs_Should_ReturnPositive()
    {
        for (int i = 1; i <= 114; i++)
        {
            Assert.True(Quran.GetAyahCount(i) > 0);
        }
    }

    #endregion

    #region IsValid Edge Cases

    [Theory]
    [InlineData(1, true)]
    [InlineData(57, true)]
    [InlineData(114, true)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    [InlineData(115, false)]
    [InlineData(int.MinValue, false)]
    [InlineData(int.MaxValue, false)]
    public void IsValidSurah_Number_Should_ReturnCorrect(int number, bool expected)
    {
        Assert.Equal(expected, Quran.IsValidSurah(number));
    }

    [Theory]
    [InlineData("Al-Fatiha", true)]
    [InlineData("al-fatiha", true)]
    [InlineData("AL-FATIHA", true)]
    [InlineData("An-Nas", true)]
    [InlineData("NonExistent", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    [InlineData("   ", false)]
    public void IsValidSurah_Name_Should_ReturnCorrect(string? name, bool expected)
    {
        Assert.Equal(expected, Quran.IsValidSurah(name!));
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(15, true)]
    [InlineData(30, true)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    [InlineData(31, false)]
    [InlineData(int.MinValue, false)]
    [InlineData(int.MaxValue, false)]
    public void IsValidJuz_Should_ReturnCorrect(int number, bool expected)
    {
        Assert.Equal(expected, Quran.IsValidJuz(number));
    }

    #endregion
}
