using AlQuran.SDK.Enums;

namespace AlQuran.SDK.Models;

/// <summary>
/// Contains metadata about a Quran translation edition.
/// </summary>
public sealed class TranslationInfo
{
    /// <summary>
    /// The translation edition enum identifier.
    /// </summary>
    public TranslationEdition Edition { get; }

    /// <summary>
    /// The display name of the translation (e.g., "Saheeh International").
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The author or organization name (e.g., "Abdullah Yusuf Ali").
    /// </summary>
    public string AuthorName { get; }

    /// <summary>
    /// The language of the translation (e.g., "English", "Urdu").
    /// </summary>
    public string Language { get; }

    /// <summary>
    /// The text direction ("ltr" for left-to-right, "rtl" for right-to-left).
    /// </summary>
    public string Direction { get; }

    /// <summary>
    /// The type of edition: "translation" or "transliteration".
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// The API identifier used by AlQuran Cloud API (e.g., "en.sahih").
    /// </summary>
    internal string ApiIdentifier { get; }

    /// <summary>
    /// The embedded resource file name (without extension).
    /// </summary>
    internal string ResourceName { get; }

    internal TranslationInfo(
        TranslationEdition edition,
        string apiIdentifier,
        string resourceName,
        string name,
        string authorName,
        string language,
        string direction,
        string type = "translation")
    {
        Edition = edition;
        ApiIdentifier = apiIdentifier;
        ResourceName = resourceName;
        Name = name;
        AuthorName = authorName;
        Language = language;
        Direction = direction;
        Type = type;
    }

    /// <inheritdoc />
    public override string ToString() => $"{Name} ({Language}) - {AuthorName}";
}
