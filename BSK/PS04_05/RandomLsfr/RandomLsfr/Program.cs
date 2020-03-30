using System;
using System.Linq;

namespace RandomLsfr
{
    class Program
    {
        static string polynomial;
        public static bool ParseToBool(char c)
        {
            if (c == '0')
                return false;
            else if (c == '1')
                return true;
            else
                throw new ArgumentException("Invalid value");
        }

        public static int ParseToInt(bool b)
        {
            if (b == true)
                return 1;
            else
                return 0;
        }

        public static bool Xor(bool[] values)
        {
            if (values.Length == 1)
                return true;
            bool result = values[0] ^ values[1];
            for(int i=2; i < values.Length; i++)
            {
                result ^= values[i];
            }
            return result;
        }

        public static bool ValidatePylonomial(string p)
        {
            int oneValueCounter = 0;
            foreach (char c in p)
            {
                if (c != '0' && c != '1')
                    return false;
                if (c == '1')
                    oneValueCounter++;
            }
            if (p[p.Length - 1] != '1')
                return false;
            if (oneValueCounter < 2)
                return false;
            return true;
        }

        public static bool ValidateSeed(string s)
        {
            if (s.Length != polynomial.Length)
                return false;
            foreach (char c in s)
            {
                if (c != '0' && c != '1')
                    return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            //Read and validate pylonomial
            Console.Write("Type pylonomial (x^1, x^2, x^3, ...): ");
            polynomial = Console.ReadLine();
            if (ValidatePylonomial(polynomial) != true)
            {
                Console.WriteLine("Invalid polynomial");
                return;
            }
            //Read and validate seed
            Console.Write("Type seed: ");
            string seed = Console.ReadLine();
            if(ValidateSeed(seed) != true)
            {
                Console.WriteLine("Invalid seed");
                return;
            }

            Lsfr lsfr = new Lsfr(polynomial, seed);
            var initialValue = lsfr.GetValue();
            Console.Write("0: ");
            foreach (var b in initialValue)
                Console.Write(ParseToInt(b));
            Console.WriteLine();

            for (int i=0; i<Math.Pow(2, polynomial.Length);i++)
            {
                var result = lsfr.Clock();
                //Print result
                Console.Write("{0}: ", i+1);
                foreach (var b in result)
                    Console.Write(ParseToInt(b));
                Console.WriteLine();
            }
        }
    }
}
