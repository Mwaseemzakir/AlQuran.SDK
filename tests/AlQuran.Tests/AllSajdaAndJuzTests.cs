using AlQuran.Enums;

namespace AlQuran.Tests;

/// <summary>
/// Exhaustive tests for all 15 Sajda positions and all 30 Juz.
/// </summary>
public class AllSajdaAndJuzTests
{
    #region All 15 Sajda Positions

    [Theory]
    [InlineData(1,  7,  206, SajdaType.Recommended)]   // Al-A'raf
    [InlineData(2,  13, 15,  SajdaType.Recommended)]   // Ar-Ra'd
    [InlineData(3,  16, 50,  SajdaType.Recommended)]   // An-Nahl
    [InlineData(4,  17, 109, SajdaType.Recommended)]   // Al-Isra
    [InlineData(5,  19, 58,  SajdaType.Recommended)]   // Maryam
    [InlineData(6,  22, 18,  SajdaType.Recommended)]   // Al-Hajj
    [InlineData(7,  22, 77,  SajdaType.Recommended)]   // Al-Hajj (2nd)
    [InlineData(8,  25, 60,  SajdaType.Recommended)]   // Al-Furqan
    [InlineData(9,  27, 26,  SajdaType.Recommended)]   // An-Naml
    [InlineData(10, 32, 15,  SajdaType.Obligatory)]    // As-Sajdah
    [InlineData(11, 38, 24,  SajdaType.Recommended)]   // Sad
    [InlineData(12, 41, 38,  SajdaType.Recommended)]   // Fussilat
    [InlineData(13, 53, 62,  SajdaType.Recommended)]   // An-Najm
    [InlineData(14, 84, 21,  SajdaType.Recommended)]   // Al-Inshiqaq
    [InlineData(15, 96, 19,  SajdaType.Recommended)]   // Al-'Alaq
    public void Sajda_Should_HaveCorrectData(int number, int surah, int ayah, SajdaType type)
    {
        var sajdas = Quran.GetAllSajdas();
        var sajda = sajdas.First(s => s.Number == number);

        Assert.Equal(surah, sajda.SurahNumber);
        Assert.Equal(ayah, sajda.AyahNumber);
        Assert.Equal(type, sajda.Type);
    }

    [Theory]
    [InlineData(7, 206)]
    [InlineData(13, 15)]
    [InlineData(16, 50)]
    [InlineData(17, 109)]
    [InlineData(19, 58)]
    [InlineData(22, 18)]
    [InlineData(22, 77)]
    [InlineData(25, 60)]
    [InlineData(27, 26)]
    [InlineData(32, 15)]
    [InlineData(38, 24)]
    [InlineData(41, 38)]
    [InlineData(53, 62)]
    [InlineData(84, 21)]
    [InlineData(96, 19)]
    public void IsSajdaAyah_ForAllPositions_Should_ReturnTrue(int surah, int ayah)
    {
        Assert.True(Quran.IsSajdaAyah(surah, ayah));
    }

