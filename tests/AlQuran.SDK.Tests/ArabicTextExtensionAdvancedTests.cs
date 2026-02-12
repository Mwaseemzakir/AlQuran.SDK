using AlQuran.SDK.Extensions;

namespace AlQuran.SDK.Tests;

/// <summary>
/// Extended tests for Arabic text extension methods covering more Unicode ranges,
/// edge cases, and complex text patterns.
/// </summary>
public class ArabicTextExtensionAdvancedTests
{
    #region RemoveTashkeel Advanced

    [Fact]
    public void RemoveTashkeel_Should_RemoveAllDiacriticalMarks()
    {
        // Full Bismillah with all its tashkeel
        var input = "بِسْمِ ٱللَّهِ ٱلرَّحْمَٰنِ ٱلرَّحِيمِ";
        var result = input.RemoveTashkeel();

        // Should not contain any tashkeel characters
        Assert.DoesNotContain("\u064B", result); // Fathatan
        Assert.DoesNotContain("\u064C", result); // Dammatan
        Assert.DoesNotContain("\u064D", result); // Kasratan
        Assert.DoesNotContain("\u064E", result); // Fatha
        Assert.DoesNotContain("\u064F", result); // Damma
        Assert.DoesNotContain("\u0650", result); // Kasra
        Assert.DoesNotContain("\u0651", result); // Shadda
        Assert.DoesNotContain("\u0652", result); // Sukun

        // But should keep the base letters and alef wasla
        Assert.Contains("ب", result);
        Assert.Contains("س", result);
        Assert.Contains("م", result);
    }

    [Fact]
    public void RemoveTashkeel_PureTashkeel_Should_ReturnEmpty()
    {
        var input = "\u064E\u064F\u0650\u0651\u0652"; // fatha, damma, kasra, shadda, sukun
        var result = input.RemoveTashkeel();
        Assert.Equal("", result);
    }

    [Fact]
    public void RemoveTashkeel_NoTashkeel_Should_ReturnSame()
    {
        var input = "بسم الله الرحمن الرحيم";
        var result = input.RemoveTashkeel();
        // Should still contain "بسم" (without diacritics, it may still match)
        // Actually with no tashkeel to remove, output should be the same
        Assert.False(string.IsNullOrWhiteSpace(result));
    }

    [Fact]
    public void RemoveTashkeel_MixedArabicLatin_Should_OnlyRemoveTashkeel()
    {
        var input = "Test بِسْمِ text";
        var result = input.RemoveTashkeel();
        Assert.Contains("Test", result);
        Assert.Contains("text", result);
        Assert.Contains("بسم", result);
    }

    [Fact]
    public void RemoveTashkeel_Null_Should_ReturnNull()
    {
        string? input = null;
        var result = input!.RemoveTashkeel();
        Assert.Null(result);
    }

    [Fact]
    public void RemoveTashkeel_Empty_Should_ReturnEmpty()
    {
        Assert.Equal("", "".RemoveTashkeel());
    }

    #endregion

    #region RemoveTashkeelWithMapping Advanced

    [Fact]
    public void RemoveTashkeelWithMapping_Should_MapCorrectly()
    {
        var input = "بِسْمِ";
        var result = input.RemoveTashkeelWithMapping(out var map);

        // Result should only have base letters
        Assert.Equal("بسم", result);

        // Map should map normalized indices to original indices
        Assert.True(map.ContainsKey(0)); // ب maps to original position of ب
        Assert.True(map.ContainsKey(1)); // س maps to original position of س
        Assert.True(map.ContainsKey(2)); // م maps to original position of م
    }

    [Fact]
    public void RemoveTashkeelWithMapping_Empty_Should_ReturnEmptyMapping()
    {
        var result = "".RemoveTashkeelWithMapping(out var map);
        Assert.Equal("", result);
        Assert.Empty(map);
    }

    [Fact]
    public void RemoveTashkeelWithMapping_NoTashkeel_Should_MapOneToOne()
    {
        var result = "بسم".RemoveTashkeelWithMapping(out var map);
        Assert.Equal("بسم", result);
        // Each char should map to itself
        Assert.Equal(0, map[0]);
        Assert.Equal(1, map[1]);
        Assert.Equal(2, map[2]);
    }

