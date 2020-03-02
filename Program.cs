using System;

namespace zadanie1poprawka
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            int j = 0;
            string napis = "Ala_ma_kota";
            int n = 4;
            int dlugosc = napis.Length;
            int[,] tablica = new int[dlugosc,n];
            int pom = 0;
            int count = dlugosc / n;
            while(i < napis.Length-1)
            {
                while (pom != count)
                {                    
                    while (j != n-1)
                    {
                        Console.WriteLine(napis[i]);
                        tablica[i, j] = napis[i];
                        j++;
                        i++;
                    }

                    while (j != 0)
                    {
                        tablica[i, j] = napis[i];
                        Console.WriteLine(napis[i]);
                        j--;
                        i++;
                    }
                    pom++;
                }
            }

            for (int m=0;m<n;m++)
            {
                for (int l = 0; l<dlugosc; l++)
                {
                    Console.WriteLine(tablica[m,l]);        
                }
            }
            //Console.WriteLine("Hello World!");
        }
    }
}
