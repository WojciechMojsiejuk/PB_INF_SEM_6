using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Cesar
{
    public class cesar
    {
        int N = 26;
        public char[] cesarCipher(string text,int k0,int k1)
        {
            char[] wynik = new char[text.Length];
            char c;
            int asciiHelper1;
            for (int i = 0; i < text.Length; i++)
            {
                asciiHelper1 = 65 + ((((int)text[i] - 65) * k1) + k0) % N;
                if (asciiHelper1 > 90) { asciiHelper1 -= 90;
                    c = (char)(asciiHelper1 + 64);
                }
               else c = (char)asciiHelper1;
                wynik[i] = c;
            }
            return wynik;
        }
        public char[] cesarDecipher(string text, int k0, int k1)
        {
            char[] wynik = text.ToCharArray();
            char[] wynik2 = new char[text.Length];
            int asciiHelper1;
            BigInteger asciiHelper2;
            int PHI = phi(N);
            BigInteger power = BigInteger.Pow(k1, PHI - 1);
            for (int i = 0; i < text.Length; i++)
            {
                asciiHelper1 = ((int)wynik[i] - 65 + (N - (k0%N) ) );
                asciiHelper2 = (asciiHelper1 * power % N);
                wynik2[i] = (char)(65 + (asciiHelper2));               
            }
            return wynik2;
        }
        //QPVCNJIHN
        public int gcd(int a, int b)
        {
            if (a == 0)
            {
                return b;

            }
            return gcd(b % a, a);
        }
        public int phi(int n)
        {
            int result = 1;
            for (int i = 2; i <= n; i++)
                if (gcd(i, n) == 1)
                {                 
                    result++;
                }            
            return result;

        }
    }
}
