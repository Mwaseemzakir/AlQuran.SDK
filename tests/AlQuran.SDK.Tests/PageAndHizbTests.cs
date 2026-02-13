using AlQuran.SDK;
using AlQuran.SDK.Enums;
using AlQuran.SDK.Models;
using FluentAssertions;

namespace AlQuran.SDK.Tests;

public class PageAndHizbTests
{
    #region Constants

    [Fact]
    public void TotalPages_Is_604()
    {
        Quran.TotalPages.Should().Be(604);
    }

    [Fact]
    public void TotalHizbQuarters_Is_240()
    {
        Quran.TotalHizbQuarters.Should().Be(240);
    }

    #endregion

    #region GetAyahsByPage

    [Fact]
    public void GetAyahsByPage_Page1_Returns_AlFatiha_Start()
    {
        var ayahs = Quran.GetAyahsByPage(1);
        ayahs.Should().NotBeEmpty();
        ayahs[0].SurahNumber.Should().Be(1);
        ayahs[0].AyahNumber.Should().Be(1);
    }

    [Fact]
    public void GetAyahsByPage_Page604_Returns_Last_Ayahs()
    {
        var ayahs = Quran.GetAyahsByPage(604);
        ayahs.Should().NotBeEmpty();
        var last = ayahs[^1];
        last.SurahNumber.Should().Be(114);
        last.AyahNumber.Should().Be(6);
    }

    [Fact]
    public void GetAyahsByPage_AllPages_Cover_All_Ayahs()
    {
        int total = 0;
        for (int p = 1; p <= Quran.TotalPages; p++)
        {
            total += Quran.GetAyahsByPage(p).Count;
        }
        total.Should().Be(Quran.TotalAyahs, "all 604 pages should cover 6236 ayahs");
    }

