using AlQuran.SDK.Enums;

namespace AlQuran.SDK.Tests;

public class SajdaTests
{
    [Fact]
    public void GetAllSajdas_Should_Return15Sajdas()
    {
        var sajdas = Quran.GetAllSajdas();
        Assert.Equal(15, sajdas.Count);
    }

    [Fact]
    public void GetAllSajdas_Should_ReturnNewListEachTime()
    {
        var list1 = Quran.GetAllSajdas();
        var list2 = Quran.GetAllSajdas();
        Assert.NotSame(list1, list2);
    }

    [Fact]
    public void GetObligatorySajdas_Should_ReturnOnlyObligatory()
    {
        var obligatory = Quran.GetObligatorySajdas();
        Assert.True(obligatory.Count > 0);
        Assert.All(obligatory, s => Assert.Equal(SajdaType.Obligatory, s.Type));
    }

    [Fact]
    public void GetRecommendedSajdas_Should_ReturnOnlyRecommended()
    {
        var recommended = Quran.GetRecommendedSajdas();
        Assert.True(recommended.Count > 0);
        Assert.All(recommended, s => Assert.Equal(SajdaType.Recommended, s.Type));
    }

    [Fact]
    public void ObligatoryAndRecommended_Combined_Should_Equal15()
    {
        var obligatory = Quran.GetObligatorySajdas();
        var recommended = Quran.GetRecommendedSajdas();
        Assert.Equal(15, obligatory.Count + recommended.Count);
    }

    [Theory]
    [InlineData(7, 206, true)]     // Al-A'raf
    [InlineData(32, 15, true)]     // As-Sajdah (obligatory)
    [InlineData(96, 19, true)]     // Al-'Alaq
    [InlineData(1, 1, false)]      // Al-Fatiha verse 1 is not sajda
    [InlineData(2, 1, false)]      // Al-Baqarah verse 1 is not sajda
    public void IsSajdaAyah_Should_ReturnCorrectResult(int surah, int ayah, bool expected)
    {
        Assert.Equal(expected, Quran.IsSajdaAyah(surah, ayah));
    }

    [Fact]
    public void GetSajda_ForSajdaVerse_Should_ReturnSajdaInfo()
    {
        var sajda = Quran.GetSajda(32, 15);
        Assert.NotNull(sajda);
        Assert.Equal(32, sajda!.SurahNumber);
        Assert.Equal(15, sajda.AyahNumber);
        Assert.Equal(SajdaType.Obligatory, sajda.Type);
    }

    [Fact]
    public void GetSajda_ForNonSajdaVerse_Should_ReturnNull()
    {
        var sajda = Quran.GetSajda(1, 1);
        Assert.Null(sajda);
    }

    [Fact]
    public void AllSajdas_Should_HaveValidSurahReferences()
    {
        var sajdas = Quran.GetAllSajdas();
        Assert.All(sajdas, s =>
        {
            Assert.InRange(s.SurahNumber, 1, 114);
            Assert.True(s.AyahNumber > 0);

            // Verify sajda ayah exists within surah
            var surah = Quran.GetSurah(s.SurahNumber);
            Assert.True(s.AyahNumber <= surah.AyahCount,
                $"Sajda ayah {s.AyahNumber} exceeds surah {s.SurahNumber} ayah count of {surah.AyahCount}");
        });
    }

    [Fact]
    public void SajdaVerse_ToString_Should_ContainInfo()
    {
        var sajda = Quran.GetSajda(32, 15);
        Assert.NotNull(sajda);
        var str = sajda!.ToString();
        Assert.Contains("32:15", str);
        Assert.Contains("Obligatory", str);
    }
}
