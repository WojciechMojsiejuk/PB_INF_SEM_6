using System;
using System.Collections.Generic;

//Izabela Siemaszko Zadanie 4
namespace Szyfr_Cezara
{
    class Program
    {
        
            static double CalculationFi(int n)
            {
                int pom = n,i=2, w=n/2;
                double fi=n;
                while(i<=w)
                {
                    if (n % i == 0)
                    {
                    double dzielenie = (double)1 / i;
                       fi *= (double)1-dzielenie;
                    }
                    i++;
                }
           
               // Console.WriteLine(fi);

                return fi;
            }
        
        static void Main(string[] args)
        {
            String text = "CEZAR";
            int n = 26;
            int k1 = 3;
            int k0 = 5;

            double fi = CalculationFi(26);
            fi--;
            Console.WriteLine(fi);


            int[] alphabet = new int[n];
            for (int i = 0; i < n; i++)
            {
                alphabet[i] = 65 + i;
            }

            Console.WriteLine("Szyfrowanie: ");

            int[] tab_cipher = new int[text.Length];
            for(int i = 0; i < text.Length; i++)
            {
                int a = (int)text[i];
                for(int j = 0; j < n; j++)
                {
                    if (a == alphabet[j])
                    {
                        int c = (j * k1 + k0) % n;
                        tab_cipher[i] = c;
                    }
                }
            }

            for(int i = 0; i < text.Length; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if (tab_cipher[i] == j)
                    {
                        Console.WriteLine((char)alphabet[j]);
                    }
                }
            }

            Console.WriteLine("deszyfrowanie");
            //k1=3 k0=5
            String text2 = "LRCFE";  

            double pow = Math.Pow(k1,fi);
            double[] tab_decrypt = new double[text2.Length];
            for (int i = 0; i < text2.Length; i++)
            {
                int a = (int)text2[i];
                for (int j = 0; j < n; j++)
                {
                    if (a == alphabet[j])
                    {
                        double c = ((j + n - k0) * pow )% n;
                       // Console.WriteLine(c);
                        tab_decrypt[i] = c;
                    }
                }
            }

            for (int i = 0; i < text2.Length; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (tab_decrypt[i] == j)
                    {
                        Console.WriteLine((char)alphabet[j]);
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
