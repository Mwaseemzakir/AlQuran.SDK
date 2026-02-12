using AlQuran.Enums;
using AlQuran.Extensions;

namespace AlQuran.Tests;

/// <summary>
/// Tests comparing Simple and Uthmani script types to ensure they contain
/// genuinely different text while maintaining structural consistency.
/// </summary>
public class ScriptComparisonTests
{
    [Fact]
    public void SimpleAndUthmani_Should_HaveDifferentTextForMostAyahs()
    {
        int differentCount = 0;
        for (int s = 1; s <= 114; s++)
        {
            var uthmani = Quran.GetAyahs(s, ScriptType.Uthmani);
            var simple = Quran.GetAyahs(s, ScriptType.Simple);

            for (int a = 0; a < uthmani.Count; a++)
            {
                if (uthmani[a].Text != simple[a].Text)
                    differentCount++;
            }
        }

        // At least 90% of ayahs should be different (verified as 6120/6236)
        Assert.True(differentCount > 5500,
            $"Expected most ayahs to differ between scripts, but only {differentCount} differ");
    }

    [Fact]
    public void BothScripts_Should_HaveSameAyahCounts()
    {
        for (int s = 1; s <= 114; s++)
        {
            var uthmani = Quran.GetAyahs(s, ScriptType.Uthmani);
            var simple = Quran.GetAyahs(s, ScriptType.Simple);
            Assert.Equal(uthmani.Count, simple.Count);
        }
    }

    [Fact]
    public void BothScripts_Should_HaveSameJuzValues()
    {
        for (int s = 1; s <= 114; s++)
        {
            var uthmani = Quran.GetAyahs(s, ScriptType.Uthmani);
            var simple = Quran.GetAyahs(s, ScriptType.Simple);

            for (int a = 0; a < uthmani.Count; a++)
            {
                Assert.Equal(uthmani[a].Juz, simple[a].Juz);
            }
        }
    }

    [Fact]
    public void BothScripts_Should_HaveSamePageValues()
    {
        for (int s = 1; s <= 114; s++)
        {
            var uthmani = Quran.GetAyahs(s, ScriptType.Uthmani);
            var simple = Quran.GetAyahs(s, ScriptType.Simple);

            for (int a = 0; a < uthmani.Count; a++)
            {
                Assert.Equal(uthmani[a].Page, simple[a].Page);
            }
        }
    }

    [Fact]
    public void BothScripts_Should_HaveSameHizbQuarterValues()
    {
        for (int s = 1; s <= 114; s++)
        {
            var uthmani = Quran.GetAyahs(s, ScriptType.Uthmani);
            var simple = Quran.GetAyahs(s, ScriptType.Simple);

            for (int a = 0; a < uthmani.Count; a++)
            {
                Assert.Equal(uthmani[a].HizbQuarter, simple[a].HizbQuarter);
            }
        }
    }

    [Fact]
    public void BothScripts_Should_HaveSameAyahNumbers()
    {
        for (int s = 1; s <= 114; s++)
        {
            var uthmani = Quran.GetAyahs(s, ScriptType.Uthmani);
            var simple = Quran.GetAyahs(s, ScriptType.Simple);

            for (int a = 0; a < uthmani.Count; a++)
            {
                Assert.Equal(uthmani[a].AyahNumber, simple[a].AyahNumber);
                Assert.Equal(uthmani[a].SurahNumber, simple[a].SurahNumber);
            }
        }
    }

    [Fact]
    public void UthmaniText_AlFatiha1_Should_ContainAlefWasla()
    {
        var ayah = Quran.GetAyah(1, 1, ScriptType.Uthmani);
        // Uthmani uses ٱ (alef wasla U+0671)
        Assert.Contains("\u0671", ayah.Text);
    }

    [Fact]
    public void SimpleText_AlFatiha1_Should_ContainRegularAlef()
    {
        var ayah = Quran.GetAyah(1, 1, ScriptType.Simple);
        var stripped = ayah.Text.RemoveTashkeel();
        // Simple uses ال (regular alef-lam) not ٱل
        Assert.Contains("\u0627\u0644\u0644\u0647", stripped); // الله
    }

    [Fact]
    public void UthmaniText_AlFatiha1_Should_NotHaveRegularAlefLamForDefiniteArticle()
    {
        // In Uthmani script, "الله" uses alef wasla: "ٱلله"
        var ayah = Quran.GetAyah(1, 1, ScriptType.Uthmani);
        var stripped = ayah.Text.RemoveTashkeel();
        Assert.Contains("\u0671\u0644\u0644\u0647", stripped); // ٱلله
    }

    [Fact]
    public void SimpleText_AlFatiha1_Should_UseRegularAlefForDefiniteArticle()
    {
        var ayah = Quran.GetAyah(1, 1, ScriptType.Simple);
        var stripped = ayah.Text.RemoveTashkeel();
        Assert.Contains("\u0627\u0644\u0644\u0647", stripped); // الله with regular alef
    }

    [Fact]
    public void BismillahUthmani_And_BismillahSimple_Should_BeDifferent()
    {
        var uthmani = Quran.GetBismillah(ScriptType.Uthmani);
        var simple = Quran.GetBismillah(ScriptType.Simple);
        Assert.NotEqual(uthmani, simple);
    }

    [Fact]
    public void BismillahUthmani_Should_ContainAlefWasla()
    {
        var bismillah = Quran.GetBismillah(ScriptType.Uthmani);
        Assert.Contains("\u0671", bismillah);
    }

    [Fact]
    public void BismillahSimple_Should_NotContainAlefWasla()
    {
        var bismillah = Quran.GetBismillah(ScriptType.Simple);
        Assert.DoesNotContain("\u0671", bismillah);
    }

    [Fact]
    public void GetAyah_DefaultsToUthmani()
    {
        var defaultAyah = Quran.GetAyah(1, 1);
        var uthmani = Quran.GetAyah(1, 1, ScriptType.Uthmani);
        Assert.Equal(uthmani.Text, defaultAyah.Text);
    }

    [Fact]
    public void GetAyahs_DefaultsToUthmani()
    {
        var defaultAyahs = Quran.GetAyahs(1);
        var uthmani = Quran.GetAyahs(1, ScriptType.Uthmani);
        Assert.Equal(uthmani.Count, defaultAyahs.Count);
        for (int i = 0; i < uthmani.Count; i++)
        {
            Assert.Equal(uthmani[i].Text, defaultAyahs[i].Text);
        }
    }

    [Fact]
    public void IsTextDataAvailable_Should_ReturnTrueForBothScripts()
    {
        Assert.True(Quran.IsTextDataAvailable(ScriptType.Uthmani));
        Assert.True(Quran.IsTextDataAvailable(ScriptType.Simple));
    }

    [Fact]
    public void BothScripts_HasSajda_Should_BeConsistent()
    {
        foreach (var sajda in Quran.GetAllSajdas())
        {
            var uthmani = Quran.GetAyah(sajda.SurahNumber, sajda.AyahNumber, ScriptType.Uthmani);
            var simple = Quran.GetAyah(sajda.SurahNumber, sajda.AyahNumber, ScriptType.Simple);
            Assert.True(uthmani.HasSajda);
            Assert.True(simple.HasSajda);
        }
    }
}
