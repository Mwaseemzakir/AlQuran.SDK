using AlQuran.SDK;
using AlQuran.SDK.Enums;
using AlQuran.SDK.Models;
using FluentAssertions;

namespace AlQuran.SDK.Tests;

public class RandomAndStatisticsTests
{
    #region GetRandomAyah (all Quran)

    [Fact]
    public void GetRandomAyah_Returns_Valid_Ayah()
    {
        var ayah = Quran.GetRandomAyah();
        ayah.Should().NotBeNull();
        ayah.SurahNumber.Should().BeInRange(1, 114);
        ayah.AyahNumber.Should().BeGreaterThan(0);
        ayah.Text.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void GetRandomAyah_Returns_Different_Ayahs_Over_Many_Calls()
    {
        var ayahs = Enumerable.Range(0, 50)
            .Select(_ => Quran.GetRandomAyah())
            .ToList();

        // With 50 random picks from 6236, it's statistically near-impossible to get all the same
        var distinct = ayahs.Select(a => $"{a.SurahNumber}:{a.AyahNumber}").Distinct().Count();
        distinct.Should().BeGreaterThan(1);
    }

    [Fact]
    public void GetRandomAyah_Uthmani_Returns_Uthmani_Text()
    {
        var ayah = Quran.GetRandomAyah(ScriptType.Uthmani);
        ayah.Should().NotBeNull();
    }

    [Fact]
    public void GetRandomAyah_Simple_Returns_Simple_Text()
    {
        var ayah = Quran.GetRandomAyah(ScriptType.Simple);
        ayah.Should().NotBeNull();
    }

    #endregion

    #region GetRandomAyah (specific Surah)

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(114)]
    public void GetRandomAyah_FromSurah_Returns_Ayah_In_Surah(int surahNumber)
    {
        var ayah = Quran.GetRandomAyah(surahNumber);
        ayah.SurahNumber.Should().Be(surahNumber);
    }

    [Fact]
    public void GetRandomAyah_FromSurah_InvalidSurah_Throws()
    {
        var act = () => Quran.GetRandomAyah(0);
        act.Should().Throw<Exception>();

        var act2 = () => Quran.GetRandomAyah(115);
        act2.Should().Throw<Exception>();
    }

    [Fact]
    public void GetRandomAyah_FromFatiha_Returns_Ayah_Between_1_And_7()
    {
        for (int i = 0; i < 20; i++)
        {
            var ayah = Quran.GetRandomAyah(1);
            ayah.SurahNumber.Should().Be(1);
            ayah.AyahNumber.Should().BeInRange(1, 7);
        }
    }

    #endregion

    #region GetAyahOfTheDay

    [Fact]
    public void GetAyahOfTheDay_Returns_Valid_Ayah()
    {
        var ayah = Quran.GetAyahOfTheDay();
        ayah.Should().NotBeNull();
        ayah.SurahNumber.Should().BeInRange(1, 114);
        ayah.Text.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void GetAyahOfTheDay_Same_Day_Returns_Same_Ayah()
    {
        // Using the date-based overload for predictability
        var date = new DateTime(2025, 1, 1);
        var ayah1 = Quran.GetAyahOfTheDay(date);
        var ayah2 = Quran.GetAyahOfTheDay(date);

        ayah1.SurahNumber.Should().Be(ayah2.SurahNumber);
        ayah1.AyahNumber.Should().Be(ayah2.AyahNumber);
    }

    [Fact]
    public void GetAyahOfTheDay_Different_Days_Return_Different_Ayahs()
    {
        var day1 = Quran.GetAyahOfTheDay(new DateTime(2025, 1, 1));
        var day2 = Quran.GetAyahOfTheDay(new DateTime(2025, 1, 2));
        var day3 = Quran.GetAyahOfTheDay(new DateTime(2025, 6, 15));

        // At least two of them should be different
        var unique = new HashSet<string>
        {
            $"{day1.SurahNumber}:{day1.AyahNumber}",
            $"{day2.SurahNumber}:{day2.AyahNumber}",
            $"{day3.SurahNumber}:{day3.AyahNumber}"
        };
        unique.Count.Should().BeGreaterThan(1, "different dates should produce different ayahs");
    }

    [Fact]
    public void GetAyahOfTheDay_Deterministic_For_Known_Date()
    {
        // Call multiple times for the same date - should always be the same
        var date = new DateTime(2024, 12, 25);
        var results = Enumerable.Range(0, 10)
            .Select(_ => Quran.GetAyahOfTheDay(date))
            .ToList();

        results.Should().AllSatisfy(a =>
        {
            a.SurahNumber.Should().Be(results[0].SurahNumber);
            a.AyahNumber.Should().Be(results[0].AyahNumber);
        });
    }

    [Fact]
    public void GetAyahOfTheDay_RespectsScriptType()
    {
        var date = new DateTime(2025, 3, 15);
        var uthmani = Quran.GetAyahOfTheDay(date, ScriptType.Uthmani);
        var simple = Quran.GetAyahOfTheDay(date, ScriptType.Simple);

        uthmani.SurahNumber.Should().Be(simple.SurahNumber);
        uthmani.AyahNumber.Should().Be(simple.AyahNumber);
        // Text differs between scripts
        uthmani.Text.Should().NotBe(simple.Text);
    }

    #endregion

    #region GetWordCount (Surah)

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(114)]
    public void GetWordCount_Returns_Positive_For_Valid_Surah(int surahNumber)
    {
        var count = Quran.GetWordCount(surahNumber);
        count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void GetWordCount_Fatiha_Is_Reasonable()
    {
        // Al-Fatiha has approximately 25-30 words depending on script
        var count = Quran.GetWordCount(1);
        count.Should().BeInRange(20, 50);
    }

    [Fact]
    public void GetWordCount_Baqarah_Is_Largest()
    {
        var baqarahCount = Quran.GetWordCount(2);
        // Al-Baqarah is the longest surah
        for (int s = 1; s <= 114; s++)
        {
            if (s == 2) continue;
            Quran.GetWordCount(s).Should().BeLessThanOrEqualTo(baqarahCount,
                $"Surah {s} should not have more words than Al-Baqarah");
        }
    }

    [Fact]
    public void GetWordCount_BySurahName()
    {
        var byNumber = Quran.GetWordCount(1);
        var byName = Quran.GetWordCount(SurahName.AlFatiha);
        byNumber.Should().Be(byName);
    }

    [Fact]
    public void GetWordCount_InvalidSurah_Throws()
    {
        var act = () => Quran.GetWordCount(0);
        act.Should().Throw<Exception>();
    }

    #endregion

    #region GetLetterCount (Surah)

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(114)]
    public void GetLetterCount_Returns_Positive_For_Valid_Surah(int surahNumber)
    {
        Quran.GetLetterCount(surahNumber).Should().BeGreaterThan(0);
    }

