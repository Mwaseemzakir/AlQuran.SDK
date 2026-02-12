using AlQuran.Enums;

namespace AlQuran.Tests;

/// <summary>
/// Comprehensive tests validating the integrity of all Quran data —
/// metadata, loaded text, Juz boundaries, Sajda positions, and cross-references.
/// </summary>
public class DataIntegrityTests
{
    #region Total Counts

    [Fact]
    public void TotalAyahsFromMetadata_Should_Be6236()
    {
        var surahs = Quran.GetAllSurahs();
        var total = surahs.Sum(s => s.AyahCount);
        Assert.Equal(6236, total);
    }

    [Fact]
    public void TotalLoadedAyahs_Uthmani_Should_Be6236()
    {
        int count = 0;
        for (int i = 1; i <= 114; i++)
            count += Quran.GetAyahs(i, ScriptType.Uthmani).Count;
        Assert.Equal(6236, count);
    }

    [Fact]
    public void TotalLoadedAyahs_Simple_Should_Be6236()
    {
        int count = 0;
        for (int i = 1; i <= 114; i++)
            count += Quran.GetAyahs(i, ScriptType.Simple).Count;
        Assert.Equal(6236, count);
    }

    [Fact]
    public void SurahCount_Should_Be114()
    {
        Assert.Equal(114, Quran.GetAllSurahs().Count);
    }

    [Fact]
    public void JuzCount_Should_Be30()
    {
        Assert.Equal(30, Quran.GetAllJuz().Count);
    }

    [Fact]
    public void SajdaCount_Should_Be15()
    {
        Assert.Equal(15, Quran.GetAllSajdas().Count);
    }

    [Fact]
    public void MeccanPlusMedinan_Should_Equal114()
    {
        var meccan = Quran.GetMeccanSurahs();
        var medinan = Quran.GetMedinanSurahs();
        Assert.Equal(114, meccan.Count + medinan.Count);
    }

    [Fact]
    public void MeccanSurahs_Should_Be86()
    {
        Assert.Equal(86, Quran.GetMeccanSurahs().Count);
    }

    [Fact]
    public void MedinanSurahs_Should_Be28()
    {
        Assert.Equal(28, Quran.GetMedinanSurahs().Count);
    }

    #endregion

    #region Surah–Ayah Count Cross-Reference

    [Fact]
    public void EverySurah_LoadedAyahCount_Should_MatchMetadata_Uthmani()
    {
        for (int i = 1; i <= 114; i++)
        {
            var surah = Quran.GetSurah(i);
            var ayahs = Quran.GetAyahs(i, ScriptType.Uthmani);
            Assert.Equal(surah.AyahCount, ayahs.Count);
        }
    }

    [Fact]
    public void EverySurah_LoadedAyahCount_Should_MatchMetadata_Simple()
    {
        for (int i = 1; i <= 114; i++)
        {
            var surah = Quran.GetSurah(i);
            var ayahs = Quran.GetAyahs(i, ScriptType.Simple);
            Assert.Equal(surah.AyahCount, ayahs.Count);
        }
    }

