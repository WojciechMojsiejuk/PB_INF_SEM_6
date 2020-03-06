using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BSKPS01_02
{
    class Zadanie4_IS
    {
        public static string Cypher(string text, string d, string k01, string k11)
        {
            text = text.ToUpper();
            
            int n = int.Parse(d);
            int k0 = int.Parse(k01);
            int k1 = int.Parse(k11);

            int[] alphabet = new int[n];
            for (int i = 0; i < n; i++)
            {
                alphabet[i] = 65 + i;
            }

            int[] tab_cipher = new int[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                int a = (int)text[i];
                for (int j = 0; j < n; j++)
                {
                    if (a == alphabet[j])
                    {
                        int c = (j * k1 + k0) % n;
                        tab_cipher[i] = c;
                    }
                }
            }

            string ctext = "";

            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (tab_cipher[i] == j)
                    {
                      
                        ctext += (char)alphabet[j];
                    }
                }
            }

            return ctext;
        }
        static double CalculationFi(int n)
        {
            int pom = n, i = 2, w = n / 2;
            double fi = n;
            while (i <= w)
            {
                if (n % i == 0)
                {
                    double dzielenie = (double)1 / i;
                    fi *= (double)1 - dzielenie;
                }
                i++;
            }

            return fi;
        }

        public static string Decypher(string text, string d, string k01, string k11)
        {
            text = text.ToUpper();

            int n = int.Parse(d);
            int k0 = int.Parse(k01);
            int k1 = int.Parse(k11);

            double fi = CalculationFi(26);


            fi--;//bo wzór do potęgi jest fi(n)-1
            
            BigInteger pow = BigInteger.Pow(k1, int.Parse(fi.ToString()));

            int[] alphabet = new int[n];
            for (int i = 0; i < n; i++)
            {
                alphabet[i] = 65 + i;
            }

          //  double pow = Math.Pow(k1, fi);
            BigInteger[] tab_decrypt = new BigInteger[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                int a = (int)text[i];
                for (int j = 0; j < n; j++)
                {
                    if (a == alphabet[j])
                    {
                        BigInteger c = ((j + (n - k0)) * pow) % n;
                        if (c < 0)
                        {
                            c += n;
                        }
                        tab_decrypt[i] = c;
                    }
                }
            }

            string dtext = "";

            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (tab_decrypt[i] == j)
                    {
                        dtext += (char)alphabet[j];
                    }
                }
            }

            return dtext;
        }


    }
}