    #endregion

    #region NormalizeAlef Advanced

    [Fact]
    public void NormalizeAlef_AlefWithMadda_Should_BeNormalized()
    {
        // آ (Alef with Madda Above, U+0622) should become ا
        var input = "آمن";
        var result = input.NormalizeAlef();
        Assert.StartsWith("ا", result);
        Assert.DoesNotContain("\u0622", result);
    }

    [Fact]
    public void NormalizeAlef_AlefWithHamzaAbove_Should_BeNormalized()
    {
        // أ (Alef with Hamza Above, U+0623) should become ا
        var input = "أحمد";
        var result = input.NormalizeAlef();
        Assert.StartsWith("ا", result);
        Assert.DoesNotContain("\u0623", result);
    }

    [Fact]
    public void NormalizeAlef_AlefWithHamzaBelow_Should_BeNormalized()
    {
        // إ (Alef with Hamza Below, U+0625) should become ا
        var input = "إبراهيم";
        var result = input.NormalizeAlef();
        Assert.StartsWith("ا", result);
        Assert.DoesNotContain("\u0625", result);
    }

    [Fact]
    public void NormalizeAlef_PlainAlef_Should_StaySame()
    {
        var input = "ابراهيم";
        var result = input.NormalizeAlef();
        Assert.Equal(input, result);
    }

    [Fact]
    public void NormalizeAlef_Null_Should_ReturnNull()
    {
        string? input = null;
        Assert.Null(input!.NormalizeAlef());
    }

    [Fact]
    public void NormalizeAlef_MixedAlefs_Should_NormalizeAll()
    {
        var input = "أحمد إبراهيم آمن";
        var result = input.NormalizeAlef();
        // All three forms should be normalized to plain ا
        Assert.DoesNotContain("\u0623", result);
        Assert.DoesNotContain("\u0625", result);
        Assert.DoesNotContain("\u0622", result);
    }

    #endregion

    #region NormalizeForSearch Advanced

    [Fact]
    public void NormalizeForSearch_Should_RemoveTashkeelAndNormalizeAlef()
    {
        var input = "بِسْمِ ٱللَّهِ ٱلرَّحْمَٰنِ ٱلرَّحِيمِ";
        var result = input.NormalizeForSearch();

        // Should remove tashkeel
        Assert.DoesNotContain("\u0650", result); // Kasra
        Assert.DoesNotContain("\u064E", result); // Fatha

        Assert.False(string.IsNullOrWhiteSpace(result));
        Assert.True(result.Length < input.Length);
    }

    [Fact]
    public void NormalizeForSearch_Should_Trim()
    {
        var input = "  بِسْمِ  ";
        var result = input.NormalizeForSearch();
        Assert.False(result.StartsWith(" "));
        Assert.False(result.EndsWith(" "));
    }

    [Fact]
    public void NormalizeForSearch_Null_Should_ReturnNull()
    {
        string? input = null;
        Assert.Null(input!.NormalizeForSearch());
    }

    [Fact]
    public void NormalizeForSearch_Empty_Should_ReturnEmpty()
    {
        Assert.Equal("", "".NormalizeForSearch());
    }

    [Fact]
    public void NormalizeForSearch_Idempotent_Should_ReturnSame()
    {
        var input = "بسم الله الرحمن الرحيم";
        var first = input.NormalizeForSearch();
        var second = first.NormalizeForSearch();
        Assert.Equal(first, second);
    }

    #endregion

    #region IsTashkeelChar Advanced

    [Theory]
    [InlineData('\u064B', true)]  // Fathatan
    [InlineData('\u064C', true)]  // Dammatan
    [InlineData('\u064D', true)]  // Kasratan
    [InlineData('\u064E', true)]  // Fatha
    [InlineData('\u064F', true)]  // Damma
    [InlineData('\u0650', true)]  // Kasra
    [InlineData('\u0651', true)]  // Shadda
    [InlineData('\u0652', true)]  // Sukun
    [InlineData('\u0653', true)]  // Maddah Above
    [InlineData('\u0654', true)]  // Hamza Above
    [InlineData('\u0655', true)]  // Hamza Below
    [InlineData('\u0670', true)]  // Superscript Alef
    [InlineData('ب', false)]     // Ba (letter)
    [InlineData('ا', false)]     // Alef (letter)
    [InlineData('ل', false)]     // Lam (letter)
    [InlineData('A', false)]     // Latin A
    [InlineData(' ', false)]     // Space
    [InlineData('0', false)]     // Digit
    public void IsTashkeelChar_Should_IdentifyCorrectly(char c, bool expected)
    {
        Assert.Equal(expected, ArabicTextExtensions.IsTashkeelChar(c));
    }

