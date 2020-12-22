namespace ahbsd.lib.numbersystems
{
    /// <summary>
    /// Interface for <see cref="OutOfRangeException"/>.
    /// </summary>
    public interface IOutOfRangeException
    {
        /// <summary>
        /// Gets the numeric system.
        /// </summary>
        /// <value>The numeric system.</value>
        uint System { get; }
        /// <summary>
        /// Gets the base that was tried, but dint't worked.
        /// </summary>
        /// <value>The base that was tried.</value>
        IBase TryBase { get; }

        /// <summary>
        /// Finds out, if another object equals this object.
        /// </summary>
        /// <param name="obj">Other object.</param>
        /// <returns><c>TRUE</c>, if the other object eaquals this object, otherwise <c>FALSE</c>.</returns>
        bool Equals(object obj);
        /// <summary>
        /// Finds out, if another object of <see cref="IOutOfRangeException"/> equals this object.
        /// </summary>
        /// <param name="other">Other object.</param>
        /// <returns><c>TRUE</c>, if the other object eaquals this object, otherwise <c>FALSE</c>.</returns>
        bool Equals(IOutOfRangeException other);
        /// <summary>
        /// Gets the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        int GetHashCode();
        /// <summary>
        /// Returns a string representing this object.
        /// </summary>
        /// <returns>A string representing this object.</returns>
        string ToString();
    }
}