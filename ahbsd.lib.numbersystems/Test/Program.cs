using System;
using System.Collections.Generic;
using ahbsd.lib.numbersystems;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int max = 71;
            string tmp;

            try
            {

                List<IBase> l = new List<IBase>(max);

                for (int i = 2; i < max + 2; i++)
                {
                    l.Add(new Base((uint)i));
                    Console.WriteLine(l[i - 2]);
                }

                // ulong v = 12387643555634563003;
                Console.WriteLine("Enter V (decimal):");
                uint v = uint.Parse(Console.ReadLine());
                // uint b = 1024;
                Console.WriteLine("Enter b (base):");
                uint b = uint.Parse(Console.ReadLine());

                Console.WriteLine(string.Format("Enter a value of base {0}:", Base.GetSignByNumber(b)));
                string c = Console.ReadLine();

                List<String> erg;


                erg = Base.Base10toBaseX(v, b, true);

                tmp = erg[0];
                WriteErg(erg);

                erg = Base.BaseXtoBase10(tmp, b, true);
                WriteErg(erg);

                erg = Base.BaseXtoBase10(c, b, true);
                WriteErg(erg);
                // */

                erg = Base.BaseXtoBaseY(c, v, b, true);
                WriteErg(erg);
            }
            catch (Exception ex)
            {
                Console.WriteLine("******************");
                Console.WriteLine(ex.Message);
                Console.WriteLine("******************");
            }
        }

        /// <summary>
        /// Writing erg to console.
        /// </summary>
        /// <param name="erg">A List of the result and the way of calculation.</param>
        private static void WriteErg(List<string> erg)
        {
            for (int i = 0; i < erg.Count; i++)
            {
                Console.WriteLine(erg[i]);
            }
        }
    }
}
