using System;
using System.Collections.Generic;
using System.Text;

namespace ahbsd.lib.numbersystems
{
    /// <summary>
    /// A class for number bases.
    /// </summary>
    public class Base : IBase, IEquatable<IBase>
    {
        /// <summary>
        /// The name of the base.
        /// </summary>
        private string name;
        /// <summary>
        /// The numeric system.
        /// </summary>
        private readonly uint system;
        /// <summary>
        /// The highest possible sign.
        /// </summary>
        private readonly char maxSign;
        /// <summary>
        /// The position where <c>A</c> is.
        /// </summary>
        protected const uint A_POS = 65;
        /// <summary>
        /// The position where <c>0</c> is.
        /// </summary>
        protected const uint ZERO_POS = 48;
        /// <summary>
        /// Constructor without parameters.
        /// </summary>
        /// <remarks>For the usual decimal system with base 10.</remarks>
        public Base()
        {
            system = 10;
            SetName();
            maxSign = GetSign(9);
        }

        /// <summary>
        /// Constructor with a defined system.
        /// </summary>
        /// <param name="System">The defined numeric system.</param>
        public Base(uint System)
        {
            system = System;
            SetName();
            maxSign = GetSign(System - 1);

            if (System < 2)
            {
                throw new OutOfRangeException(this);
            }
        }

        /// <summary>
        /// Constructor with a defined system and its name.
        /// </summary>
        /// <param name="System">The defined numeric system.</param>
        /// <param name="Name">The name of the defined numeric system.</param>
        public Base(uint System, string Name)
        {
            system = System;
            name = "Base " + Name;
            maxSign = GetSign(System - 1);

            if (System < 2)
            {
                throw new OutOfRangeException(this);
            }
        }

        /// <summary>
        /// Generates a base name, if the name wasn't set by the constructor.
        /// </summary>
        protected void SetName()
        {
            switch (system)
            {
                case 2:
                    name = "binary [Base 2]";
                    break;
                case 8:
                    name = "octal [Base 8]";
                    break;
                case 10:
                    name = "decimal [Base 10]";
                    break;
                case 16:
                    name = "hexadecimal [Base 16]";
                    break;
                default:
                    name = "Base " + system.ToString();
                    break;
            }
        }

        #region IBase Members
        /// <summary>
        /// Gets the name of the base.
        /// </summary>
        /// <value>The name of the base.</value>
        public string Name => name;
        /// <summary>
        /// Gets the numeric system.
        /// </summary>
        /// <value>The numeric system.</value>
        /// <example>For the decimal system: 10; for the binary system 2; for the hexadecimal system 16; etc.</example>
        public uint System => system;
        /// <summary>
        /// Gets the highest possible sign.
        /// </summary>
        /// <value>The highest possible sign.</value>
        /// <example>For the decimal system: 9; for the binary system: 1; for the hexadecimal system: F; etc.</example>
        /// <remarks>Always the <see cref="System"/> - 1…</remarks>
        public char MaxSign => maxSign;
        /// <summary>
        /// Gets the sign for a number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>The sign for the number.</returns>
        public char GetSign(uint number)
        {
            char result;

            if (number < system)
            {
                result = GetSignByNumber(number);
            }
            else
            {
                throw new OutOfRangeException(this, number);
            }

            return result;
        }

        /// <summary>
        /// Gets the number for a sign.
        /// </summary>
        /// <param name="sign">The sign.</param>
        /// <returns>The number for the sign.</returns>
        public uint GetNumber(char sign)
        {
            uint result = GetNumberBySign(sign);

            if (result >= system)
            {
                throw new OutOfRangeException(this, result);
            }

            return result;
        }

        /// <summary>
        /// Gets a short info about the current number system.
        /// </summary>
        /// <returns>A short info about the current number system.</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder(name);
            result.Append("; MaxSign: ");
            result.Append(maxSign);

            return result.ToString();
        }

