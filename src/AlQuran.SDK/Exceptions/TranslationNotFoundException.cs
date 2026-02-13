namespace AlQuran.SDK.Exceptions;

/// <summary>
/// The exception thrown when a translation resource for the specified edition cannot be found or is unavailable.
/// </summary>
[Serializable]
public class TranslationNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TranslationNotFoundException"/> class.
    /// </summary>
    public TranslationNotFoundException() : base("The specified translation was not found or is unavailable.") { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TranslationNotFoundException"/> class with a specified error message.
    /// </summary>
    public TranslationNotFoundException(string? message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TranslationNotFoundException"/> class with a specified error message
    /// and a reference to the inner exception.
    /// </summary>
    public TranslationNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
}
