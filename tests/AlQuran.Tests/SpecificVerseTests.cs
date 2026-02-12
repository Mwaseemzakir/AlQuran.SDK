using AlQuran.Enums;
using AlQuran.Extensions;

namespace AlQuran.Tests;

/// <summary>
/// Tests validating specific well-known verses and surahs of the Quran
/// to ensure data accuracy for commonly referenced content.
/// </summary>
public class SpecificVerseTests
{
    #region Al-Fatiha (Surah 1)

    [Fact]
    public void AlFatiha_Should_Have7Ayahs()
    {
        var ayahs = Quran.GetAyahs(1);
        Assert.Equal(7, ayahs.Count);
    }

    [Fact]
    public void AlFatiha_FirstAyah_Uthmani_Should_BeBismillah()
    {
        var ayah = Quran.GetAyah(1, 1, ScriptType.Uthmani);
        // Al-Fatiha's first ayah IS the Bismillah - verify by stripping diacritics
        var stripped = ayah.Text.RemoveTashkeel();
        Assert.StartsWith("\u0628\u0633\u0645", stripped); // Starts with بسم
        Assert.Contains("\u0671\u0644\u0644\u0647", stripped); // Contains ٱلله (alef wasla)
        Assert.Contains("\u0644\u0631\u062D\u0645\u0646", stripped); // لرحمن
        Assert.Contains("\u0644\u0631\u062D\u064A\u0645", stripped); // لرحيم
    }

    [Fact]
    public void AlFatiha_FirstAyah_Simple_Should_BeBismillah()
    {
        var ayah = Quran.GetAyah(1, 1, ScriptType.Simple);
        var stripped = ayah.Text.RemoveTashkeel();
        Assert.StartsWith("\u0628\u0633\u0645", stripped); // Starts with بسم
        Assert.Contains("\u0627\u0644\u0644\u0647", stripped); // Contains الله (regular alef)
        Assert.Contains("\u0644\u0631\u062D\u0645\u0646", stripped); // لرحمن
        Assert.Contains("\u0644\u0631\u062D\u064A\u0645", stripped); // لرحيم
    }

    [Fact]
    public void AlFatiha_Ayah2_Should_ContainAlhamdulillah()
    {
        var uthmani = Quran.GetAyah(1, 2, ScriptType.Uthmani);
        var simple = Quran.GetAyah(1, 2, ScriptType.Simple);
        // Use RemoveTashkeel to avoid combining-character ordering issues
        Assert.Contains("\u0671\u0644\u062D\u0645\u062F", uthmani.Text.RemoveTashkeel()); // ٱلحمد
        Assert.Contains("\u0627\u0644\u062D\u0645\u062F", simple.Text.RemoveTashkeel());  // الحمد
    }

    [Fact]
    public void AlFatiha_Ayah5_Should_ContainIyyakaNabudu()
    {
        var ayah = Quran.GetAyah(1, 5, ScriptType.Uthmani);
        var stripped = ayah.Text.RemoveTashkeel();
        Assert.Contains("\u0646\u0639\u0628\u062F", stripped); // نعبد
    }

    [Fact]
    public void AlFatiha_Properties_Should_BeCorrect()
    {
        var surah = Quran.GetSurah(1);
        Assert.Equal("الفاتحة", surah.ArabicName);
        Assert.Equal("Al-Fatiha", surah.EnglishName);
        Assert.Equal("The Opening", surah.EnglishMeaning);
        Assert.Equal(RevelationType.Meccan, surah.RevelationType);
        Assert.Equal(5, surah.RevelationOrder);
        Assert.Equal(1, surah.JuzStart);
        Assert.Equal(1, surah.JuzEnd);
        Assert.Equal(1, surah.PageStart);
        Assert.True(surah.HasBismillah);
    }

    #endregion

    #region Al-Baqarah (Surah 2) & Ayat al-Kursi

    [Fact]
    public void AlBaqarah_Should_Have286Ayahs()
    {
        var ayahs = Quran.GetAyahs(2);
        Assert.Equal(286, ayahs.Count);
    }

    [Fact]
    public void AyatAlKursi_2_255_Should_ContainAllah()
    {
        var ayah = Quran.GetAyah(2, 255, ScriptType.Uthmani);
        var stripped = ayah.Text.RemoveTashkeel();
        // Contains "لله" (base chars for Allah without article alef)
        Assert.Contains("\u0644\u0644\u0647", stripped);
    }

    [Fact]
    public void AyatAlKursi_2_255_Should_BeInJuz3()
    {
        var ayah = Quran.GetAyah(2, 255, ScriptType.Uthmani);
        Assert.Equal(3, ayah.Juz);
    }

