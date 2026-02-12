using AlQuran.SDK.Exceptions;

namespace AlQuran.SDK.Tests;

public class ExceptionTests
{
    #region SurahNotFoundException

    [Fact]
    public void SurahNotFoundException_DefaultConstructor_Should_HaveDefaultMessage()
    {
        var ex = new SurahNotFoundException();
        Assert.NotNull(ex.Message);
    }

    [Fact]
    public void SurahNotFoundException_WithMessage_Should_HaveCorrectMessage()
    {
        var ex = new SurahNotFoundException("test message");
        Assert.Equal("test message", ex.Message);
    }

    [Fact]
    public void SurahNotFoundException_WithInnerException_Should_HaveCorrectInner()
    {
        var inner = new InvalidOperationException("inner");
        var ex = new SurahNotFoundException("outer", inner);
        Assert.Same(inner, ex.InnerException);
    }

    #endregion

    #region AyahNotFoundException

    [Fact]
    public void AyahNotFoundException_DefaultConstructor_Should_HaveDefaultMessage()
    {
        var ex = new AyahNotFoundException();
        Assert.NotNull(ex.Message);
    }

    [Fact]
    public void AyahNotFoundException_WithMessage_Should_HaveCorrectMessage()
    {
        var ex = new AyahNotFoundException("test message");
        Assert.Equal("test message", ex.Message);
    }

    [Fact]
    public void AyahNotFoundException_WithInnerException_Should_HaveCorrectInner()
    {
        var inner = new InvalidOperationException("inner");
        var ex = new AyahNotFoundException("outer", inner);
        Assert.Same(inner, ex.InnerException);
    }

    #endregion

    #region JuzNotFoundException

    [Fact]
    public void JuzNotFoundException_DefaultConstructor_Should_HaveDefaultMessage()
    {
        var ex = new JuzNotFoundException();
        Assert.NotNull(ex.Message);
    }

    [Fact]
    public void JuzNotFoundException_WithMessage_Should_HaveCorrectMessage()
    {
        var ex = new JuzNotFoundException("test message");
        Assert.Equal("test message", ex.Message);
    }

    [Fact]
    public void JuzNotFoundException_WithInnerException_Should_HaveCorrectInner()
    {
        var inner = new InvalidOperationException("inner");
        var ex = new JuzNotFoundException("outer", inner);
        Assert.Same(inner, ex.InnerException);
    }

    #endregion
}
