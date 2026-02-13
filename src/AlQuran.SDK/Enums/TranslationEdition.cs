namespace AlQuran.SDK.Enums;

/// <summary>
/// Available Quran translation editions embedded in the package.
/// Each edition corresponds to an offline-embedded translation resource.
/// </summary>
public enum TranslationEdition
{
    /// <summary>Saheeh International — Modern, clear English translation widely used worldwide.</summary>
    EnglishSaheehInternational = 1,

    /// <summary>Abdullah Yusuf Ali — Classic English translation with literary style.</summary>
    EnglishYusufAli = 2,

    /// <summary>Mohammed Marmaduke Pickthall — One of the earliest English translations by a Muslim scholar.</summary>
    EnglishPickthall = 3,

    /// <summary>Clear Quran by Talal Itani — Modern, easy-to-read English translation.</summary>
    EnglishClearQuran = 4,

    /// <summary>Abul Ala Maududi — English translation with scholarly commentary.</summary>
    EnglishMaududi = 5,

    /// <summary>English Transliteration — Latin-script pronunciation guide for the Arabic text.</summary>
    EnglishTransliteration = 10,

    /// <summary>Fateh Muhammad Jalandhry — Popular Urdu translation.</summary>
    UrduJalandhry = 101,

    /// <summary>Muhammad Junagarhi — Widely-read Urdu translation.</summary>
    UrduJunagarhi = 102,

    /// <summary>Abul A'ala Maududi — Tafheem-ul-Quran, comprehensive Urdu commentary/translation.</summary>
    UrduMaududi = 103,
}
