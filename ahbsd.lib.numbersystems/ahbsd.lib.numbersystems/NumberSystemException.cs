using System;
using System.Collections.Generic;
using System.Text;

namespace ahbsd.lib.numbersystems
{
    /// <summary>
    /// An Exception for tries with numeric systems, which didn't worked fine…
    /// </summary>
    public class OutOfRangeException : Exception, IEquatable<IOutOfRangeException>, IOutOfRangeException
    {
        /// <summary>
        /// The numeric system.
        /// </summary>
        private readonly uint system;
        /// <summary>
        /// The base that was tried.
        /// </summary>
        private readonly IBase tryBase;

        /// <summary>
        /// Constructor with a given numeric system.
        /// </summary>
        /// <param name="tb">The given numeric system.</param>
        public OutOfRangeException(IBase tb)
           : base(string.Format("The smallest possible system is 2; {0} is too small!!", tb.System))
        {
            tryBase = tb;
            system = tb.System;
        }

        /// <summary>
        /// Constructor with a given numeric system.
        /// </summary>
        /// <param name="s">The given numeric system.</param>
        public OutOfRangeException(uint s)
           : base(string.Format("The smallest possible system is 2; {0} is too small!!", s))
        {
            tryBase = null;
            system = s;
        }

        /// <summary>
        /// Constructor with a given system and a number that is out of the base of the given system.
        /// </summary>
        /// <param name="tb">The given system.</param>
        /// <param name="s">The number that is out of the base.</param>
        public OutOfRangeException(IBase tb, uint s)
           : base(string.Format("{0}; {1} ({3}) is out of base {2}.", tb, s, tb.System, Base.GetSignByNumber(s)))
        {
            tryBase = tb;
            system = tb.System;
        }

        /// <summary>
        /// Gets the numeric system.
        /// </summary>
        /// <value>The numeric system.</value>
        public uint System => system;
        /// <summary>
        /// Gets the base that was tried, but dint't worked.
        /// </summary>
        /// <value>The base that was tried.</value>
        public IBase TryBase => tryBase;

        /// <summary>
        /// Finds out, if another object equals this object.
        /// </summary>
        /// <param name="obj">Other object.</param>
        /// <returns><c>TRUE</c>, if the other object eaquals this object, otherwise <c>FALSE</c>.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as OutOfRangeException);
        }

        /// <summary>
        /// Finds out, if another object of <see cref="IOutOfRangeException"/> equals this object.
        /// </summary>
        /// <param name="other">Other object.</param>
        /// <returns><c>TRUE</c>, if the other object eaquals this object, otherwise <c>FALSE</c>.</returns>
        public bool Equals(IOutOfRangeException other)
        {
            return other != null &&
                   System == other.System &&
                   EqualityComparer<IBase>.Default.Equals(TryBase, other.TryBase);
        }

        /// <summary>
        /// Gets the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(System, TryBase);
        }

        /// <summary>
        /// Returns a string representing this object.
        /// </summary>
        /// <returns>A string representing this object.</returns>
        public override string ToString()
        {
            StringBuilder b = new StringBuilder(Message);
            string tmp = base.ToString();

            if (!string.IsNullOrEmpty(tmp))
            {
                b.AppendLine(tmp);
            }

            return b.ToString();
        }

        /// <summary>
        /// Compares two objects of <see cref="OutOfRangeException"/>, if they do eaquals.
        /// </summary>
        /// <param name="left">Object on the left side.</param>
        /// <param name="right">Object on the right side.</param>
        /// <returns><c>TRUE</c> if both objects eaquals, otherwise <c>FALSE</c>.</returns>
        public static bool operator ==(OutOfRangeException left, OutOfRangeException right)
        {
            return EqualityComparer<IOutOfRangeException>.Default.Equals(left, right);
        }

        /// <summary>
        /// Compares two objects of <see cref="OutOfRangeException"/>, if they do <c>not</c> eaquals.
        /// </summary>
        /// <param name="left">Object on the left side.</param>
        /// <param name="right">Object on the right side.</param>
        /// <returns><c>TRUE</c> if both objects don't eaquals, otherwise <c>FALSE</c>.</returns>
        public static bool operator !=(OutOfRangeException left, OutOfRangeException right)
        {
            return !(left == right);
        }
    }
}