        /// <summary>
        /// Method to find out if another object equals this object.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns><c>TRUE</c> if the other object equals this object, otherwise <c>FALSE</c>.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Base);
        }

        /// <summary>
        /// Method to find out if another object of the same interface <see cref="IBase"/> equals this object.
        /// </summary>
        /// <param name="other">The other object.</param>
        /// <returns><c>TRUE</c> if the other object equals this object, otherwise <c>FALSE</c>.</returns>
        public bool Equals(IBase other)
        {
            return other != null &&
                   Name == other.Name &&
                   System == other.System &&
                   MaxSign == other.MaxSign;
        }

        /// <summary>
        /// Gets the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(name, system, maxSign);
        }
        #endregion

        /// <summary>
        /// Static function to get a sign by number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>The sign.</returns>
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
        /// Static function to get a number by sign.
        /// </summary>
        /// <param name="sign">The sign.</param>
        /// <returns>The number.</returns>
        public static uint GetNumberBySign(char sign)
        {
            uint tmp;

            uint result;
            if (char.IsDigit(sign))
            {
                result = uint.Parse(sign.ToString());
            }
            else
            {
                tmp = sign;
                result = tmp - A_POS + 10;
            }

            return result;
        }


        /// <summary>
        /// Static function to calculate a value of base 10 to base X.
        /// </summary>
        /// <param name="valB10">The value of base 10.</param>
        /// <param name="X">The base to calculate to.</param>
        /// <returns>The value in base X</returns>
        public static string Base10toBaseX(ulong valB10, uint X) => Base10toBaseX(valB10, X, false)[0];

        /// <summary>
        /// Static function to calculate a value of base 10 to base X with the possibillity to get the way of calculation.
        /// </summary>
        /// <param name="valB10">The value of base 10.</param>
        /// <param name="X">The base to calculate to.</param>
        /// <param name="way">Get the way of calculation?</c></param>
        /// <returns>The value in base X plus the way of calculation.</returns>
        public static List<string> Base10toBaseX(ulong valB10, uint X, bool way)
        {
            List<string> result;
            string resultVal = string.Empty;
            IBase targetBase = new Base(X);
            ulong quotient = valB10;
            ulong tmpQuotient;
            List<uint> restList = new List<uint>();
            string[] fmt = new string[2];
            string tmpFmt;

            fmt[0] = "{0} : {1} = {2} rest {3}";
            fmt[1] = "{0} : {1} = {2} rest {3} [{4}]";

            if (!way)
            {
                result = new List<string>(1);
                result.Add(string.Empty);
            }
            else
            {
                result = new List<string>();
                result.Add(string.Empty);
                result.Add("Way of calculation:");
            }

            while (quotient > 0)
            {
                tmpQuotient = quotient;
                uint rest = (uint)(quotient % X);
                quotient /= X;
                restList.Add(rest);
                if (way)
                {
                    if (rest > 9)
                    {
                        tmpFmt = fmt[1];
                    }
                    else
                    {
                        tmpFmt = fmt[0];
                    }
                    result.Add(string.Format(tmpFmt, tmpQuotient, X, quotient, rest, targetBase.GetSign(rest)));
                }

                resultVal = targetBase.GetSign(rest).ToString() + resultVal;
            }

            if (way) result.Add(string.Format("The result of converting {0} of base 10 to base {1} is '{2}'", valB10, X, resultVal));
            result[0] = resultVal;

            return result;
        }

        /// <summary>
        /// Static function to calculate a value of base X to base 10.
        /// </summary>
        /// <param name="valBX">The value of base X.</param>
        /// <param name="X">The base X.</param>
        /// <returns>The value in base 10.</returns>
        public static ulong BaseXtoBase10(string valBX, uint X) => ulong.Parse(BaseXtoBase10(valBX, X, false)[0]);

        /// <summary>
        /// Static function to calculate a value of base X to base 10 with the possibillity to get the way of calculation.
        /// </summary>
        /// <param name="valBX">The value of base X.</param>
        /// <param name="X">The base X.</param>
        /// <param name="way">Get the way of calculation?</param>
        /// <returns>The value in base 10.</returns>
        public static List<string> BaseXtoBase10(string valBX, uint X, bool way)
        {
            List<string> result;
            IBase sourceSystem = new Base(X);

            int step = valBX.Length - 1;
            char[] valBXC = valBX.ToCharArray();
            char charW;
            uint intW = 0;
            uint tmp;
            int counter = 0;
            string[] fmt = new string[2];
            string tmpFmt;

            fmt[0] = "Value at position {0}: '{1}' = {2} int; {3}^{0} * {2} = {4} * {2} = {5}";
            fmt[1] = "Value at position {0}: {1}; {3}^{0} * {2} = {4} * {2} = {5}";

            if (!way)
            {
                result = new List<string>(1);
                result.Add(string.Empty);
            }
            else
            {
                result = new List<string>();
                result.Add(string.Empty);
                result.Add("Way of calculation:");
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

                if (way) result.Add(string.Format(tmpFmt, counter, charW, tmp, X, (uint)Math.Pow(X, counter), tmp * (uint)Math.Pow(X, counter)));
                intW += tmp * (uint)Math.Pow(X, counter);

                counter++;
                step--;
            }
            result[0] = intW.ToString();

            if (way) result.Add(string.Format("The result of converting '{0}' of base {1} to base 10 is {2}", valBX, X, result[0]));
            return result;
        }

        /// <summary>
        /// Static function to calculate a value of base X to base Y.
        /// </summary>
        /// <param name="valX">The value of base X.</param>
        /// <param name="X">The base X.</param>
        /// <param name="Y">The base Y.</param>
        /// <returns>The value in base Y.</returns>
        public static string BaseXtoBaseY(string valX, uint X, uint Y) => BaseXtoBaseY(valX, X, Y, false)[0];

        /// <summary>
        /// Static function to calculate a value of base X to base Y with the possibillity to get the way of calculation.
        /// </summary>
        /// <param name="valX">The value of base X.</param>
        /// <param name="X">The base X.</param>
        /// <param name="Y">The base Y.</param>
        /// <param name="way">Get the way of calculation?</param>
        /// <returns>The value in base Y.</returns>
        public static List<string> BaseXtoBaseY(string valX, uint X, uint Y, bool way)
        {
            List<string> result;
            uint tmp;
            int l;
            string tmpS;

            if (X != 10)
            {
                result = BaseXtoBase10(valX, X, way);
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
                if (Y != 10)
                {
                    result.AddRange(Base10toBaseX(tmp, Y, way));
                }
                else
                {
                    result.Add(tmp.ToString());
                }
            }
            else
            {
                if (Y != 10)
                {
                    result = Base10toBaseX(tmp, Y, way);
                }
                else
                {
                    result = new List<string>();
                    result.Add(tmp.ToString());
                }
            }
            tmpS = string.Format("The result of converting '{0}' from base {1} to base {3} is '{2}'", valX, X, result[l], Y);
            if (way) result.Add(tmpS);
            result.Add(result[l]);
            return result;
        }

        /// <summary>
        /// Operator to find out, if to objects of class <see cref="Base"/> equals.
        /// </summary>
        /// <param name="left">The object on the left side.</param>
        /// <param name="right">The object on the right side.</param>
        /// <returns><c>TRUE</c> if both objects do equal, otherwise <c>FALSE</c>.</returns>
        public static bool operator ==(Base left, Base right)
        {
            return EqualityComparer<Base>.Default.Equals(left, right);
        }

        /// <summary>
        /// Operator to find out, if to objects of class <see cref="Base"/> do not equal.
        /// </summary>
        /// <param name="left">The object on the left side.</param>
        /// <param name="right">The object on the right side.</param>
        /// <returns><c>TRUE</c> if both objects do not equal, otherwise <c>FALSE</c>.</returns>
        public static bool operator !=(Base left, Base right)
        {
            return !(left == right);
        }
    }
}
