using System;
namespace ahbsd.lib.numbersystems
{
    /// <summary>
    /// An Exception for tries with numeric systems, which didn't worked fine…
    /// </summary>
    public class OutOfRangeException : Exception
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
    }
}
