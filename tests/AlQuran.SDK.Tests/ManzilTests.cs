using AlQuran.SDK;
using AlQuran.SDK.Enums;
using AlQuran.SDK.Models;
using FluentAssertions;

namespace AlQuran.SDK.Tests;

public class ManzilTests
{
    #region GetManzil

    [Theory]
    [InlineData(1, 1, 1, 4, 126)]
    [InlineData(2, 4, 127, 9, 92)]
    [InlineData(3, 9, 93, 16, 128)]
    [InlineData(4, 17, 1, 25, 20)]
    [InlineData(5, 25, 21, 36, 27)]
    [InlineData(6, 36, 28, 48, 29)]
    [InlineData(7, 49, 1, 114, 6)]
    public void GetManzil_Returns_Correct_Boundaries(int number, int startSurah, int startAyah, int endSurah, int endAyah)
    {
        var manzil = Quran.GetManzil(number);

        manzil.Number.Should().Be(number);
        manzil.StartSurah.Should().Be(startSurah);
        manzil.StartAyah.Should().Be(startAyah);
        manzil.EndSurah.Should().Be(endSurah);
        manzil.EndAyah.Should().Be(endAyah);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(8)]
    [InlineData(100)]
    public void GetManzil_Invalid_Number_Throws(int number)
    {
        var act = () => Quran.GetManzil(number);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    #endregion

    #region GetManzilOrDefault

    [Fact]
    public void GetManzilOrDefault_Valid_Returns_Manzil()
    {
        var manzil = Quran.GetManzilOrDefault(1);
        manzil.Should().NotBeNull();
        manzil!.Number.Should().Be(1);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(8)]
    [InlineData(-1)]
    public void GetManzilOrDefault_Invalid_Returns_Null(int number)
    {
        Quran.GetManzilOrDefault(number).Should().BeNull();
    }

    #endregion

    #region GetAllManzils

    [Fact]
    public void GetAllManzils_Returns_Seven()
    {
        var manzils = Quran.GetAllManzils();
        manzils.Should().HaveCount(7);
    }

    [Fact]
    public void GetAllManzils_Numbers_Are_Sequential()
    {
        var manzils = Quran.GetAllManzils();
        for (int i = 0; i < manzils.Count; i++)
        {
            manzils[i].Number.Should().Be(i + 1);
        }
    }

    [Fact]
    public void GetAllManzils_Cover_Entire_Quran()
    {
        var manzils = Quran.GetAllManzils();

        // First manzil starts at 1:1
        manzils[0].StartSurah.Should().Be(1);
        manzils[0].StartAyah.Should().Be(1);

        // Last manzil ends at 114:6
        manzils[6].EndSurah.Should().Be(114);
        manzils[6].EndAyah.Should().Be(6);

        // Each manzil starts where the previous one ended
        for (int i = 1; i < manzils.Count; i++)
        {
            var prev = manzils[i - 1];
            var curr = manzils[i];

            // Current start should follow previous end
            if (prev.EndSurah == curr.StartSurah)
            {
                curr.StartAyah.Should().Be(prev.EndAyah + 1,
                    $"Manzil {curr.Number} should start right after Manzil {prev.Number}");
            }
            else
            {
                curr.StartSurah.Should().Be(prev.EndSurah + 1,
                    $"Manzil {curr.Number} should start in the surah right after Manzil {prev.Number}'s end surah");
                curr.StartAyah.Should().Be(1,
                    $"Manzil {curr.Number} should start at ayah 1 of a new surah");
            }
        }
    }

    [Fact]
    public void GetAllManzils_Returns_New_List_Instance()
    {
        var manzils1 = Quran.GetAllManzils();
        var manzils2 = Quran.GetAllManzils();
        manzils1.Should().NotBeSameAs(manzils2);
    }

    #endregion

    #region GetManzilNumber

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(2, 255, 1)]
    [InlineData(4, 126, 1)]
    [InlineData(4, 127, 2)]
    [InlineData(9, 92, 2)]
    [InlineData(9, 93, 3)]
    [InlineData(16, 128, 3)]
    [InlineData(17, 1, 4)]
    [InlineData(25, 20, 4)]
    [InlineData(25, 21, 5)]
    [InlineData(36, 27, 5)]
    [InlineData(36, 28, 6)]
    [InlineData(48, 29, 6)]
    [InlineData(49, 1, 7)]
    [InlineData(114, 6, 7)]
    public void GetManzilNumber_Returns_Correct_Manzil(int surah, int ayah, int expectedManzil)
    {
        Quran.GetManzilNumber(surah, ayah).Should().Be(expectedManzil);
    }

    #endregion

    #region GetAyahsByManzil

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    public void GetAyahsByManzil_Returns_NonEmpty_For_Each_Manzil(int manzilNumber)
    {
        var ayahs = Quran.GetAyahsByManzil(manzilNumber);
        ayahs.Should().NotBeEmpty();
    }

    [Fact]
    public void GetAyahsByManzil_All_Seven_Cover_6236_Ayahs()
    {
        int totalAyahs = 0;
        for (int i = 1; i <= 7; i++)
        {
            totalAyahs += Quran.GetAyahsByManzil(i).Count;
        }
        totalAyahs.Should().Be(Quran.TotalAyahs, "all 7 manzils should cover all 6236 ayahs");
    }

    [Fact]
    public void GetAyahsByManzil_First_Manzil_Starts_With_Fatiha()
    {
        var ayahs = Quran.GetAyahsByManzil(1);
        var first = ayahs[0];
        first.SurahNumber.Should().Be(1);
        first.AyahNumber.Should().Be(1);
    }

    [Fact]
    public void GetAyahsByManzil_Last_Manzil_Ends_With_AnNas()
    {
        var ayahs = Quran.GetAyahsByManzil(7);
        var last = ayahs[^1];
        last.SurahNumber.Should().Be(114);
        last.AyahNumber.Should().Be(6);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(8)]
    public void GetAyahsByManzil_Invalid_Throws(int manzilNumber)
    {
        var act = () => Quran.GetAyahsByManzil(manzilNumber);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void GetAyahsByManzil_Supports_Both_Scripts()
    {
        var uthmani = Quran.GetAyahsByManzil(1, ScriptType.Uthmani);
        var simple = Quran.GetAyahsByManzil(1, ScriptType.Simple);
        uthmani.Count.Should().Be(simple.Count);
    }

    #endregion

    #region IsValidManzil

    [Theory]
    [InlineData(1, true)]
    [InlineData(7, true)]
    [InlineData(4, true)]
    [InlineData(0, false)]
    [InlineData(8, false)]
    [InlineData(-1, false)]
    public void IsValidManzil_Returns_Expected(int number, bool expected)
    {
        Quran.IsValidManzil(number).Should().Be(expected);
    }

    #endregion

    #region Manzil Model

    [Fact]
    public void Manzil_ToString_Contains_Number_And_Range()
    {
        var manzil = Quran.GetManzil(1);
        var str = manzil.ToString();
        str.Should().Contain("Manzil 1");
        str.Should().Contain("1:1");
        str.Should().Contain("4:126");
    }

    #endregion

    #region Constants

    [Fact]
    public void TotalManzils_Is_Seven()
    {
        Quran.TotalManzils.Should().Be(7);
    }

    #endregion
}
