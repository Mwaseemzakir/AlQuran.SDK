namespace AlQuran.SDK.Exceptions;

/// <summary>
/// The exception thrown when an Ayah with the specified identifier cannot be found.
/// </summary>
[Serializable]
public class AyahNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AyahNotFoundException"/> class.
    /// </summary>
    public AyahNotFoundException() : base("The specified Ayah was not found.") { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AyahNotFoundException"/> class with a specified error message.
    /// </summary>
    public AyahNotFoundException(string? message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AyahNotFoundException"/> class with a specified error message
    /// and a reference to the inner exception.
    /// </summary>
    public AyahNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
}
