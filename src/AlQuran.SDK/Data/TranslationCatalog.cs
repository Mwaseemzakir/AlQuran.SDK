using AlQuran.SDK.Enums;
using AlQuran.SDK.Models;

namespace AlQuran.SDK.Data;

/// <summary>
/// Contains metadata catalog for all embedded translation editions.
/// Maps each <see cref="TranslationEdition"/> to its API identifier, resource name, and display metadata.
/// </summary>
internal static class TranslationCatalog
{
    internal static readonly TranslationInfo[] AllTranslations =
    {
        new(TranslationEdition.EnglishSaheehInternational, "en.sahih",            "en_sahih",            "Saheeh International",            "Saheeh International",                    "English", "ltr"),
        new(TranslationEdition.EnglishYusufAli,            "en.yusufali",         "en_yusufali",         "Abdullah Yusuf Ali",              "Abdullah Yusuf Ali",                      "English", "ltr"),
        new(TranslationEdition.EnglishPickthall,           "en.pickthall",        "en_pickthall",        "Mohammed Marmaduke Pickthall",    "Mohammed Marmaduke William Pickthall",    "English", "ltr"),
        new(TranslationEdition.EnglishClearQuran,          "en.itani",            "en_itani",            "Clear Quran - Talal Itani",       "Talal Itani",                             "English", "ltr"),
        new(TranslationEdition.EnglishMaududi,             "en.maududi",          "en_maududi",          "Abul Ala Maududi",                "Abul Ala Maududi",                        "English", "ltr"),
        new(TranslationEdition.EnglishTransliteration,     "en.transliteration",  "en_transliteration",  "English Transliteration",         "English Transliteration",                 "English", "ltr", "transliteration"),
        new(TranslationEdition.UrduJalandhry,              "ur.jalandhry",        "ur_jalandhry",        "Fateh Muhammad Jalandhry",        "Fateh Muhammad Jalandhry",                "Urdu",    "rtl"),
        new(TranslationEdition.UrduJunagarhi,              "ur.junagarhi",        "ur_junagarhi",        "Muhammad Junagarhi",              "Muhammad Junagarhi",                      "Urdu",    "rtl"),
        new(TranslationEdition.UrduMaududi,                "ur.maududi",          "ur_maududi",          "Abul A'ala Maududi",              "Abul A'ala Maududi",                      "Urdu",    "rtl"),
    };

    private static readonly Dictionary<TranslationEdition, TranslationInfo> Lookup =
        AllTranslations.ToDictionary(t => t.Edition);

    /// <summary>
    /// Gets the translation info for a specific edition, or null if not found.
    /// </summary>
    internal static TranslationInfo? GetInfo(TranslationEdition edition)
    {
        Lookup.TryGetValue(edition, out var info);
        return info;
    }
}
