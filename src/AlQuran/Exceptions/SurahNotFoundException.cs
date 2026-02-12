namespace AlQuran.Exceptions;

/// <summary>
/// The exception thrown when a Surah with the specified identifier cannot be found.
/// </summary>
[Serializable]
public class SurahNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SurahNotFoundException"/> class.
    /// </summary>
    public SurahNotFoundException() : base("The specified Surah was not found.") { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SurahNotFoundException"/> class with a specified error message.
    /// </summary>
    public SurahNotFoundException(string? message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SurahNotFoundException"/> class with a specified error message
    /// and a reference to the inner exception.
    /// </summary>
    public SurahNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
}