    /// <summary>
    /// Validates every single surah's ayah count against the well-known authoritative totals.
    /// </summary>
    [Theory]
    [InlineData(1, 7)]
    [InlineData(2, 286)]
    [InlineData(3, 200)]
    [InlineData(4, 176)]
    [InlineData(5, 120)]
    [InlineData(6, 165)]
    [InlineData(7, 206)]
    [InlineData(8, 75)]
    [InlineData(9, 129)]
    [InlineData(10, 109)]
    [InlineData(11, 123)]
    [InlineData(12, 111)]
    [InlineData(13, 43)]
    [InlineData(14, 52)]
    [InlineData(15, 99)]
    [InlineData(16, 128)]
    [InlineData(17, 111)]
    [InlineData(18, 110)]
    [InlineData(19, 98)]
    [InlineData(20, 135)]
    [InlineData(21, 112)]
    [InlineData(22, 78)]
    [InlineData(23, 118)]
    [InlineData(24, 64)]
    [InlineData(25, 77)]
    [InlineData(26, 227)]
    [InlineData(27, 93)]
    [InlineData(28, 88)]
    [InlineData(29, 69)]
    [InlineData(30, 60)]
    [InlineData(31, 34)]
    [InlineData(32, 30)]
    [InlineData(33, 73)]
    [InlineData(34, 54)]
    [InlineData(35, 45)]
    [InlineData(36, 83)]
    [InlineData(37, 182)]
    [InlineData(38, 88)]
    [InlineData(39, 75)]
    [InlineData(40, 85)]
    [InlineData(41, 54)]
    [InlineData(42, 53)]
    [InlineData(43, 89)]
    [InlineData(44, 59)]
    [InlineData(45, 37)]
    [InlineData(46, 35)]
    [InlineData(47, 38)]
    [InlineData(48, 29)]
    [InlineData(49, 18)]
    [InlineData(50, 45)]
    [InlineData(51, 60)]
    [InlineData(52, 49)]
    [InlineData(53, 62)]
    [InlineData(54, 55)]
    [InlineData(55, 78)]
    [InlineData(56, 96)]
    [InlineData(57, 29)]
    [InlineData(58, 22)]
    [InlineData(59, 24)]
    [InlineData(60, 13)]
    [InlineData(61, 14)]
    [InlineData(62, 11)]
    [InlineData(63, 11)]
    [InlineData(64, 18)]
    [InlineData(65, 12)]
    [InlineData(66, 12)]
    [InlineData(67, 30)]
    [InlineData(68, 52)]
    [InlineData(69, 52)]
    [InlineData(70, 44)]
    [InlineData(71, 28)]
    [InlineData(72, 28)]
    [InlineData(73, 20)]
    [InlineData(74, 56)]
    [InlineData(75, 40)]
    [InlineData(76, 31)]
    [InlineData(77, 50)]
    [InlineData(78, 40)]
    [InlineData(79, 46)]
    [InlineData(80, 42)]
    [InlineData(81, 29)]
    [InlineData(82, 19)]
    [InlineData(83, 36)]
    [InlineData(84, 25)]
    [InlineData(85, 22)]
    [InlineData(86, 17)]
    [InlineData(87, 19)]
    [InlineData(88, 26)]
    [InlineData(89, 30)]
    [InlineData(90, 20)]
    [InlineData(91, 15)]
    [InlineData(92, 21)]
    [InlineData(93, 11)]
    [InlineData(94, 8)]
    [InlineData(95, 8)]
    [InlineData(96, 19)]
    [InlineData(97, 5)]
    [InlineData(98, 8)]
    [InlineData(99, 8)]
    [InlineData(100, 11)]
    [InlineData(101, 11)]
    [InlineData(102, 8)]
    [InlineData(103, 3)]
    [InlineData(104, 9)]
    [InlineData(105, 5)]
    [InlineData(106, 4)]
    [InlineData(107, 7)]
    [InlineData(108, 3)]
    [InlineData(109, 6)]
    [InlineData(110, 3)]
    [InlineData(111, 5)]
    [InlineData(112, 4)]
    [InlineData(113, 5)]
    [InlineData(114, 6)]
    public void EverySurah_AyahCount_Should_MatchAuthoritativeCount(int surahNumber, int expectedAyahCount)
    {
        var surah = Quran.GetSurah(surahNumber);
        Assert.Equal(expectedAyahCount, surah.AyahCount);
    }

    #endregion

    #region Ayah Sequential Numbering

    [Fact]
    public void EverySurah_AyahNumbers_Should_BeSequentialFrom1_Uthmani()
    {
        for (int s = 1; s <= 114; s++)
        {
            var ayahs = Quran.GetAyahs(s, ScriptType.Uthmani);
            for (int a = 0; a < ayahs.Count; a++)
            {
                Assert.Equal(a + 1, ayahs[a].AyahNumber);
            }
        }
    }

    [Fact]
    public void EverySurah_AyahNumbers_Should_BeSequentialFrom1_Simple()
    {
        for (int s = 1; s <= 114; s++)
        {
            var ayahs = Quran.GetAyahs(s, ScriptType.Simple);
            for (int a = 0; a < ayahs.Count; a++)
            {
                Assert.Equal(a + 1, ayahs[a].AyahNumber);
            }
        }
    }

    [Fact]
    public void EverySurah_AllAyahs_Should_HaveCorrectSurahNumber_Uthmani()
    {
        for (int s = 1; s <= 114; s++)
        {
            var ayahs = Quran.GetAyahs(s, ScriptType.Uthmani);
            Assert.All(ayahs, a => Assert.Equal(s, a.SurahNumber));
        }
    }

