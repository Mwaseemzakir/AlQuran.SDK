namespace AlQuran.SDK.Enums;

/// <summary>
/// Specifies the Arabic script type for Quran text.
/// </summary>
public enum ScriptType
{
    /// <summary>
    /// Simple Arabic script without diacritical marks (tashkeel/harakat).
    /// Easier to read for non-native speakers.
    /// </summary>
    Simple = 0,

    /// <summary>
    /// Uthmani script with full tashkeel (diacritical marks including fatha, kasra, damma, sukun, shadda, tanween, etc.).
    /// The traditional script used in printed Mushafs.
    /// </summary>
    Uthmani = 1
}
