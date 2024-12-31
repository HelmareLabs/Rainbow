using System;

namespace HelmareLabs.Rainbow.State
{
    /// <summary>
    ///     An exception for state errors.
    /// </summary>
    public class RainbowStateException(string message, Exception? innerException)
        : Exception(message, innerException);
}