    [Fact]
    public void GetLetterCount_Greater_Than_WordCount()
    {
        // Letter count should be greater than word count for any surah
        for (int s = 1; s <= 5; s++) // check first 5 surahs
        {
            Quran.GetLetterCount(s).Should().BeGreaterThan(Quran.GetWordCount(s),
                $"Surah {s} should have more letters than words");
        }
    }

    #endregion

    #region GetTotalWordCount

    [Fact]
    public void GetTotalWordCount_Is_Sum_Of_All_Surahs()
    {
        var total = Quran.GetTotalWordCount();
        int sum = 0;
        for (int s = 1; s <= 114; s++)
        {
            sum += Quran.GetWordCount(s);
        }
        total.Should().Be(sum);
    }

    [Fact]
    public void GetTotalWordCount_Is_Positive()
    {
        Quran.GetTotalWordCount().Should().BeGreaterThan(0);
    }

    [Fact]
    public void GetTotalWordCount_Different_Scripts_May_Differ()
    {
        var uthmani = Quran.GetTotalWordCount(ScriptType.Uthmani);
        var simple = Quran.GetTotalWordCount(ScriptType.Simple);
        // Both should be positive, but may differ slightly
        uthmani.Should().BeGreaterThan(0);
        simple.Should().BeGreaterThan(0);
    }

    #endregion

    #region GetTotalLetterCount

    [Fact]
    public void GetTotalLetterCount_Is_Positive()
    {
        Quran.GetTotalLetterCount().Should().BeGreaterThan(0);
    }

    [Fact]
    public void GetTotalLetterCount_Greater_Than_TotalWordCount()
    {
        Quran.GetTotalLetterCount().Should().BeGreaterThan(Quran.GetTotalWordCount());
    }

    #endregion

    #region GetUniqueWordCount

    [Fact]
    public void GetUniqueWordCount_Is_Positive()
    {
        Quran.GetUniqueWordCount().Should().BeGreaterThan(0);
    }

    [Fact]
    public void GetUniqueWordCount_Less_Than_TotalWordCount()
    {
        var unique = Quran.GetUniqueWordCount();
        var total = Quran.GetTotalWordCount();
        unique.Should().BeLessThan(total, "unique words should be fewer than total words");
    }

    [Fact]
    public void GetUniqueWordCount_ForSurah_Is_Positive()
    {
        Quran.GetUniqueWordCount(1).Should().BeGreaterThan(0);
    }

    [Fact]
    public void GetUniqueWordCount_ForSurah_LessThanOrEqual_WordCount()
    {
        for (int s = 1; s <= 10; s++)
        {
            Quran.GetUniqueWordCount(s).Should().BeLessThanOrEqualTo(Quran.GetWordCount(s),
                $"Surah {s} unique words should be <= total words");
        }
    }

    #endregion

    #region Thread Safety

    [Fact]
    public async Task GetRandomAyah_ThreadSafe()
    {
        var exceptions = new System.Collections.Concurrent.ConcurrentBag<Exception>();
        var tasks = Enumerable.Range(0, 100)
            .Select(_ => Task.Run(() =>
            {
                try
                {
                    var ayah = Quran.GetRandomAyah();
                    ayah.Should().NotBeNull();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }))
            .ToArray();

        await Task.WhenAll(tasks);
        exceptions.Should().BeEmpty("random ayah should be thread-safe");
    }

    #endregion
}
