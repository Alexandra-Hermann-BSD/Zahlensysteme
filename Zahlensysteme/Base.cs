using System;
using System.Text;
using System.Collections.Generic;


namespace Zahlensysteme
{
	/// <summary>
	/// Zahlenbasis; implementiert <see cref="Zahlensysteme.IBase"/>.
	/// </summary>
	public class Base : IBase
	{
		/// <summary>
		/// Der Name der Zahlenbasis.
		/// </summary>
		private string name;
		/// <summary>
		/// Die Zahlenbasis; die Anzahl der möglichen Zeichen.
		/// </summary>
		private uint system;
		/// <summary>
		/// Das höchstmögliche Zeichen.
		/// </summary>
		private Char maxSign;
		/// <summary>
		/// Die Position von 'A'.
		/// </summary>
		protected const uint A_POS=65;
		/// <summary>
		/// Die Position von '0'.
		/// </summary>
		protected const uint ZERO_POS=48;

		/// <summary>
		/// Konstruktor ohne Parameter.
		/// </summary>
		public Base()
		{
			this.system = 10;
			this.SetName();
			this.maxSign = this.GetSign(9);
		}

		/// <summary>
		/// Konstruktor mit Übergabe einer Basis-Zahl.
		/// </summary>
		/// <param name="System">Basis-Zahl.</param>
		public Base(uint System)
		{
			this.system = System;
			this.SetName();
			this.maxSign = this.GetSign(System-1);

			if (System < 2)
			{
				throw new ZahlensystemException(this);
			}
		}

		/// <summary>
		/// Konstruktor mit Übergabe einer Basis-Zahl und einem Namen der Zahlenbasis.
		/// </summary>
		/// <param name="System">Basis-Zahl.</param>
		/// <param name="Name">Name der Zahlenbasis.</param>
		public Base(uint System, string Name)
		{
			this.system = System;
			this.name = "Basis " + Name;
			this.maxSign = this.GetSign(System-1);

			if (System < 2)
			{
				throw new ZahlensystemException(this);
			}
		}

		/// <summary>
		/// Setzt den Namen diese Zahlenbasis.
		/// </summary>
		protected void SetName()
		{
			this.name = "Basis " + this.system.ToString();
		}

		#region IBase Members
		/// <summary>
		/// Der Name der Zahlenbasis.
		/// </summary>
		/// <value>Der Name der Zahlenbasis.</value>
		public string Name { get{ return this.name; } }

		/// <summary>
		/// Die Anzahl der möglichen Zeichen.
		/// </summary>
		/// <value>Das Zahlensystem.</value>
		public uint System { get { return this.system; } }

		/// <summary>
		/// Das höchstmögliche Zeichen.
		/// </summary>
		/// <value>Die maximale Anzahl an Zeichen.</value>
		public char MaxSign { get { return this.maxSign; } }

		/// <summary>
		/// Gibt das entsprechende Zeichen für die übergebene Zahl zurück.
		/// </summary>
		/// <returns>Das Zeichen für die übergebene Zahl.</returns>
		/// <param name="number">Die zu übersetzende Zahl.</param>
		public char GetSign(uint number)
		{
			char result;

			if (number < this.system)
			{
				result = GetSignByNumber(number);
			}
			else
			{
				throw new ZahlensystemException(this, number);
			}

			return result;
		}

		/// <summary>
		/// Gibt die Zahl des übergebenen Zeichens zurück.
		/// </summary>
		/// <returns>Die Zahl für das übergebene Zeichen.</returns>
		/// <param name="sign">Das zu übersetzende Zeichen.</param>
		public uint GetNumber(char sign)
		{
			uint result = GetNumberBySign(sign);

			if (result >= this.system)
			{
				throw new ZahlensystemException(this, result);
			}

			return result;
		}
		#endregion

		/// <summary>
		/// Gibt das entsprechende Zeichen für die übergebene Zahl zurück.
		/// </summary>
		/// <returns>Das Zeichen für die übergebene Zahl.</returns>
		/// <param name="number">Die zu übersetzende Zahl.</param>
		public static char GetSignByNumber(uint number)
		{
			char result = ' ';
			uint tmpI;

			if (number < 10)
			{
				tmpI = number + ZERO_POS;
			}
			else
			{
				tmpI = number - 10 + A_POS;
			}
			result = (char)tmpI;

			return result;
		}

		/// <summary>
		/// Gibt die Zahl des übergebenen Zeichens zurück.
		/// </summary>
		/// <returns>Die Zahl für das übergebene Zeichen.</returns>
		/// <param name="sign">Das zu übersetzende Zeichen.</param>
		public static uint GetNumberBySign(char sign)
		{
			uint result = 0;
			uint tmp;

			if (Char.IsDigit(sign))
			{
				result = uint.Parse(sign.ToString());
			}
			else
			{
				tmp = (uint)sign;
				result = tmp - A_POS + 10;
			}

			return result;
		}

		/// <summary>
		/// Gibt grundlegende Informationen über diese Zahlenbasis zurück.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="Zahlensysteme.Base"/>.</returns>
		public override string ToString()
		{
			StringBuilder result = new StringBuilder(this.name);
			result.Append("; MaxSign: ");
			result.Append(this.maxSign);

			return result.ToString();
		}

