using System;
using System.Collections.Generic;
using System.Linq;
using ahbsd.lib.numbersystems;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int max = 71;
            string tmp;
            bool way = true;

            Console.Clear();
            Console.WriteLine("Demonstration of ahbsd.lib.numbersystems");
            Console.WriteLine("========================================");
            Console.WriteLine("By default the way of calculation is");
            Console.WriteLine("displayed. If you want to turn that off,");
            Console.Write("start with {0} false\n", "Test");
            Console.WriteLine();

            if (args.Length >= 1)
            {
                if (args.Contains("true") || args.Contains("false"))
                {
                    if (args.Contains("true"))
                    {
                        way = true;
                    }
                    else if (args.Contains("false"))
                    {
                        way = false;
                    }
                }
            }

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

                Console.WriteLine(string.Format("Enter a value of base {0} ({1}):", Base.GetSignByNumber(b), b));
                string c = Console.ReadLine();


                if (way)
                {

                    List<string> erg;
                    erg = Base.Base10toBaseX(v, b, way);

                    tmp = erg[0];
                    WriteErg(erg);

                    erg = Base.BaseXtoBase10(tmp, b, way);
                    WriteErg(erg);

                    erg = Base.BaseXtoBase10(c, b, way);
                    WriteErg(erg);
                    // */

                    erg = Base.BaseXtoBaseY(c, v, b, way);
                    WriteErg(erg);
                }
                else
                {
                    string erg;
                    ulong erg2;
                    erg = Base.Base10toBaseX(v, b);
                    tmp = erg;
                    Console.Write("Conversion {0} (of base 10) to base {1}: {2}", v, b, erg);
                    Console.WriteLine();

                    erg2 = Base.BaseXtoBase10(tmp, b);
                    Console.Write("Conversion {0} (of base {1}) to base 10: {2}", tmp, b, erg2);
                    Console.WriteLine();

                    erg2 = Base.BaseXtoBase10(c, b);
                    Console.Write("Conversion {0} (of base {1}) to base 10: {2}", c, b, erg2);
                    Console.WriteLine();

                    erg = Base.BaseXtoBaseY(c, v, b);
                    Console.Write("Conversion {0} (of base {1}) to base {2}: {3}", c, v, b, erg);
                    Console.WriteLine();
                }

                
            }
            catch(OutOfRangeException ox)
            {
                Console.WriteLine("********** Out of range exception ************");
                Console.Write("TryBase: {0}", ox.TryBase.ToString());
                Console.WriteLine();
                Console.WriteLine("Message:");
                Console.WriteLine(ox.Message);
                Console.WriteLine("StackTrace:");
                Console.WriteLine(ox.StackTrace);
                Console.WriteLine("**********************************************");
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************** Exception ********************");
                Console.Write("Exception type: {0}", ex.GetType().FullName);
                Console.WriteLine();
                Console.WriteLine("Message:");
                Console.WriteLine(ex.Message);
                Console.WriteLine("StackTrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("**********************************************");
            }
        }

        /// <summary>
        /// Writing erg to console.
        /// </summary>
        /// <param name="erg">A List of the result and the way of calculation.</param>
        private static void WriteErg(List<string> erg)
        {
            foreach (string v in erg)
            {
                Console.WriteLine(v);
            }
        }
    }
}