    [Fact]
    public void EverySurah_AllAyahs_Should_HaveCorrectSurahNumber_Simple()
    {
        for (int s = 1; s <= 114; s++)
        {
            var ayahs = Quran.GetAyahs(s, ScriptType.Simple);
            Assert.All(ayahs, a => Assert.Equal(s, a.SurahNumber));
        }
    }

    #endregion

    #region Text Data

    [Fact]
    public void AllAyahs_Should_HaveNonEmptyText_Uthmani()
    {
        for (int s = 1; s <= 114; s++)
        {
            var ayahs = Quran.GetAyahs(s, ScriptType.Uthmani);
            Assert.All(ayahs, a =>
                Assert.False(string.IsNullOrWhiteSpace(a.Text),
                    $"Surah {s}, Ayah {a.AyahNumber} has empty text (Uthmani)"));
        }
    }

    [Fact]
    public void AllAyahs_Should_HaveNonEmptyText_Simple()
    {
        for (int s = 1; s <= 114; s++)
        {
            var ayahs = Quran.GetAyahs(s, ScriptType.Simple);
            Assert.All(ayahs, a =>
                Assert.False(string.IsNullOrWhiteSpace(a.Text),
                    $"Surah {s}, Ayah {a.AyahNumber} has empty text (Simple)"));
        }
    }

    [Fact]
    public void AllAyahs_Should_ContainArabicCharacters_Uthmani()
    {
        for (int s = 1; s <= 114; s++)
        {
            var ayahs = Quran.GetAyahs(s, ScriptType.Uthmani);
            Assert.All(ayahs, a =>
                Assert.True(a.Text.Any(c => c >= '\u0600' && c <= '\u06FF'),
                    $"Surah {s}, Ayah {a.AyahNumber} does not contain Arabic characters"));
        }
    }

    [Fact]
    public void AllAyahs_Should_ContainArabicCharacters_Simple()
    {
        for (int s = 1; s <= 114; s++)
        {
            var ayahs = Quran.GetAyahs(s, ScriptType.Simple);
            Assert.All(ayahs, a =>
                Assert.True(a.Text.Any(c => c >= '\u0600' && c <= '\u06FF'),
                    $"Surah {s}, Ayah {a.AyahNumber} does not contain Arabic characters"));
        }
    }

    #endregion

    #region Juz & Page & HizbQuarter Validity

    [Fact]
    public void AllAyahs_Should_HaveValidJuzNumber()
    {
        for (int s = 1; s <= 114; s++)
        {
            var ayahs = Quran.GetAyahs(s, ScriptType.Uthmani);
            Assert.All(ayahs, a =>
                Assert.InRange(a.Juz, 1, 30));
        }
    }

    [Fact]
    public void AllAyahs_Should_HaveValidPageNumber()
    {
        for (int s = 1; s <= 114; s++)
        {
            var ayahs = Quran.GetAyahs(s, ScriptType.Uthmani);
            Assert.All(ayahs, a => Assert.True(a.Page > 0,
                $"Surah {s}, Ayah {a.AyahNumber} has invalid page {a.Page}"));
        }
    }

    [Fact]
    public void AllAyahs_Should_HaveValidHizbQuarter()
    {
        for (int s = 1; s <= 114; s++)
        {
            var ayahs = Quran.GetAyahs(s, ScriptType.Uthmani);
            Assert.All(ayahs, a =>
                Assert.InRange(a.HizbQuarter, 1, 240));
        }
    }

    [Fact]
    public void PageNumbers_Should_BeNonDecreasingWithinSurah()
    {
        for (int s = 1; s <= 114; s++)
        {
            var ayahs = Quran.GetAyahs(s, ScriptType.Uthmani);
            for (int i = 1; i < ayahs.Count; i++)
            {
                Assert.True(ayahs[i].Page >= ayahs[i - 1].Page,
                    $"Surah {s}: Page decreases at ayah {ayahs[i].AyahNumber} ({ayahs[i].Page} < {ayahs[i - 1].Page})");
            }
        }
    }

