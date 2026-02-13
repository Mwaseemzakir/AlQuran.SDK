using AlQuran.SDK;
using AlQuran.SDK.Enums;
using AlQuran.SDK.Exceptions;
using AlQuran.SDK.Models;
using FluentAssertions;

namespace AlQuran.SDK.Tests;

public class VerseReferenceTests
{
    #region VerseReference.Parse - Single Verse

    [Theory]
    [InlineData("1:1", 1, 1)]
    [InlineData("2:255", 2, 255)]
    [InlineData("114:6", 114, 6)]
    [InlineData("36:1", 36, 1)]
    [InlineData(" 2:255 ", 2, 255)] // whitespace trimming
    public void Parse_SingleVerse_Correct(string reference, int expectedSurah, int expectedAyah)
    {
        var vr = VerseReference.Parse(reference);
        vr.SurahNumber.Should().Be(expectedSurah);
        vr.AyahNumber.Should().Be(expectedAyah);
        vr.IsRange.Should().BeFalse();
        vr.EndAyahNumber.Should().BeNull();
    }

    #endregion

    #region VerseReference.Parse - Range

    [Theory]
    [InlineData("2:1-5", 2, 1, 5)]
    [InlineData("1:1-7", 1, 1, 7)]
    [InlineData("36:1-10", 36, 1, 10)]
    public void Parse_Range_Correct(string reference, int expectedSurah, int expectedStart, int expectedEnd)
    {
        var vr = VerseReference.Parse(reference);
        vr.SurahNumber.Should().Be(expectedSurah);
        vr.AyahNumber.Should().Be(expectedStart);
        vr.EndAyahNumber.Should().Be(expectedEnd);
        vr.IsRange.Should().BeTrue();
    }

    #endregion

    #region VerseReference.Parse - Invalid Formats

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public void Parse_NullOrEmpty_Throws(string? reference)
    {
        var act = () => VerseReference.Parse(reference!);
        act.Should().Throw<FormatException>();
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("2")]
    [InlineData("2:3:4")]
    [InlineData("surah:ayah")]
    public void Parse_InvalidFormat_Throws(string reference)
    {
        var act = () => VerseReference.Parse(reference);
        act.Should().Throw<FormatException>();
    }

    [Theory]
    [InlineData("0:1")]    // Surah 0 invalid
    [InlineData("115:1")]  // Surah 115 invalid
    [InlineData("-1:1")]   // Negative surah
    public void Parse_InvalidSurah_Throws(string reference)
    {
        var act = () => VerseReference.Parse(reference);
        act.Should().Throw<FormatException>();
    }

    [Theory]
    [InlineData("2:0")]     // Ayah 0 invalid
    [InlineData("2:-1")]    // Negative ayah
    [InlineData("2:abc")]   // Non-numeric
    public void Parse_InvalidAyah_Throws(string reference)
    {
        var act = () => VerseReference.Parse(reference);
        act.Should().Throw<FormatException>();
    }

    [Theory]
    [InlineData("2:5-3")]   // End < Start
    [InlineData("2:1-2-3")] // Multiple dashes
    public void Parse_InvalidRange_Throws(string reference)
    {
        var act = () => VerseReference.Parse(reference);
        act.Should().Throw<FormatException>();
    }

    #endregion

    #region VerseReference.TryParse

    [Fact]
    public void TryParse_Valid_Returns_True()
    {
        VerseReference.TryParse("2:255", out var result).Should().BeTrue();
        result.Should().NotBeNull();
        result!.SurahNumber.Should().Be(2);
        result.AyahNumber.Should().Be(255);
    }

    [Fact]
    public void TryParse_Invalid_Returns_False()
    {
        VerseReference.TryParse("invalid", out var result).Should().BeFalse();
        result.Should().BeNull();
    }

    [Fact]
    public void TryParse_Range_Returns_True()
    {
        VerseReference.TryParse("2:1-5", out var result).Should().BeTrue();
        result!.IsRange.Should().BeTrue();
        result.EndAyahNumber.Should().Be(5);
    }

    #endregion

    #region VerseReference.ToString

    [Fact]
    public void ToString_SingleVerse_Format()
    {
        var vr = VerseReference.Parse("2:255");
        vr.ToString().Should().Be("2:255");
    }

    [Fact]
    public void ToString_Range_Format()
    {
        var vr = VerseReference.Parse("2:1-5");
        vr.ToString().Should().Be("2:1-5");
    }

    #endregion

    #region Quran.ParseVerseReference

    [Fact]
    public void QuranParseVerseReference_Delegates_Correctly()
    {
        var vr = Quran.ParseVerseReference("2:255");
        vr.SurahNumber.Should().Be(2);
        vr.AyahNumber.Should().Be(255);
    }

    #endregion

    #region Quran.GetAyah (string reference)

    [Fact]
    public void GetAyah_ByReference_Returns_Correct_Ayah()
    {
        var ayah = Quran.GetAyah("2:255");
        ayah.SurahNumber.Should().Be(2);
        ayah.AyahNumber.Should().Be(255);
        ayah.Text.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void GetAyah_ByReference_Fatiha()
    {
        var ayah = Quran.GetAyah("1:1");
        ayah.SurahNumber.Should().Be(1);
        ayah.AyahNumber.Should().Be(1);
    }

    [Fact]
    public void GetAyah_ByReference_RespectsScriptType()
    {
        var uthmani = Quran.GetAyah("1:1", ScriptType.Uthmani);
        var simple = Quran.GetAyah("1:1", ScriptType.Simple);
        uthmani.Text.Should().NotBe(simple.Text, "Uthmani and Simple scripts should differ");
    }

    [Fact]
    public void GetAyah_ByReference_InvalidFormat_Throws_FormatException()
    {
        var act = () => Quran.GetAyah("invalid");
        act.Should().Throw<FormatException>();
    }

    [Fact]
    public void GetAyah_ByReference_InvalidSurah_Throws()
    {
        var act = () => Quran.GetAyah("999:1");
        act.Should().Throw<Exception>();
    }

    #endregion

    #region Quran.GetAyahRange

    [Fact]
    public void GetAyahRange_Returns_Correct_Range()
    {
        var ayahs = Quran.GetAyahRange("1:1-7");
        ayahs.Should().HaveCount(7);
        ayahs[0].SurahNumber.Should().Be(1);
        ayahs[0].AyahNumber.Should().Be(1);
        ayahs[6].AyahNumber.Should().Be(7);
    }

    [Fact]
    public void GetAyahRange_Single_Verse_Returns_One_Ayah()
    {
        var ayahs = Quran.GetAyahRange("2:255");
        ayahs.Should().HaveCount(1);
        ayahs[0].SurahNumber.Should().Be(2);
        ayahs[0].AyahNumber.Should().Be(255);
    }

    [Fact]
    public void GetAyahRange_RespectsScriptType()
    {
        var uthmani = Quran.GetAyahRange("1:1-3", ScriptType.Uthmani);
        var simple = Quran.GetAyahRange("1:1-3", ScriptType.Simple);
        uthmani.Should().HaveCount(simple.Count);
        uthmani[0].Text.Should().NotBe(simple[0].Text);
    }

    [Fact]
    public void GetAyahRange_First_Five_Of_Baqarah()
    {
        var ayahs = Quran.GetAyahRange("2:1-5");
        ayahs.Should().HaveCount(5);
        for (int i = 0; i < 5; i++)
        {
            ayahs[i].SurahNumber.Should().Be(2);
            ayahs[i].AyahNumber.Should().Be(i + 1);
        }
    }

    #endregion
}
