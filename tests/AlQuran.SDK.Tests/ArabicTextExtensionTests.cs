using AlQuran.SDK.Extensions;

namespace AlQuran.SDK.Tests;

public class ArabicTextExtensionTests
{
    [Fact]
    public void RemoveTashkeel_Should_RemoveDiacriticalMarks()
    {
        var withTashkeel = "بِسْمِ ٱللَّهِ ٱلرَّحْمَٰنِ ٱلرَّحِيمِ";
        var result = withTashkeel.RemoveTashkeel();

        Assert.DoesNotContain("\u0650", result); // Kasra
        Assert.DoesNotContain("\u064E", result); // Fatha
        Assert.DoesNotContain("\u064F", result); // Damma
        Assert.DoesNotContain("\u0652", result); // Sukun
        Assert.DoesNotContain("\u0651", result); // Shadda
    }

    [Fact]
    public void RemoveTashkeel_EmptyString_Should_ReturnEmpty()
    {
        Assert.Equal("", "".RemoveTashkeel());
    }

    [Fact]
    public void RemoveTashkeel_NullString_Should_ReturnNull()
    {
        string? input = null;
        Assert.Null(input!.RemoveTashkeel());
    }

    [Fact]
    public void RemoveTashkeelWithMapping_Should_ProduceCorrectMapping()
    {
        var with = "بِسْمِ";
        var result = with.RemoveTashkeelWithMapping(out var map);

        Assert.True(result.Length <= with.Length);
        Assert.True(map.Count > 0);
    }

    [Fact]
    public void NormalizeAlef_Should_NormalizeAlefForms()
    {
        // Alef with Hamza Above (أ) and Alef with Hamza Below (إ) should become plain Alef (ا)
        var input = "أحمد إبراهيم";
        var result = input.NormalizeAlef();

        Assert.DoesNotContain("\u0623", result); // Alef with Hamza Above
        Assert.DoesNotContain("\u0625", result); // Alef with Hamza Below
    }

    [Fact]
    public void NormalizeAlef_EmptyString_Should_ReturnEmpty()
    {
        Assert.Equal("", "".NormalizeAlef());
    }

    [Fact]
    public void NormalizeForSearch_Should_RemoveTashkeelAndNormalizeAlef()
    {
        var input = "بِسْمِ ٱللَّهِ ٱلرَّحْمَٰنِ ٱلرَّحِيمِ";
        var result = input.NormalizeForSearch();

        Assert.False(string.IsNullOrWhiteSpace(result));
        Assert.True(result.Length < input.Length);
    }

    [Fact]
    public void IsTashkeelChar_Should_IdentifyTashkeel()
    {
        Assert.True(ArabicTextExtensions.IsTashkeelChar('\u064E'));  // Fatha
        Assert.True(ArabicTextExtensions.IsTashkeelChar('\u0650'));  // Kasra
        Assert.True(ArabicTextExtensions.IsTashkeelChar('\u064F'));  // Damma
        Assert.True(ArabicTextExtensions.IsTashkeelChar('\u0651'));  // Shadda
        Assert.True(ArabicTextExtensions.IsTashkeelChar('\u0652'));  // Sukun
    }

    [Fact]
    public void IsTashkeelChar_Should_RejectNonTashkeel()
    {
        Assert.False(ArabicTextExtensions.IsTashkeelChar('ب'));  // Ba
        Assert.False(ArabicTextExtensions.IsTashkeelChar('ا'));  // Alef
        Assert.False(ArabicTextExtensions.IsTashkeelChar('A'));  // Latin
    }

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
    public void HighlightMatch_EmptyInput_Should_ReturnOriginal()
    {
        var result = "".HighlightMatch("test", s => $"<b>{s}</b>");
        Assert.Equal("", result);
    }
}