    [Fact]
    public void JuzNumbers_Should_BeNonDecreasingGlobally()
    {
        int lastJuz = 0;
        for (int s = 1; s <= 114; s++)
        {
            var ayahs = Quran.GetAyahs(s, ScriptType.Uthmani);
            foreach (var ayah in ayahs)
            {
                Assert.True(ayah.Juz >= lastJuz,
                    $"Juz decreases at [{s}:{ayah.AyahNumber}]: {ayah.Juz} < {lastJuz}");
                lastJuz = ayah.Juz;
            }
        }
    }

    [Fact]
    public void FirstAyah_Should_BeInJuz1Page1()
    {
        var ayah = Quran.GetAyah(1, 1);
        Assert.Equal(1, ayah.Juz);
        Assert.Equal(1, ayah.Page);
    }

    [Fact]
    public void LastAyah_Should_BeInJuz30()
    {
        var ayah = Quran.GetAyah(114, 6);
        Assert.Equal(30, ayah.Juz);
    }

    #endregion

    #region Surah Uniqueness

    [Fact]
    public void AllSurahs_Should_HaveUniqueNumbers()
    {
        var surahs = Quran.GetAllSurahs();
        var numbers = surahs.Select(s => s.Number).ToHashSet();
        Assert.Equal(114, numbers.Count);
    }

    [Fact]
    public void AllSurahs_Should_HaveUniqueEnglishNames()
    {
        var surahs = Quran.GetAllSurahs();
        var names = surahs.Select(s => s.EnglishName).ToHashSet();
        Assert.Equal(114, names.Count);
    }

    [Fact]
    public void AllSurahs_Should_HaveUniqueArabicNames()
    {
        var surahs = Quran.GetAllSurahs();
        var names = surahs.Select(s => s.ArabicName).ToHashSet();
        Assert.Equal(114, names.Count);
    }

    [Fact]
    public void AllSurahs_Should_HaveUniqueRevelationOrders()
    {
        var surahs = Quran.GetAllSurahs();
        var orders = surahs.Select(s => s.RevelationOrder).ToHashSet();
        Assert.Equal(114, orders.Count);
    }

    [Fact]
    public void AllSurahs_RevelationOrders_Should_Cover1To114()
    {
        var surahs = Quran.GetAllSurahs();
        var orders = surahs.Select(s => s.RevelationOrder).OrderBy(o => o).ToList();
        for (int i = 0; i < 114; i++)
        {
            Assert.Equal(i + 1, orders[i]);
        }
    }

    #endregion

    #region Surah Metadata Validity

    [Fact]
    public void AllSurahs_Should_HaveNonEmptyArabicName()
    {
        Assert.All(Quran.GetAllSurahs(), s =>
            Assert.False(string.IsNullOrWhiteSpace(s.ArabicName)));
    }

    [Fact]
    public void AllSurahs_Should_HaveNonEmptyEnglishName()
    {
        Assert.All(Quran.GetAllSurahs(), s =>
            Assert.False(string.IsNullOrWhiteSpace(s.EnglishName)));
    }

    [Fact]
    public void AllSurahs_Should_HaveNonEmptyEnglishMeaning()
    {
        Assert.All(Quran.GetAllSurahs(), s =>
            Assert.False(string.IsNullOrWhiteSpace(s.EnglishMeaning)));
    }

    [Fact]
    public void AllSurahs_Should_HavePositiveAyahCount()
    {
        Assert.All(Quran.GetAllSurahs(), s =>
            Assert.True(s.AyahCount > 0, $"Surah {s.Number} has AyahCount {s.AyahCount}"));
    }

    [Fact]
    public void AllSurahs_Should_HavePositiveRukuCount()
    {
        Assert.All(Quran.GetAllSurahs(), s =>
            Assert.True(s.RukuCount > 0, $"Surah {s.Number} has RukuCount {s.RukuCount}"));
    }

    [Fact]
    public void AllSurahs_Should_HavePositivePageStart()
    {
        Assert.All(Quran.GetAllSurahs(), s =>
            Assert.True(s.PageStart > 0, $"Surah {s.Number} has PageStart {s.PageStart}"));
    }

    [Fact]
    public void AllSurahs_Should_HaveValidJuzRange()
    {
        Assert.All(Quran.GetAllSurahs(), s =>
        {
            Assert.InRange(s.JuzStart, 1, 30);
            Assert.InRange(s.JuzEnd, 1, 30);
            Assert.True(s.JuzEnd >= s.JuzStart,
                $"Surah {s.Number}: JuzEnd {s.JuzEnd} < JuzStart {s.JuzStart}");
        });
    }