    [Theory]
    [InlineData(7, 206)]
    [InlineData(13, 15)]
    [InlineData(16, 50)]
    [InlineData(17, 109)]
    [InlineData(19, 58)]
    [InlineData(22, 18)]
    [InlineData(22, 77)]
    [InlineData(25, 60)]
    [InlineData(27, 26)]
    [InlineData(32, 15)]
    [InlineData(38, 24)]
    [InlineData(41, 38)]
    [InlineData(53, 62)]
    [InlineData(84, 21)]
    [InlineData(96, 19)]
    public void GetSajda_ForAllPositions_Should_ReturnNonNull(int surah, int ayah)
    {
        var sajda = Quran.GetSajda(surah, ayah);
        Assert.NotNull(sajda);
        Assert.Equal(surah, sajda!.SurahNumber);
        Assert.Equal(ayah, sajda.AyahNumber);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 255)]
    [InlineData(36, 1)]
    [InlineData(114, 1)]
    public void IsSajdaAyah_ForNonSajdaVerse_Should_ReturnFalse(int surah, int ayah)
    {
        Assert.False(Quran.IsSajdaAyah(surah, ayah));
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 255)]
    [InlineData(36, 1)]
    public void GetSajda_ForNonSajdaVerse_Should_ReturnNull(int surah, int ayah)
    {
        Assert.Null(Quran.GetSajda(surah, ayah));
    }

    #endregion

    #region All 30 Juz Boundaries

    [Theory]
    [InlineData(1,  1,  1,   2,  141)]
    [InlineData(2,  2,  142, 2,  252)]
    [InlineData(3,  2,  253, 3,  92)]
    [InlineData(4,  3,  93,  4,  23)]
    [InlineData(5,  4,  24,  4,  147)]
    [InlineData(6,  4,  148, 5,  81)]
    [InlineData(7,  5,  82,  6,  110)]
    [InlineData(8,  6,  111, 7,  87)]
    [InlineData(9,  7,  88,  8,  40)]
    [InlineData(10, 8,  41,  9,  92)]
    [InlineData(11, 9,  93,  11, 5)]
    [InlineData(12, 11, 6,   12, 52)]
    [InlineData(13, 12, 53,  14, 52)]
    [InlineData(14, 15, 1,   16, 128)]
    [InlineData(15, 17, 1,   18, 74)]
    [InlineData(16, 18, 75,  20, 135)]
    [InlineData(17, 21, 1,   22, 78)]
    [InlineData(18, 23, 1,   25, 20)]
    [InlineData(19, 25, 21,  27, 55)]
    [InlineData(20, 27, 56,  29, 45)]
    [InlineData(21, 29, 46,  33, 30)]
    [InlineData(22, 33, 31,  36, 27)]
    [InlineData(23, 36, 28,  39, 31)]
    [InlineData(24, 39, 32,  41, 46)]
    [InlineData(25, 41, 47,  45, 37)]
    [InlineData(26, 46, 1,   51, 30)]
    [InlineData(27, 51, 31,  57, 29)]
    [InlineData(28, 58, 1,   66, 12)]
    [InlineData(29, 67, 1,   77, 50)]
    [InlineData(30, 78, 1,   114, 6)]
    public void Juz_Should_HaveCorrectBoundaries(int juzNumber, int startSurah, int startAyah, int endSurah, int endAyah)
    {
        var juz = Quran.GetJuz(juzNumber);
        Assert.Equal(startSurah, juz.StartSurah);
        Assert.Equal(startAyah, juz.StartAyah);
        Assert.Equal(endSurah, juz.EndSurah);
        Assert.Equal(endAyah, juz.EndAyah);
    }

    [Theory]
    [InlineData(1,  "الم")]
    [InlineData(2,  "سيقول")]
    [InlineData(3,  "تلك الرسل")]
    [InlineData(4,  "لن تنالوا")]
    [InlineData(5,  "والمحصنات")]
    [InlineData(6,  "لا يحب الله")]
    [InlineData(7,  "وإذا سمعوا")]
    [InlineData(8,  "ولو أننا")]
    [InlineData(9,  "قال الملأ")]
    [InlineData(10, "واعلموا")]
    [InlineData(11, "يعتذرون")]
    [InlineData(12, "وما من دابة")]
    [InlineData(13, "وما أبرئ")]
    [InlineData(14, "ربما")]
    [InlineData(15, "سبحان الذي")]
    [InlineData(16, "قال ألم")]
    [InlineData(17, "اقترب للناس")]
    [InlineData(18, "قد أفلح")]
    [InlineData(19, "وقال الذين")]
    [InlineData(20, "أمن خلق")]
    [InlineData(21, "اتل ما أوحي")]
    [InlineData(22, "ومن يقنت")]
    [InlineData(23, "وما لي")]
    [InlineData(24, "فمن أظلم")]
    [InlineData(25, "إليه يرد")]
    [InlineData(26, "حم")]
    [InlineData(27, "قال فما خطبكم")]
    [InlineData(28, "قد سمع")]
    [InlineData(29, "تبارك الذي")]
    [InlineData(30, "عم")]
    public void Juz_Should_HaveCorrectArabicName(int juzNumber, string expectedArabicName)
    {
        var juz = Quran.GetJuz(juzNumber);
        Assert.Equal(expectedArabicName, juz.ArabicName);
    }

    [Fact]
    public void AllJuz_GetJuzNumber_Should_MapCorrectly()
    {
        // For each juz, its start ayah should map to its number
        for (int j = 1; j <= 30; j++)
        {
            var juz = Quran.GetJuz(j);
            var mappedJuz = Quran.GetJuzNumber(juz.StartSurah, juz.StartAyah);
            Assert.Equal(j, mappedJuz);
        }
    }

    [Fact]
    public void GetSurahsByJuz_Juz1_Should_ContainExactSurahs()
    {
        var surahs = Quran.GetSurahsByJuz(1);
        // Juz 1: 1:1 to 2:141, so surahs 1 and 2
        Assert.Contains(surahs, s => s.Number == 1);
        Assert.Contains(surahs, s => s.Number == 2);
    }

    [Fact]
    public void GetSurahsByJuz_Juz30_Should_ContainManyShortSurahs()
    {
        var surahs = Quran.GetSurahsByJuz(30);
        // Juz 30: 78:1 to 114:6 - surahs 78 through 114 = 37 surahs
        Assert.Equal(37, surahs.Count);
        Assert.Contains(surahs, s => s.Number == 78);
        Assert.Contains(surahs, s => s.Number == 114);
    }

    #endregion
}