    [Fact]
    public void GetAyahsByPage_Every_Page_Has_At_Least_One_Ayah()
    {
        for (int p = 1; p <= Quran.TotalPages; p++)
        {
            Quran.GetAyahsByPage(p).Should().NotBeEmpty($"page {p} should have at least one ayah");
        }
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(605)]
    [InlineData(1000)]
    public void GetAyahsByPage_Invalid_Throws(int page)
    {
        var act = () => Quran.GetAyahsByPage(page);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void GetAyahsByPage_SimpleScript()
    {
        var ayahs = Quran.GetAyahsByPage(1, ScriptType.Simple);
        ayahs.Should().NotBeEmpty();
    }

    [Fact]
    public void GetAyahsByPage_Each_Ayah_Has_Matching_Page()
    {
        const int testPage = 50;
        var ayahs = Quran.GetAyahsByPage(testPage);
        ayahs.Should().AllSatisfy(a => a.Page.Should().Be(testPage));
    }

    #endregion

    #region GetPageNumber

    [Fact]
    public void GetPageNumber_Fatiha_1_Returns_Page1()
    {
        Quran.GetPageNumber(1, 1).Should().Be(1);
    }

    [Fact]
    public void GetPageNumber_AyatAlKursi_Returns_Valid_Page()
    {
        var page = Quran.GetPageNumber(2, 255);
        page.Should().BeInRange(1, 604);
    }

    [Fact]
    public void GetPageNumber_LastAyah_Returns_Page604()
    {
        Quran.GetPageNumber(114, 6).Should().Be(604);
    }

    [Fact]
    public void GetPageNumber_Invalid_Returns_Zero()
    {
        Quran.GetPageNumber(999, 1).Should().Be(0);
    }

    [Fact]
    public void GetPageNumber_RoundTrip_With_GetAyahsByPage()
    {
        // Get the page for a known ayah, then verify it's on that page
        var page = Quran.GetPageNumber(2, 1);
        var ayahsOnPage = Quran.GetAyahsByPage(page);
        ayahsOnPage.Should().Contain(a => a.SurahNumber == 2 && a.AyahNumber == 1);
    }

    #endregion

    #region GetAyahsByHizbQuarter

    [Fact]
    public void GetAyahsByHizbQuarter_First_Returns_NonEmpty()
    {
        var ayahs = Quran.GetAyahsByHizbQuarter(1);
        ayahs.Should().NotBeEmpty();
        ayahs[0].SurahNumber.Should().Be(1);
        ayahs[0].AyahNumber.Should().Be(1);
    }

    [Fact]
    public void GetAyahsByHizbQuarter_Last_Returns_NonEmpty()
    {
        var ayahs = Quran.GetAyahsByHizbQuarter(240);
        ayahs.Should().NotBeEmpty();
    }

    [Fact]
    public void GetAyahsByHizbQuarter_AllQuarters_Cover_All_Ayahs()
    {
        int total = 0;
        for (int hq = 1; hq <= Quran.TotalHizbQuarters; hq++)
        {
            total += Quran.GetAyahsByHizbQuarter(hq).Count;
        }
        total.Should().Be(Quran.TotalAyahs, "all 240 hizb quarters should cover 6236 ayahs");
    }

    [Fact]
    public void GetAyahsByHizbQuarter_Every_Quarter_Has_At_Least_One_Ayah()
    {
        for (int hq = 1; hq <= Quran.TotalHizbQuarters; hq++)
        {
            Quran.GetAyahsByHizbQuarter(hq).Should().NotBeEmpty($"hizb quarter {hq} should have at least one ayah");
        }
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(241)]
    [InlineData(1000)]
    public void GetAyahsByHizbQuarter_Invalid_Throws(int hizbQuarter)
    {
        var act = () => Quran.GetAyahsByHizbQuarter(hizbQuarter);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void GetAyahsByHizbQuarter_Each_Ayah_Has_Matching_HizbQuarter()
    {
        const int testHq = 10;
        var ayahs = Quran.GetAyahsByHizbQuarter(testHq);
        ayahs.Should().AllSatisfy(a => a.HizbQuarter.Should().Be(testHq));
    }

    #endregion

    #region GetHizbQuarter

    [Fact]
    public void GetHizbQuarter_Fatiha_1_Returns_1()
    {
        Quran.GetHizbQuarter(1, 1).Should().Be(1);
    }

    [Fact]
    public void GetHizbQuarter_LastAyah_Returns_240()
    {
        Quran.GetHizbQuarter(114, 6).Should().Be(240);
    }

    [Fact]
    public void GetHizbQuarter_Invalid_Returns_Zero()
    {
        Quran.GetHizbQuarter(999, 1).Should().Be(0);
    }

    [Fact]
    public void GetHizbQuarter_RoundTrip_With_GetAyahsByHizbQuarter()
    {
        var hq = Quran.GetHizbQuarter(2, 1);
        var ayahsInQuarter = Quran.GetAyahsByHizbQuarter(hq);
        ayahsInQuarter.Should().Contain(a => a.SurahNumber == 2 && a.AyahNumber == 1);
    }

    #endregion

    #region Page / Hizb Consistency

    [Fact]
    public void Pages_Are_In_Ascending_Order_Within_Quran()
    {
        int prevPage = 0;
        for (int s = 1; s <= Quran.TotalSurahs; s++)
        {
            var surah = Quran.GetSurah(s);
            for (int a = 1; a <= surah.AyahCount; a++)
            {
                var page = Quran.GetPageNumber(s, a);
                page.Should().BeGreaterThanOrEqualTo(prevPage,
                    $"page for {s}:{a} should be >= previous page {prevPage}");
                prevPage = page;
            }
        }
    }

    [Fact]
    public void HizbQuarters_Are_In_Ascending_Order_Within_Quran()
    {
        int prevHq = 0;
        for (int s = 1; s <= Quran.TotalSurahs; s++)
        {
            var surah = Quran.GetSurah(s);
            for (int a = 1; a <= surah.AyahCount; a++)
            {
                var hq = Quran.GetHizbQuarter(s, a);
                hq.Should().BeGreaterThanOrEqualTo(prevHq,
                    $"hizb quarter for {s}:{a} should be >= previous {prevHq}");
                prevHq = hq;
            }
        }
    }

    #endregion
}