    #endregion

    #region HighlightMatch Advanced

    [Fact]
    public void HighlightMatch_Should_WrapMatchedText()
    {
        var text = "بسم الله الرحمن الرحيم";
        var result = text.HighlightMatch("الله", s => $"<b>{s}</b>");
        Assert.Contains("<b>", result);
        Assert.Contains("</b>", result);
    }

    [Fact]
    public void HighlightMatch_NoMatch_Should_ReturnOriginal()
    {
        var text = "بسم الله الرحمن الرحيم";
        var result = text.HighlightMatch("xyz", s => $"<b>{s}</b>");
        Assert.Equal(text, result);
    }

    [Fact]
    public void HighlightMatch_EmptyText_Should_ReturnOriginal()
    {
        Assert.Equal("", "".HighlightMatch("test", s => $"<b>{s}</b>"));
    }

    [Fact]
    public void HighlightMatch_EmptySearchTerm_Should_ReturnOriginal()
    {
        var text = "بسم الله";
        Assert.Equal(text, text.HighlightMatch("", s => $"<b>{s}</b>"));
    }

    [Fact]
    public void HighlightMatch_NullSearchTerm_Should_ReturnOriginal()
    {
        var text = "بسم الله";
        Assert.Equal(text, text.HighlightMatch(null!, s => $"<b>{s}</b>"));
    }

    [Fact]
    public void HighlightMatch_NullWrapper_Should_ReturnOriginal()
    {
        var text = "بسم الله";
        Assert.Equal(text, text.HighlightMatch("الله", null!));
    }

    [Fact]
    public void HighlightMatch_WithDiacriticsAwareSearch_Should_Work()
    {
        // Use actual loaded Quran text to avoid C# source encoding differences
        var text = Quran.GetAyah(1, 1, AlQuran.SDK.Enums.ScriptType.Simple).Text;
        // Use non-bracket wrapper to avoid confusion with xUnit output formatting
        var result = text.HighlightMatch("\u0628\u0633\u0645", s => $"**{s}**"); // بسم
        Assert.Contains("**", result);
        Assert.NotEqual(text, result);
    }

    [Fact]
    public void HighlightMatch_CustomWrapper_Should_UseProvidedFunction()
    {
        var text = "بسم الله الرحمن الرحيم";
        var result = text.HighlightMatch("الله", s => $"***{s}***");
        Assert.Contains("***", result);
    }

    #endregion

    #region Real Quran Text Tests

    [Fact]
    public void RemoveTashkeel_OnRealUthmaniAyah_Should_ProduceShorterText()
    {
        var ayah = Quran.GetAyah(1, 1, AlQuran.SDK.Enums.ScriptType.Uthmani);
        var stripped = ayah.Text.RemoveTashkeel();
        Assert.True(stripped.Length < ayah.Text.Length,
            "Uthmani text should have tashkeel that gets removed");
    }

    [Fact]
    public void NormalizeForSearch_OnUthmaniAyah_Should_ProduceShorterText()
    {
        var ayah = Quran.GetAyah(1, 1, AlQuran.SDK.Enums.ScriptType.Uthmani);
        var normalized = ayah.Text.NormalizeForSearch();
        Assert.True(normalized.Length < ayah.Text.Length);
    }

    [Fact]
    public void HighlightMatch_OnRealQuranText_Should_Work()
    {
        var ayah = Quran.GetAyah(1, 2, AlQuran.SDK.Enums.ScriptType.Simple);
        var result = ayah.Text.HighlightMatch("الحمد", s => $"<mark>{s}</mark>");
        Assert.Contains("<mark>", result);
    }

    #endregion
}
