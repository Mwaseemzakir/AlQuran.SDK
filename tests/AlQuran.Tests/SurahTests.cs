using AlQuran.Enums;
using AlQuran.Exceptions;
using Xunit;

namespace AlQuran.Tests;

public class SurahTests
{
    #region GetAllSurahs

    [Fact]
    public void GetAllSurahs_Should_Return114Surahs()
    {
        var surahs = Quran.GetAllSurahs();
        Assert.Equal(114, surahs.Count);
    }

    [Fact]
    public void GetAllSurahs_Should_ReturnNewListEachTime()
    {
        var list1 = Quran.GetAllSurahs();
        var list2 = Quran.GetAllSurahs();
        Assert.NotSame(list1, list2);
    }

    [Fact]
    public void GetAllSurahs_Should_BeOrderedByNumber()
    {
        var surahs = Quran.GetAllSurahs();
        for (int i = 0; i < surahs.Count; i++)
        {
            Assert.Equal(i + 1, surahs[i].Number);
        }
    }

    [Fact]
    public void GetAllSurahs_Should_HaveNoDuplicateNumbers()
    {
        var surahs = Quran.GetAllSurahs();
        var numbers = surahs.Select(s => s.Number).ToHashSet();
        Assert.Equal(114, numbers.Count);
    }

    [Fact]
    public void GetAllSurahs_Should_HaveNoDuplicateEnglishNames()
    {
        var surahs = Quran.GetAllSurahs();
        var names = surahs.Select(s => s.EnglishName).ToHashSet();
        Assert.Equal(114, names.Count);
    }

    #endregion

    #region GetSurah By Number

    [Theory]
    [InlineData(1, "Al-Fatiha", "الفاتحة", 7)]
    [InlineData(2, "Al-Baqarah", "البقرة", 286)]
    [InlineData(36, "Ya-Sin", "يس", 83)]
    [InlineData(55, "Ar-Rahman", "الرحمن", 78)]
    [InlineData(112, "Al-Ikhlas", "الإخلاص", 4)]
    [InlineData(114, "An-Nas", "الناس", 6)]
    public void GetSurah_ByNumber_Should_ReturnCorrectSurah(int number, string expectedEnglish, string expectedArabic, int expectedAyahs)
    {
        var surah = Quran.GetSurah(number);

        Assert.Equal(number, surah.Number);
        Assert.Equal(expectedEnglish, surah.EnglishName);
        Assert.Equal(expectedArabic, surah.ArabicName);
        Assert.Equal(expectedAyahs, surah.AyahCount);
    }