		/// <summary>
		/// Statische Methode zur Umwandlung einer Zahl der Basis 10 zu der Basis X.
		/// </summary>
		/// <returns>Eine <see cref="System.Collections.Generic.List<String>"/>, die an erster Stelle das Ergebnis enthält.</returns>
		/// <param name="valB10">Wert der Basis 10.</param>
		/// <param name="X">Ziel-Basis.</param>
		/// <param name="Rechenweg">Rechenweg mit <c>true</c> in die Ergebnis-Liste packen, oder nicht.</param>
		public static List<string> Base10toBaseX(ulong valB10, uint X, bool Rechenweg)
		{
			List<string> result;
			string resultVal = String.Empty;
			IBase targetBase = new Base(X);
			ulong quotient = valB10;
			ulong tmpQuotient;
			uint rest = 0;
			List<uint> restList = new List<uint>();
			string[] fmt = new string[2];
			string tmpFmt;

			fmt[0] = "{0} : {1} = {2} Rest {3}";
			fmt[1] = "{0} : {1} = {2} Rest {3} [{4}]";

			if (!Rechenweg)
			{
				result = new List<string>(1);
				result.Add(String.Empty);
			}
			else
			{
				result = new List<string>();
				result.Add(String.Empty);
				result.Add("Rechenweg:");
			}

			while (quotient > 0)
			{
				tmpQuotient = quotient;
				rest = (uint)(quotient % X);
				quotient = quotient / X;
				restList.Add(rest);
				if (Rechenweg)
				{
					if (rest > 9)
					{
						tmpFmt = fmt[1];
					}
					else
					{
						tmpFmt = fmt[0];
					}
					result.Add(String.Format(tmpFmt, tmpQuotient, X, quotient, rest, targetBase.GetSign((uint)rest)));
				}

				resultVal = targetBase.GetSign((uint)rest).ToString() + resultVal;
			}

			if (Rechenweg) result.Add(String.Format("Das Ergebnis der Umwandlung von {0} der Basis 10 in die Basis {1} ist '{2}'", valB10, X, resultVal));
			result[0] = resultVal;

			return result;
		}

		/// <summary>
		/// Statische Methode zur Umwandlung einer Zahl der Basis X zu der Basis 10.
		/// </summary>
		/// <returns>Eine <see cref="System.Collections.Generic.List<String>"/>, die an erster Stelle das Ergebnis enthält.</returns>
		/// <param name="valBX">Wert der Basis X.</param>
		/// <param name="X">Quell-Basis.</param>
		/// <param name="Rechenweg">Rechenweg mit <c>true</c> in die Ergebnis-Liste packen, oder nicht.</param>
		public static List<string> BaseXtoBase10(string valBX, uint X, bool Rechenweg)
		{
			List<string> result;
			IBase sourceSystem = new Base(X);

			int step = valBX.Length -1;
			char[] valBXC = valBX.ToCharArray();
			char charW;
			uint intW = 0;
			uint tmp;
			int zaehler = 0;
			string[] fmt = new string[2];
			string tmpFmt;

			fmt[0] = "Wert an Position {0}: '{1}' = {2} int; {3}^{0} * {2} = {4} * {2} = {5}";
			fmt[1] = "Wert an Position {0}: {1}; {3}^{0} * {2} = {4} * {2} = {5}";

			if (!Rechenweg)
			{
				result = new List<string>(1);
				result.Add(String.Empty);
			}
			else
			{
				result = new List<string>();
				result.Add(String.Empty);
				result.Add("Rechenweg:");
			}

			while (step >= 0)
			{
				charW = valBXC[step];

				tmp = sourceSystem.GetNumber(charW);

				if (Char.IsDigit(charW))
				{
					tmpFmt = fmt[1];
				}
				else
				{
					tmpFmt = fmt[0];
				}

				if (Rechenweg) result.Add(String.Format(tmpFmt, zaehler, charW, tmp, X, (uint)Math.Pow(X, zaehler), tmp * (uint)Math.Pow(X, zaehler)));
				intW += tmp * (uint)Math.Pow(X, zaehler);

				zaehler++;
				step--;
			}
			result[0] = intW.ToString();

			if (Rechenweg) result.Add(String.Format("Das Ergebnis der Umwandlung von '{0}' der Basis {1} in die Basis 10 ist {2}", valBX, X, result[0]));
			return result;
		}

		/// <summary>
		/// Statische Methode zur Umwandlung einer Zahl der Basis X zu der Basis Y.
		/// </summary>
		/// <returns>Eine <see cref="System.Collections.Generic.List<String>"/>, die an erster Stelle das Ergebnis enthält.</returns>
		/// <param name="valX">Wert der Basis X.</param>
		/// <param name="X">Quell-Basis.</param>
		/// <param name="Y">Ziel-Basis</param>
		/// <param name="Rechenweg">Rechenweg mit <c>true</c> in die Ergebnis-Liste packen, oder nicht.</param>
		public static List<string> BaseXtoBaseY(string valX, uint X, uint Y, bool Rechenweg)
		{
			List<string> result;
			uint tmp;
			int l;
			string tmpS;

			if (X!=10)
			{
				result = Base.BaseXtoBase10(valX, X, Rechenweg);
				tmp = uint.Parse(result[0]);
				l = result.Count;
			}
			else
			{
				tmp = uint.Parse(valX);
				l = 0;
				result = new List<string>();
			}

			if (l > 0)
			{
				if (Y!=10)
				{
					result.AddRange(Base.Base10toBaseX(tmp, Y, Rechenweg));
				}
				else
				{
					result.Add(tmp.ToString());
				}
			}
			else
			{
				if (Y!=10)
				{
					result = Base.Base10toBaseX(tmp, Y, Rechenweg);
				}
				else
				{
					result = new List<string>();
					result.Add(tmp.ToString());
				}
			}
			tmpS = String.Format("Das Ergebnis der Umwandlung von '{0}' der Basis {1} in die Basis {3} ist '{2}'", valX, X, result[l], Y);
			if (Rechenweg) result.Add(tmpS);
			result.Add(result[l]);
			return result;
		}
	}
}

