namespace ahbsd.lib.numbersystems
{
    /// <summary>
    /// An interface for number bases.
    /// </summary>
    public interface IBase : System.IEquatable<IBase>
    {
        /// <summary>
        /// Gets the name of the base.
        /// </summary>
        /// <value>The name of the base.</value>
        string Name { get; }
        /// <summary>
        /// Gets the numeric system.
        /// </summary>
        /// <value>The numeric system.</value>
        /// <example>For the decimal system: 10; for the binary system 2; for the hexadecimal system 16; etc.</example>
        uint System { get; }
        /// <summary>
        /// Gets the highest possible sign.
        /// </summary>
        /// <value>The highest possible sign.</value>
        /// <example>For the decimal system: 9; for the binary system: 1; for the hexadecimal system: F; etc.</example>
        /// <remarks>Always the <see cref="System"/> - 1…</remarks>
        char MaxSign { get; }
        /// <summary>
        /// Gets the sign for a number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>The sign for the number.</returns>
        char GetSign(uint number);
        /// <summary>
        /// Gets the number for a sign.
        /// </summary>
        /// <param name="sign">The sign.</param>
        /// <returns>The number for the sign.</returns>
        uint GetNumber(char sign);
        /// <summary>
        /// Gets a short info about the current number system.
        /// </summary>
        /// <returns>A short info about the current number system.</returns>
        string ToString();
        /// <summary>
        /// Method to find out if another object equals this object.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns><c>TRUE</c> if the other object equals this object, otherwise <c>FALSE</c>.</returns>
        bool Equals(object obj);
        /// <summary>
        /// Gets the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        int GetHashCode();
    }
}
