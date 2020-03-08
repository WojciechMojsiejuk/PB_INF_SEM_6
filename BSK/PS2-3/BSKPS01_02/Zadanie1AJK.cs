using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSKPS01_02
{
    class Zadanie1AJK
    {
        public static string Cypher(string napis, string key)
        {
            int dlugosc = napis.Length;
            int n = Int32.Parse(key);
            int[,] tablica = new int[dlugosc, n];
            int pom = 0;
            int i = 0;
            int j = 0;
            string wynik = "";
            int count = (dlugosc / n) + 1;
            if (n == 1)
            {
                wynik = napis;
                return napis;
            }
            while (i < napis.Length - 1)
            {
                while (pom != count)
                {
                    while (j != n - 1 && i != napis.Length)
                    {
                        //Console.WriteLine(napis[i]);
                        tablica[i, j] = (int)napis[i];
                        j++;
                        i++;
                    }

                    while (j != 0 && i != napis.Length)
                    {
                        tablica[i, j] = (int)napis[i];

                        j--;
                        i++;
                    }
                    pom++;
                }
            }

            for (int m = 0; m < n; m++)
            {
                for (int l = 0; l < dlugosc; l++)
                {
                    if (tablica[l, m] != 0)
                    {
                        //Console.WriteLine((char)tablica[l,m]) ;
                        wynik = wynik + (char)tablica[l, m];
                    }

                }
            }
            return wynik;

        }

        public static string Decypher(string napis, string key)
        {
            int i, j, pom;
            int n = Int32.Parse(key);
            int dlugosc = napis.Length;
            char[,] tablica = new char[n, dlugosc];

            int ilosc = dlugosc / n;
            pom = 0;
            j = 0;
            string wynik = "";
            if (n == 1)
            {
                wynik = napis;
                return napis;
            }

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < dlugosc; j++)
                {
                    tablica[i, j] = '0';
                }
            }

            j = 0;
            while (j <= dlugosc - 1)
            {
                if (j % (2 * (n - 1)) == 0)

                {
                    tablica[0, j] = napis[pom];
                    //Console.WriteLine(tablica[0, j]);
                    pom++;
                }
                j++;
            }

            for (j = 1; j <= n - 1; j++)
            {
                for (i = 0; i < dlugosc + 2 * (n - 1);)
                {
                    if (pom < dlugosc)
                    {
                        if (i - j > 0 && i - j < dlugosc)
                        {
                            if (j != n - 1)
                            {
                                tablica[j, i - j] = napis[pom];
                                //Console.WriteLine("j {0}, i{1}, {2}", j, i-j, tablica[j, i - j]);
                                //Console.WriteLine(tablica[j, i - j]);
                                pom++;
                            }
                        }
                        if (i + j < dlugosc)
                        {
                            tablica[j, i + j] = napis[pom];
                            //Console.WriteLine("j {0}, i{1}, {2}", j, i+j, tablica[j, i + j]);
                            //Console.WriteLine(tablica[j, i + j]);
                            pom++;
                        }

                    }
                    i = i + 2 * (n - 1);
                }
            }


            for (i = 0; i < dlugosc; i++)
            {
                for (j = 0; j < n; j++)
                {
                    if (tablica[j, i] != '0')
                    {
                        //Console.WriteLine("i {0}, j{1}, {2}", i, j, tablica[j, i]);
                        wynik = wynik + (char)tablica[j, i];
                    }

                }
            }

            return wynik;
        }
    }
}
