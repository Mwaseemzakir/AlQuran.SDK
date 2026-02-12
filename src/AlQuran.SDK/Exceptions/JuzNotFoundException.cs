namespace AlQuran.SDK.Exceptions;

/// <summary>
/// The exception thrown when a Juz with the specified number cannot be found.
/// </summary>
[Serializable]
public class JuzNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JuzNotFoundException"/> class.
    /// </summary>
    public JuzNotFoundException() : base("The specified Juz was not found.") { }

    /// <summary>
    /// Initializes a new instance of the <see cref="JuzNotFoundException"/> class with a specified error message.
    /// </summary>
    public JuzNotFoundException(string? message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="JuzNotFoundException"/> class with a specified error message
    /// and a reference to the inner exception.
    /// </summary>
    public JuzNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
}
