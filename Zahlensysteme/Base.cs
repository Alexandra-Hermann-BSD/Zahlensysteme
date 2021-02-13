using System;
using System.Collections.Generic;


namespace Zahlensysteme
{
    /// <summary>
    /// Zahlenbasis; implementiert <see cref="IBase"/>.
    /// </summary>
    public class Base : IBase, IEquatable<IBase>
    {
		/// <summary>
		/// Der Name der Zahlenbasis.
		/// </summary>
		private string name;
		/// <summary>
		/// Die Zahlenbasis; die Anzahl der möglichen Zeichen.
		/// </summary>
		private readonly uint system;
		/// <summary>
		/// Das höchstmögliche Zeichen.
		/// </summary>
		private readonly char maxSign;
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
		/// <remarks>Als System wird das Dezimalsystem gewählt.</remarks>
		public Base()
		{
			system = 10;
			SetName();
			maxSign = GetSign(9);
		}

		/// <summary>
		/// Konstruktor mit Übergabe einer Basis-Zahl.
		/// </summary>
		/// <param name="System">Basis-Zahl.</param>
		public Base(uint System)
		{
			system = System;
			SetName();
			maxSign = this.GetSign(System-1);

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
			system = System;
			name = "Basis " + Name;
			maxSign = GetSign(System-1);

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
            string fmt = system switch {
                2 => "Binär (Basis {0})",
                3 => "Trinär (Basis {0})",
                8 => "Oktär (Basis {0})",
                10 => "Dezimal (Basis {0})",
                16 => "Hexadezimal (Basis {0})",
                _ => "Basis {0}",
            };
            name = string.Format(fmt, system);
		}

		#region IBase Members
		/// <summary>
		/// Der Name der Zahlenbasis.
		/// </summary>
		/// <value>Der Name der Zahlenbasis.</value>
		public string Name => name;

		/// <summary>
		/// Die Anzahl der möglichen Zeichen.
		/// </summary>
		/// <value>Das Zahlensystem.</value>
		public uint System => system;

		/// <summary>
		/// Das höchstmögliche Zeichen.
		/// </summary>
		/// <value>Die maximale Anzahl an Zeichen.</value>
		public char MaxSign => maxSign;

		/// <summary>
		/// Gibt das entsprechende Zeichen für die übergebene Zahl zurück.
		/// </summary>
		/// <returns>Das Zeichen für die übergebene Zahl.</returns>
		/// <param name="number">Die zu übersetzende Zahl.</param>
		public char GetSign(uint number)
		{
			char result;

			if (number < system)
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

			if (result >= system)
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
            uint tmpI;

			if (number < 10)
			{
				tmpI = number + ZERO_POS;
			}
			else
			{
				tmpI = number - 10 + A_POS;
			}
            char result = (char)tmpI;

            return result;
		}

		/// <summary>
		/// Gibt die Zahl des übergebenen Zeichens zurück.
		/// </summary>
		/// <returns>Die Zahl für das übergebene Zeichen.</returns>
		/// <param name="sign">Das zu übersetzende Zeichen.</param>
		public static uint GetNumberBySign(char sign)
		{
            uint tmp;

            uint result;
            if (char.IsDigit (sign)) {
                result = uint.Parse (sign.ToString ());
            } else {
                tmp = sign;
                result = tmp - A_POS + 10;
            }

            return result;
		}

        /// <summary>
        /// Gibt grundlegende Informationen über diese Zahlenbasis zurück.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current <see cref="Base"/>.</returns>
        public override string ToString()
		{
			return string.Format ("{0}; MaxSign: {1}", name, maxSign);
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
			string resultVal = string.Empty;
			IBase targetBase = new Base(X);
			ulong quotient = valB10;
			ulong tmpQuotient;
            List<uint> restList = new List<uint>();
			string[] fmt = new string[2];
			string tmpFmt;

			fmt[0] = "{0} : {1} = {2} Rest {3}";
			fmt[1] = "{0} : {1} = {2} Rest {3} [{4}]";

			if (!Rechenweg)
			{
                result = new List<string> (1) {
                    string.Empty
                };
            }
			else
			{
                result = new List<string> {
                    string.Empty,
                    "Rechenweg:"
                };
            }

			while (quotient > 0)
			{
				tmpQuotient = quotient;
                uint rest = (uint)(quotient % X);
                quotient /= X;
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
					result.Add(string.Format(tmpFmt, tmpQuotient, X, quotient, rest, targetBase.GetSign((uint)rest)));
				}

				resultVal = targetBase.GetSign(rest).ToString() + resultVal;
			}

			if (Rechenweg) result.Add(string.Format("Das Ergebnis der Umwandlung von {0} der Basis 10 in die Basis {1} ist '{2}'", valB10, X, resultVal));
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
                result = new List<string> (1) {
                    string.Empty
                };
            }
			else
			{
                result = new List<string> {
                    string.Empty,
                    "Rechenweg:"
                };
            }

			while (step >= 0)
			{
				charW = valBXC[step];

				tmp = sourceSystem.GetNumber(charW);

				if (char.IsDigit(charW))
				{
					tmpFmt = fmt[1];
				}
				else
				{
					tmpFmt = fmt[0];
				}

				if (Rechenweg) result.Add(string.Format(tmpFmt, zaehler, charW, tmp, X, (uint)Math.Pow(X, zaehler), tmp * (uint)Math.Pow(X, zaehler)));
				intW += tmp * (uint)Math.Pow(X, zaehler);

				zaehler++;
				step--;
			}
			result[0] = intW.ToString();

			if (Rechenweg) result.Add(string.Format("Das Ergebnis der Umwandlung von '{0}' der Basis {1} in die Basis 10 ist {2}", valBX, X, result[0]));
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
				result = BaseXtoBase10 (valX, X, Rechenweg);
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
					result.AddRange(Base10toBaseX (tmp, Y, Rechenweg));
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
					result = Base10toBaseX (tmp, Y, Rechenweg);
				}
				else
				{
                    result = new List<string> {
                        tmp.ToString ()
                    };
                }
			}
			tmpS = string.Format("Das Ergebnis der Umwandlung von '{0}' der Basis {1} in die Basis {3} ist '{2}'", valX, X, result[l], Y);
			if (Rechenweg) result.Add(tmpS);
			result.Add(result[l]);
			return result;
		}

        public override bool Equals (object obj)
        {
            return Equals (obj as Base);
        }

        public bool Equals (IBase other)
        {
            return other != null &&
                   Name == other.Name &&
                   System == other.System &&
                   MaxSign == other.MaxSign;
        }

        public override int GetHashCode ()
        {
            unchecked {
                int hashCode = 416850122;
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode (Name);
                hashCode = hashCode * -1521134295 + System.GetHashCode ();
                hashCode = hashCode * -1521134295 + MaxSign.GetHashCode ();
                return hashCode;
            }
        }

        public static bool operator == (Base left, Base right)
        {
            return EqualityComparer<IBase>.Default.Equals (left, right);
        }

        public static bool operator != (Base left, Base right)
        {
            return !(left == right);
        }
    }
}

