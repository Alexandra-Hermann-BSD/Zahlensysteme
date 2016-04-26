using System;
using System.Text;
using System.Collections.Generic;

namespace AHBSD.Zahlensysteme
{
   public interface IBase
   {
      string Name { get; }
      uint System { get; }
      Char MaxSign { get; }
      Char GetSign(uint number);
      uint GetNumber(char sign);
   }
   
   public class ZahlensystemException : Exception
   {
      private uint system;
      private IBase tryBase;
      
      public ZahlensystemException(IBase tb)
         : base(String.Format("Das kleinstmögliche Zahlensystem ist 2; {0} ist zu klein!!", tb.System))
      {
         this.tryBase = tb;
         this.system = tb.System;
      }
      
      public ZahlensystemException(uint s)
         : base(String.Format("Das kleinstmögliche Zahlensystem ist 2; {0} ist zu klein!!", s))
      {
         this.tryBase = null;
         this.system = s;
      }
      
      public ZahlensystemException(IBase tb, uint s)
         : base(String.Format("{0} {1} ist außerhalb der Basis {2}.", tb, s, tb.System))
      {
         this.tryBase = tb;
         this.system = tb.System;
      }
      
   }
   
   public class Base : IBase
   {
      private string name;
      private uint system;
      private Char maxSign;
      protected const uint A_POS=65;
      protected const uint ZERO_POS=48;
      
      public Base()
      {
         this.system = 10;
         this.SetName();
         this.maxSign = this.GetSign(9);
      }
      
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
      
      protected void SetName()
      {
         this.name = "Basis " + this.system.ToString();
      }
      
#region IBase Members
      public string Name { get{ return this.name; } }
      public uint System { get { return this.system; } }
      public char MaxSign { get { return this.maxSign; } }
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
      
      public override string ToString()
      {
         StringBuilder result = new StringBuilder(this.name);
         result.Append("; MaxSign: ");
         result.Append(this.maxSign);
         
         return result.ToString();
      }
      
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
   
   public class MainClass {
      public static void Main() {
         int max=71;
         string tmp;
         
         try
         {
         
         /* List<IBase> l = new List<IBase>(max);
         
         for (int i=2; i < max + 2; i++)
         {
           l.Add(new Base((uint)i));
           Console.WriteLine(l[i-2]);
         } */
         
         // ulong v = 12387643555634563003;
         uint v = uint.Parse(Console.ReadLine());
         // uint b = 1024;
         uint b = uint.Parse(Console.ReadLine());
         
         string c = Console.ReadLine();
         
         List<String> erg;
         
         /*
          erg = Base.Base10toBaseX(v, b, true);
         
         tmp = erg[0];
         
         for (int i=0; i < erg.Count; i++)
         {
           Console.WriteLine(erg[i]);
         }
         
         erg = Base.BaseXtoBase10(tmp, b, true);
         
         for (int i=0; i < erg.Count; i++)
         {
           Console.WriteLine(erg[i]);
         }
         
         erg = Base.BaseXtoBase10(c, b, true);
         */
         
         erg = Base.BaseXtoBaseY(c, v, b, true);
         for (int i=0; i < erg.Count; i++)
         {
           Console.WriteLine(erg[i]);
         }
         }
         catch (Exception ex)
         {
            Console.WriteLine("******************");
            Console.WriteLine(ex.Message);
            Console.WriteLine("******************");
         }
         
      }
   }
}