    [Fact]
    public void GetSurah_ByNumber_Should_ReturnSameInstance()
    {
        var surah1 = Quran.GetSurah(1);
        var surah2 = Quran.GetSurah(1);
        Assert.Same(surah1, surah2);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(115)]
    [InlineData(999)]
    public void GetSurah_ByInvalidNumber_Should_ThrowSurahNotFoundException(int number)
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.GetSurah(number));
    }

    #endregion

    #region GetSurah By SurahName Enum

    [Theory]
    [InlineData(SurahName.AlFatiha, 1)]
    [InlineData(SurahName.AlBaqarah, 2)]
    [InlineData(SurahName.YaSin, 36)]
    [InlineData(SurahName.AnNas, 114)]
    public void GetSurah_BySurahName_Should_ReturnCorrectSurah(SurahName name, int expectedNumber)
    {
        var surah = Quran.GetSurah(name);
        Assert.Equal(expectedNumber, surah.Number);
    }

    #endregion

    #region GetSurah By English Name

    [Theory]
    [InlineData("Al-Fatiha", 1)]
    [InlineData("al-fatiha", 1)]
    [InlineData("AL-FATIHA", 1)]
    [InlineData("Al-Baqarah", 2)]
    [InlineData("An-Nas", 114)]
    public void GetSurah_ByEnglishName_Should_ReturnCorrectSurah_CaseInsensitive(string name, int expectedNumber)
    {
        var surah = Quran.GetSurah(name);
        Assert.Equal(expectedNumber, surah.Number);
    }

    [Fact]
    public void GetSurah_ByNullName_Should_ThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Quran.GetSurah((string)null!));
    }

    [Fact]
    public void GetSurah_ByEmptyName_Should_ThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Quran.GetSurah(""));
    }

    [Fact]
    public void GetSurah_ByInvalidName_Should_ThrowSurahNotFoundException()
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.GetSurah("NonExistentSurah"));
    }

    #endregion

    #region GetSurahOrDefault

    [Fact]
    public void GetSurahOrDefault_ByValidNumber_Should_ReturnSurah()
    {
        var surah = Quran.GetSurahOrDefault(1);
        Assert.NotNull(surah);
        Assert.Equal("Al-Fatiha", surah!.EnglishName);
    }

    [Fact]
    public void GetSurahOrDefault_ByInvalidNumber_Should_ReturnNull()
    {
        var surah = Quran.GetSurahOrDefault(0);
        Assert.Null(surah);
    }

    [Fact]
    public void GetSurahOrDefault_ByValidName_Should_ReturnSurah()
    {
        var surah = Quran.GetSurahOrDefault("Al-Fatiha");
        Assert.NotNull(surah);
        Assert.Equal(1, surah!.Number);
    }

    [Fact]
    public void GetSurahOrDefault_ByInvalidName_Should_ReturnNull()
    {
        var surah = Quran.GetSurahOrDefault("NonExistent");
        Assert.Null(surah);
    }

    [Fact]
    public void GetSurahOrDefault_ByNullName_Should_ReturnNull()
    {
        var surah = Quran.GetSurahOrDefault((string)null!);
        Assert.Null(surah);
    }

    #endregion

    #region GetSurahByArabicName

    [Theory]
    [InlineData("الفاتحة", 1)]
    [InlineData("البقرة", 2)]
    [InlineData("الناس", 114)]
    public void GetSurahByArabicName_Should_ReturnCorrectSurah(string arabicName, int expectedNumber)
    {
        var surah = Quran.GetSurahByArabicName(arabicName);
        Assert.Equal(expectedNumber, surah.Number);
    }

    [Fact]
    public void GetSurahByArabicName_WithInvalidName_Should_ThrowSurahNotFoundException()
    {
        Assert.Throws<SurahNotFoundException>(() => Quran.GetSurahByArabicName("غير موجود"));
    }

    #endregion

    #region IsValidSurah

    [Theory]
    [InlineData(1, true)]
    [InlineData(114, true)]
    [InlineData(57, true)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    [InlineData(115, false)]
    public void IsValidSurah_ByNumber_Should_ReturnCorrectResult(int number, bool expected)
    {
        Assert.Equal(expected, Quran.IsValidSurah(number));
    }

    [Theory]
    [InlineData("Al-Fatiha", true)]
    [InlineData("al-baqarah", true)]
    [InlineData("NonExistent", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsValidSurah_ByName_Should_ReturnCorrectResult(string? name, bool expected)
    {
        Assert.Equal(expected, Quran.IsValidSurah(name!));
    }

    #endregion

    #region Revelation Type Filtering

    [Fact]
    public void GetMeccanSurahs_Should_ReturnOnlyMeccanSurahs()
    {
        var meccan = Quran.GetMeccanSurahs();
        Assert.True(meccan.Count > 0);
        Assert.All(meccan, s => Assert.Equal(RevelationType.Meccan, s.RevelationType));
    }

    [Fact]
    public void GetMedinanSurahs_Should_ReturnOnlyMedinanSurahs()
    {
        var medinan = Quran.GetMedinanSurahs();
        Assert.True(medinan.Count > 0);
        Assert.All(medinan, s => Assert.Equal(RevelationType.Medinan, s.RevelationType));
    }

    [Fact]
    public void MeccanAndMedinan_Combined_Should_Equal114()
    {
        var meccan = Quran.GetMeccanSurahs();
        var medinan = Quran.GetMedinanSurahs();
        Assert.Equal(114, meccan.Count + medinan.Count);
    }

    #endregion

    #region Surah Properties

    [Fact]
    public void AlFatiha_Should_HaveCorrectProperties()
    {
        var surah = Quran.GetSurah(1);

        Assert.Equal(1, surah.Number);
        Assert.Equal("الفاتحة", surah.ArabicName);
        Assert.Equal("Al-Fatiha", surah.EnglishName);
        Assert.Equal("The Opening", surah.EnglishMeaning);
        Assert.Equal(7, surah.AyahCount);
        Assert.Equal(RevelationType.Meccan, surah.RevelationType);
        Assert.Equal(5, surah.RevelationOrder);
        Assert.True(surah.HasBismillah);
        Assert.Equal(SurahName.AlFatiha, surah.Name);
    }

    [Fact]
    public void AtTawbah_Should_NotHaveBismillah()
    {
        var surah = Quran.GetSurah(9);
        Assert.False(surah.HasBismillah);
    }

    [Fact]
    public void AllSurahsExceptTawbah_Should_HaveBismillah()
    {
        var surahs = Quran.GetAllSurahs();
        var withoutBismillah = surahs.Where(s => !s.HasBismillah).ToList();
        Assert.Single(withoutBismillah);
        Assert.Equal(9, withoutBismillah[0].Number);
    }

    [Fact]
    public void GetSurahsByRevelationOrder_Should_ReturnCorrectOrder()
    {
        var ordered = Quran.GetSurahsByRevelationOrder();
        Assert.Equal(114, ordered.Count);

        // Al-Alaq (96) is the first revealed surah
        Assert.Equal(96, ordered[0].Number);
        // An-Nasr (110) is the last revealed surah
        Assert.Equal(110, ordered[113].Number);
    }

    #endregion

    #region Data Integrity

    [Fact]
    public void AllSurahs_Should_HaveNonEmptyNames()
    {
        var surahs = Quran.GetAllSurahs();
        Assert.All(surahs, s =>
        {
            Assert.False(string.IsNullOrWhiteSpace(s.ArabicName));
            Assert.False(string.IsNullOrWhiteSpace(s.EnglishName));
            Assert.False(string.IsNullOrWhiteSpace(s.EnglishMeaning));
        });
    }

    [Fact]
    public void AllSurahs_Should_HavePositiveAyahCount()
    {
        var surahs = Quran.GetAllSurahs();
        Assert.All(surahs, s => Assert.True(s.AyahCount > 0));
    }

    [Fact]
    public void AllSurahs_Should_HaveValidRevelationOrder()
    {
        var surahs = Quran.GetAllSurahs();
        var orders = surahs.Select(s => s.RevelationOrder).ToHashSet();
        Assert.Equal(114, orders.Count);
        Assert.All(orders, o => Assert.InRange(o, 1, 114));
    }

    [Fact]
    public void AllSurahs_Should_HaveValidJuzRange()
    {
        var surahs = Quran.GetAllSurahs();
        Assert.All(surahs, s =>
        {
            Assert.InRange(s.JuzStart, 1, 30);
            Assert.InRange(s.JuzEnd, 1, 30);
            Assert.True(s.JuzEnd >= s.JuzStart);
        });
    }

    [Fact]
    public void TotalAyahCount_Should_Equal6236()
    {
        var surahs = Quran.GetAllSurahs();
        var total = surahs.Sum(s => s.AyahCount);
        Assert.Equal(6236, total);
    }

    [Fact]
    public void Surah_ToString_Should_ReturnFormattedString()
    {
        var surah = Quran.GetSurah(1);
        var result = surah.ToString();
        Assert.Contains("Al-Fatiha", result);
        Assert.Contains("الفاتحة", result);
        Assert.Contains("7", result);
    }

    #endregion
}
