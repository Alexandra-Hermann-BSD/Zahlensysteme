using System;
using System.Collections.Generic;

namespace Zahlensysteme
{
    /// <summary>
    /// Exception für Zahlensysteme.
    /// </summary>
    public class ZahlensystemException : Exception, IEquatable<IZahlensystemException>, IZahlensystemException
    {
        /// <summary>
        /// Das Zahlensystem als uint. Gleichzusetzen mit tryBase.System.
        /// </summary>
        private readonly uint system;
        /// <summary>
        /// Die auszuprobierende Basis.
        /// </summary>
        private readonly IBase tryBase;

        /// <summary>
        /// Konstruktor mit Angabe einer Zahlenbasis. Siehe <see cref="ZahlensystemException"/>.
        /// </summary>
        /// <param name="tb">Test-Basis.</param>
        public ZahlensystemException (IBase tb)
            : base (string.Format ("Das kleinstmögliche Zahlensystem ist 2; {0} ist zu klein!!", tb.System))
        {
            tryBase = tb;
            system = tb.System;
        }

        /// <summary>
        /// Konstruktor mit Angabe einer Zahlenbasis. Siehe <see cref="ZahlensystemException"/>.
        /// </summary>
        /// <param name="s">Zahlenbasis als Zahl.</param>
        public ZahlensystemException (uint s)
            : base (string.Format ("Das kleinstmögliche Zahlensystem ist 2; {0} ist zu klein!!", s))
        {
            tryBase = null;
            system = s;
        }

        /// <summary>
        /// Konstruktor mit Übergabe der verwendeten Zahlenbasis, so wie einer falschen Zahl außerhalb der Zahlenbasis. 
        /// Siehe <see cref="ZahlensystemException"/>.
        /// </summary>
        /// <param name="tb">verwendeten Zahlenbasis.</param>
        /// <param name="s">falschen Zahl.</param>
        public ZahlensystemException (IBase tb, uint s)
            : base (string.Format ("{0} {1} ist außerhalb der Basis {2}.", tb, s, tb.System))
        {
            tryBase = tb;
            system = tb.System;
        }

        /// <summary>
        /// Gibt die auszuprobierende Basis zurück.
        /// </summary>
        /// <value>Die auszuprobierende Basis.</value>
        public IBase TryBase => tryBase;

        /// <summary>
        /// Gibt das Zahlensystem als <see cref="uint"/> zurück.
        /// </summary>
        /// <value>Das Zahlensystem als uint.</value>
        /// <remarks>Gleichzusetzen mit <see cref="TryBase.System"/>.</remarks>
        public uint System => system;

        public override bool Equals (object obj)
        {
            return Equals (obj as IZahlensystemException);
        }

        public bool Equals (IZahlensystemException other)
        {
            return other != null &&
                   EqualityComparer<IBase>.Default.Equals (TryBase, other.TryBase) &&
                   System == other.System;
        }

        public override int GetHashCode ()
        {
            unchecked {
                int hashCode = 1106227143;
                hashCode = hashCode * -1521134295 + EqualityComparer<IBase>.Default.GetHashCode (TryBase);
                hashCode = hashCode * -1521134295 + System.GetHashCode ();
                return hashCode;
            }
        }

        public static bool operator == (ZahlensystemException left, ZahlensystemException right)
        {
            return EqualityComparer<IZahlensystemException>.Default.Equals (left, right);
        }

        public static bool operator != (ZahlensystemException left, ZahlensystemException right)
        {
            return !(left == right);
        }
    }
}

