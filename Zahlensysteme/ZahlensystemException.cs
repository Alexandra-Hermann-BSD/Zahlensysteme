using System;

namespace Zahlensysteme
{
	/// <summary>
	/// Exception für Zahlensysteme.
	/// </summary>
	public class ZahlensystemException : Exception
	{
		/// <summary>
		/// Das Zahlensystem als uint. Gleichzusetzen mit tryBase.System.
		/// </summary>
		private uint system;
		/// <summary>
		/// Die auszuprobierende Basis.
		/// </summary>
		private IBase tryBase;

		/// <summary>
		/// Konstruktor mit Angabe einer Zahlenbasis. Siehe <see cref="Zahlensysteme.ZahlensystemException"/>.
		/// </summary>
		/// <param name="tb">Test-Basis.</param>
		public ZahlensystemException(IBase tb)
			: base(String.Format("Das kleinstmögliche Zahlensystem ist 2; {0} ist zu klein!!", tb.System))
		{
			this.tryBase = tb;
			this.system = tb.System;
		}

		/// <summary>
		/// Konstruktor mit Angabe einer Zahlenbasis. Siehe <see cref="Zahlensysteme.ZahlensystemException"/>.
		/// </summary>
		/// <param name="s">Zahlenbasis als Zahl.</param>
		public ZahlensystemException(uint s)
			: base(String.Format("Das kleinstmögliche Zahlensystem ist 2; {0} ist zu klein!!", s))
		{
			this.tryBase = null;
			this.system = s;
		}

		/// <summary>
		/// Konstruktor mit Übergabe der verwendeten Zahlenbasis, so wie einer falschen Zahl außerhalb der Zahlenbasis. 
		/// Siehe <see cref="Zahlensysteme.ZahlensystemException"/>.
		/// </summary>
		/// <param name="tb">verwendeten Zahlenbasis.</param>
		/// <param name="s">falschen Zahl.</param>
		public ZahlensystemException(IBase tb, uint s)
			: base(String.Format("{0} {1} ist außerhalb der Basis {2}.", tb, s, tb.System))
		{
			this.tryBase = tb;
			this.system = tb.System;
		}

	}
}