    [Fact]
    public void AllSurahs_Should_HaveValidRevelationOrder()
    {
        Assert.All(Quran.GetAllSurahs(), s =>
            Assert.InRange(s.RevelationOrder, 1, 114));
    }

    [Fact]
    public void AllSurahs_PageStart_Should_BeNonDecreasing()
    {
        var surahs = Quran.GetAllSurahs();
        for (int i = 1; i < surahs.Count; i++)
        {
            Assert.True(surahs[i].PageStart >= surahs[i - 1].PageStart,
                $"PageStart decreases: Surah {surahs[i].Number} ({surahs[i].PageStart}) < Surah {surahs[i - 1].Number} ({surahs[i - 1].PageStart})");
        }
    }

    #endregion

    #region Juz Boundary Integrity

    [Fact]
    public void AllJuz_Should_HaveValidStartSurah()
    {
        Assert.All(Quran.GetAllJuz(), j =>
            Assert.InRange(j.StartSurah, 1, 114));
    }

    [Fact]
    public void AllJuz_Should_HaveValidEndSurah()
    {
        Assert.All(Quran.GetAllJuz(), j =>
            Assert.InRange(j.EndSurah, 1, 114));
    }

    [Fact]
    public void AllJuz_EndSurah_Should_BeGreaterOrEqualStartSurah()
    {
        Assert.All(Quran.GetAllJuz(), j =>
            Assert.True(j.EndSurah >= j.StartSurah,
                $"Juz {j.Number}: EndSurah {j.EndSurah} < StartSurah {j.StartSurah}"));
    }

    [Fact]
    public void JuzBoundaries_Should_BeContiguousWithNoGaps()
    {
        var juz = Quran.GetAllJuz();
        for (int i = 1; i < juz.Count; i++)
        {
            var prev = juz[i - 1];
            var curr = juz[i];

            // Current juz should start right after previous juz ends
            if (prev.EndSurah == curr.StartSurah)
            {
                Assert.Equal(prev.EndAyah + 1, curr.StartAyah);
            }
            else
            {
                // Previous juz ends at the last ayah of a surah, current starts at ayah 1 of next surah
                var prevEndSurah = Quran.GetSurah(prev.EndSurah);
                Assert.Equal(prevEndSurah.AyahCount, prev.EndAyah);
                Assert.Equal(prev.EndSurah + 1, curr.StartSurah);
                Assert.Equal(1, curr.StartAyah);
            }
        }
    }

    [Fact]
    public void Juz1_Should_StartAt1_1()
    {
        var juz = Quran.GetJuz(1);
        Assert.Equal(1, juz.StartSurah);
        Assert.Equal(1, juz.StartAyah);
    }

    [Fact]
    public void Juz30_Should_EndAt114_6()
    {
        var juz = Quran.GetJuz(30);
        Assert.Equal(114, juz.EndSurah);
        Assert.Equal(6, juz.EndAyah);
    }

    [Fact]
    public void AllJuz_Should_HaveNonEmptyArabicName()
    {
        Assert.All(Quran.GetAllJuz(), j =>
            Assert.False(string.IsNullOrWhiteSpace(j.ArabicName)));
    }

    #endregion

    #region Sajda Integrity

    [Fact]
    public void AllSajdas_Should_ExistWithinValidSurahs()
    {
        Assert.All(Quran.GetAllSajdas(), s =>
        {
            Assert.InRange(s.SurahNumber, 1, 114);
            var surah = Quran.GetSurah(s.SurahNumber);
            Assert.True(s.AyahNumber <= surah.AyahCount,
                $"Sajda {s.Number}: Ayah {s.AyahNumber} exceeds Surah {s.SurahNumber} count ({surah.AyahCount})");
        });
    }

    [Fact]
    public void AllSajdaAyahs_Should_HaveHasSajdaTrue_Uthmani()
    {
        foreach (var sajda in Quran.GetAllSajdas())
        {
            var ayah = Quran.GetAyah(sajda.SurahNumber, sajda.AyahNumber, ScriptType.Uthmani);
            Assert.True(ayah.HasSajda,
                $"Sajda [{sajda.SurahNumber}:{sajda.AyahNumber}] does not have HasSajda=true (Uthmani)");
        }
    }

