using System;
namespace ahbsd.lib.numbersystems
{
    /// <summary>
    /// An EventArgs class that holds a number and the sign of the number.
    /// </summary>
    public class NumberAndSignEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the number.
        /// </summary>
        /// <value>The number;</value>
        public uint Number { get; private set; }
        /// <summary>
        /// Gets the sign.
        /// </summary>
        /// <value>The sign.</value>
        public char Sign { get; private set; }

        /// <summary>
        /// Constructor with the number and the sign.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="sign">The sign.</param>
        public NumberAndSignEventArgs(uint number, char sign)
            : base()
        {
            Number = number;
            Sign = sign;
        }
    }

    /// <summary>
    /// A delegate for <see cref="NumberAndSignEventArgs"/>.
    /// </summary>
    /// <param name="sender">The sending object.</param>
    /// <param name="e">The number and sign.</param>
    public delegate void NumberAndSignEventHandler(object sender, NumberAndSignEventArgs e);
}