    [Fact]
    public void AlBaqarah_FirstAyah_Should_ContainAlif()
    {
        var ayah = Quran.GetAyah(2, 1, ScriptType.Uthmani);
        // Alif-Lam-Mim opening - verify non-empty Arabic text
        Assert.False(string.IsNullOrWhiteSpace(ayah.Text));
        Assert.Contains(ayah.Text, c => c >= '\u0600' && c <= '\u06FF');
    }

    [Fact]
    public void AlBaqarah_LastAyah_286_Should_HaveText()
    {
        var ayah = Quran.GetAyah(2, 286);
        Assert.False(string.IsNullOrWhiteSpace(ayah.Text));
    }

    #endregion

    #region At-Tawbah (Surah 9)

    [Fact]
    public void AtTawbah_Should_NotHaveBismillah()
    {
        var surah = Quran.GetSurah(9);
        Assert.False(surah.HasBismillah);
        Assert.Equal("At-Tawbah", surah.EnglishName);
        Assert.Equal("التوبة", surah.ArabicName);
    }

    [Fact]
    public void AtTawbah_Should_Have129Ayahs()
    {
        Assert.Equal(129, Quran.GetAyahCount(9));
    }

    #endregion

    #region Ya-Sin (Surah 36)

    [Fact]
    public void YaSin_Should_Have83Ayahs()
    {
        Assert.Equal(83, Quran.GetAyahCount(36));
    }

    [Fact]
    public void YaSin_FirstAyah_Should_ContainYaSin()
    {
        var ayah = Quran.GetAyah(36, 1, ScriptType.Uthmani);
        Assert.False(string.IsNullOrWhiteSpace(ayah.Text));
        // The opening letters Ya-Sin
        var stripped = ayah.Text.RemoveTashkeel();
        Assert.Contains("\u064A\u0633", stripped); // يس
    }

    [Fact]
    public void YaSin_Should_BeMeccan()
    {
        Assert.Equal(RevelationType.Meccan, Quran.GetSurah(36).RevelationType);
    }

    #endregion

    #region Ar-Rahman (Surah 55)

    [Fact]
    public void ArRahman_Should_Have78Ayahs()
    {
        Assert.Equal(78, Quran.GetAyahCount(55));
    }

    [Fact]
    public void ArRahman_FirstAyah_Should_ContainArRahman()
    {
        var ayah = Quran.GetAyah(55, 1, ScriptType.Uthmani);
        var stripped = ayah.Text.RemoveTashkeel();
        // Contains "لرحمن" (base chars from Ar-Rahman)
        Assert.Contains("\u0644\u0631\u062D\u0645\u0646", stripped);
    }

    #endregion

    #region Al-Ikhlas (Surah 112)

    [Fact]
    public void AlIkhlas_Should_Have4Ayahs()
    {
        var ayahs = Quran.GetAyahs(SurahName.AlIkhlas);
        Assert.Equal(4, ayahs.Count);
    }

    [Fact]
    public void AlIkhlas_Ayah1_Should_ContainQulHuwAllahu()
    {
        var ayah = Quran.GetAyah(112, 1, ScriptType.Uthmani);
        var stripped = ayah.Text.RemoveTashkeel();
        Assert.Contains("\u0642\u0644", stripped);       // قل
        Assert.Contains("\u0644\u0644\u0647", stripped); // لله
    }

    [Fact]
    public void AlIkhlas_Should_BeMeccan()
    {
        Assert.Equal(RevelationType.Meccan, Quran.GetSurah(112).RevelationType);
    }

    #endregion

    #region Al-Falaq (Surah 113)

    [Fact]
    public void AlFalaq_Should_Have5Ayahs()
    {
        var ayahs = Quran.GetAyahs(SurahName.AlFalaq);
        Assert.Equal(5, ayahs.Count);
    }

    [Fact]
    public void AlFalaq_Ayah1_Should_ContainQulAuzu()
    {
        var ayah = Quran.GetAyah(113, 1, ScriptType.Uthmani);
        var stripped = ayah.Text.RemoveTashkeel();
        Assert.Contains("\u0642\u0644", stripped); // قل
    }

    #endregion

    #region An-Nas (Surah 114)

    [Fact]
    public void AnNas_Should_Have6Ayahs()
    {
        var ayahs = Quran.GetAyahs(SurahName.AnNas);
        Assert.Equal(6, ayahs.Count);
    }

    [Fact]
    public void AnNas_LastAyah_Should_BeLastAyahOfQuran()
    {
        var ayah = Quran.GetAyah(114, 6, ScriptType.Uthmani);
        Assert.False(string.IsNullOrWhiteSpace(ayah.Text));
        var stripped = ayah.Text.RemoveTashkeel();
        // Contains "لناس" (base chars for An-Nas)
        Assert.Contains("\u0644\u0646\u0627\u0633", stripped);
    }