    [Fact]
    public void AllSajdaAyahs_Should_HaveHasSajdaTrue_Simple()
    {
        foreach (var sajda in Quran.GetAllSajdas())
        {
            var ayah = Quran.GetAyah(sajda.SurahNumber, sajda.AyahNumber, ScriptType.Simple);
            Assert.True(ayah.HasSajda,
                $"Sajda [{sajda.SurahNumber}:{sajda.AyahNumber}] does not have HasSajda=true (Simple)");
        }
    }

    [Fact]
    public void AllSajdas_Should_HaveUniqueNumbers()
    {
        var sajdas = Quran.GetAllSajdas();
        var numbers = sajdas.Select(s => s.Number).ToHashSet();
        Assert.Equal(15, numbers.Count);
    }

    [Fact]
    public void AllSajdas_Numbers_Should_BeSequential1To15()
    {
        var sajdas = Quran.GetAllSajdas();
        for (int i = 0; i < sajdas.Count; i++)
        {
            Assert.Equal(i + 1, sajdas[i].Number);
        }
    }

    [Fact]
    public void NonSajdaAyahs_Should_HaveHasSajdaFalse()
    {
        // Al-Fatiha has no sajda
        var ayahs = Quran.GetAyahs(1, ScriptType.Uthmani);
        Assert.All(ayahs, a => Assert.False(a.HasSajda));
    }

    [Fact]
    public void ObligatorySajdas_Count_Should_BeCorrect()
    {
        var obligatory = Quran.GetObligatorySajdas();
        // As-Sajdah 32:15 is the only obligatory sajda in our data
        Assert.Single(obligatory);
    }

    [Fact]
    public void RecommendedSajdas_Count_Should_Be14()
    {
        var recommended = Quran.GetRecommendedSajdas();
        Assert.Equal(14, recommended.Count);
    }

    #endregion

    #region Bismillah Integrity

    [Fact]
    public void OnlyAtTawbah_Should_NotHaveBismillah()
    {
        var surahs = Quran.GetAllSurahs();
        var withoutBismillah = surahs.Where(s => !s.HasBismillah).ToList();
        Assert.Single(withoutBismillah);
        Assert.Equal(9, withoutBismillah[0].Number);
    }

    [Fact]
    public void AllSurahsExceptTawbah_Should_HaveBismillah()
    {
        var surahs = Quran.GetAllSurahs();
        var withBismillah = surahs.Where(s => s.HasBismillah).ToList();
        Assert.Equal(113, withBismillah.Count);
    }

    #endregion

    #region Known Facts

    [Fact]
    public void LongestSurah_Should_BeAlBaqarah_286Ayahs()
    {
        var surahs = Quran.GetAllSurahs();
        var longest = surahs.OrderByDescending(s => s.AyahCount).First();
        Assert.Equal(2, longest.Number);
        Assert.Equal(286, longest.AyahCount);
    }

    [Fact]
    public void ShortestSurahs_Should_Have3Ayahs()
    {
        var surahs = Quran.GetAllSurahs();
        var shortest = surahs.Where(s => s.AyahCount == 3).ToList();
        Assert.Equal(3, shortest.Count); // Al-Kawthar (108), Al-Asr (103), An-Nasr (110)
        Assert.Contains(shortest, s => s.Number == 103);
        Assert.Contains(shortest, s => s.Number == 108);
        Assert.Contains(shortest, s => s.Number == 110);
    }

    [Fact]
    public void AlAlaq_Should_BeFirstRevealed()
    {
        var surah = Quran.GetSurah(96);
        Assert.Equal(1, surah.RevelationOrder);
    }

    [Fact]
    public void AnNasr_Should_BeLastRevealed()
    {
        var surah = Quran.GetSurah(110);
        Assert.Equal(114, surah.RevelationOrder);
    }

    [Fact]
    public void AlFatiha_Should_Be5thRevealed()
    {
        var surah = Quran.GetSurah(1);
        Assert.Equal(5, surah.RevelationOrder);
    }

    [Fact]
    public void LastPageInQuran_Should_Be604()
    {
        var lastAyah = Quran.GetAyah(114, 6);
        Assert.Equal(604, lastAyah.Page);
    }

    #endregion
}