    [Fact]
    public void AnNas_LastAyah_Simple_Should_ContainAnNas()
    {
        var ayah = Quran.GetAyah(114, 6, ScriptType.Simple);
        var stripped = ayah.Text.RemoveTashkeel();
        Assert.Contains("\u0644\u0646\u0627\u0633", stripped); // لناس
    }

    [Fact]
    public void AnNas_Ayah1_Should_ContainQulAuzu()
    {
        var ayah = Quran.GetAyah(114, 1, ScriptType.Uthmani);
        var stripped = ayah.Text.RemoveTashkeel();
        Assert.Contains("\u0642\u0644", stripped); // قل
    }

    #endregion

    #region Al-Kawthar (Surah 108 — Shortest)

    [Fact]
    public void AlKawthar_Should_BeShortestSurah_3Ayahs()
    {
        Assert.Equal(3, Quran.GetAyahCount(108));
    }

    [Fact]
    public void AlKawthar_Ayah1_Should_ContainInnaa()
    {
        var ayah = Quran.GetAyah(108, 1, ScriptType.Uthmani);
        // Use NormalizeForSearch to normalize alef variants (إ→ا, آ→ا)
        var normalized = ayah.Text.NormalizeForSearch();
        Assert.Contains("\u0627\u0646\u0627", normalized); // انا
    }

    #endregion

    #region Al-'Alaq (Surah 96 — First Revealed)

    [Fact]
    public void AlAlaq_Should_HaveRevelationOrder1()
    {
        var surah = Quran.GetSurah(96);
        Assert.Equal(1, surah.RevelationOrder);
        Assert.Equal("Al-'Alaq", surah.EnglishName);
    }

    [Fact]
    public void AlAlaq_Ayah1_Should_ContainIqra()
    {
        var ayah = Quran.GetAyah(96, 1, ScriptType.Uthmani);
        var stripped = ayah.Text.RemoveTashkeel();
        // Contains "قر" (base chars in Iqra)
        Assert.Contains("\u0642\u0631", stripped);
    }

    [Fact]
    public void AlAlaq_Should_Have19Ayahs()
    {
        Assert.Equal(19, Quran.GetAyahCount(96));
    }

    [Fact]
    public void AlAlaq_Ayah19_Should_BeSajda()
    {
        Assert.True(Quran.IsSajdaAyah(96, 19));
    }

    #endregion

    #region Well-Known Medinan Surahs

    [Fact]
    public void AlBaqarah_Should_BeMedinan()
    {
        Assert.Equal(RevelationType.Medinan, Quran.GetSurah(2).RevelationType);
    }

    [Fact]
    public void AnNisa_Should_BeMedinan()
    {
        Assert.Equal(RevelationType.Medinan, Quran.GetSurah(4).RevelationType);
    }

    [Fact]
    public void AlMaidah_Should_BeMedinan()
    {
        Assert.Equal(RevelationType.Medinan, Quran.GetSurah(5).RevelationType);
    }

    [Fact]
    public void AlAnfal_Should_BeMedinan()
    {
        Assert.Equal(RevelationType.Medinan, Quran.GetSurah(8).RevelationType);
    }

    #endregion

    #region Well-Known Meccan Surahs

    [Fact]
    public void AlAnam_Should_BeMeccan()
    {
        Assert.Equal(RevelationType.Meccan, Quran.GetSurah(6).RevelationType);
    }

    [Fact]
    public void AlKahf_Should_BeMeccan()
    {
        Assert.Equal(RevelationType.Meccan, Quran.GetSurah(18).RevelationType);
    }

    [Fact]
    public void AlMulk_Should_BeMeccan()
    {
        Assert.Equal(RevelationType.Meccan, Quran.GetSurah(67).RevelationType);
    }

    #endregion

    #region Surah Spanning Multiple Juz

    [Fact]
    public void AlBaqarah_Should_SpanMultipleJuz()
    {
        var surah = Quran.GetSurah(2);
        Assert.Equal(1, surah.JuzStart);
        Assert.Equal(3, surah.JuzEnd);
    }

    [Fact]
    public void AnNisa_Should_SpanMultipleJuz()
    {
        var surah = Quran.GetSurah(4);
        Assert.Equal(4, surah.JuzStart);
        Assert.Equal(6, surah.JuzEnd);
    }

    #endregion

    #region Al-Hajj Special Cases (Two Sajdas)

    [Fact]
    public void AlHajj_Should_HaveTwoSajdas()
    {
        var sajdas = Quran.GetAllSajdas().Where(s => s.SurahNumber == 22).ToList();
        Assert.Equal(2, sajdas.Count);
    }

    [Fact]
    public void AlHajj_Sajda1_ShouldBeAt18()
    {
        Assert.True(Quran.IsSajdaAyah(22, 18));
    }

    [Fact]
    public void AlHajj_Sajda2_ShouldBeAt77()
    {
        Assert.True(Quran.IsSajdaAyah(22, 77));
    }

    #endregion
}
