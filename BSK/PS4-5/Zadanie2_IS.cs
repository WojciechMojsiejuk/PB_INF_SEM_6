using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadani1
{
    class Program
    {

        
      
        static void Main(string[] args)
        {
            string dane = @"C:\Users\izabe\OneDrive\Pulpit\BSK\dane.txt";
            string fileName = @"C:\Users\izabe\OneDrive\Pulpit\BSK\test.bin";
        string file = @"C:\Users\izabe\OneDrive\Pulpit\BSK\res.txt";
        string s = "1000";
            string d = "0111";

            byte[] fileBytes = File.ReadAllBytes(fileName);
            StringBuilder sb = new StringBuilder();


            foreach (byte b in fileBytes)
            {
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));

            }
            File.WriteAllText(dane, sb.ToString());
            String x = sb.ToString();
            // string x="10111010";
            int[] seed = new int[s.Length];
            int[] degree = new int[d.Length];
         //   int[] dane = new int[x.Length];
            int[] key = new int[x.Length];

            for (int i = 0; i < s.Length; i++)
            {
                seed[i] = int.Parse(s[i].ToString());
              //  Console.Write(seed[i]);

            }
           // Console.WriteLine();
            int n = 0;//liczba jedynek w stopniu
            for (int i = 0; i < d.Length; i++)
            {
                degree[i] = int.Parse(d[i].ToString());
                if (degree[i] == 1)
                {
                    n++;
                }
                //Console.WriteLine(degree[i]);
            }



           
            
          /*  for (int i = 0; i < x.Length; i++)
            {
                dane[i] = int.Parse(x[i].ToString());
            }*/

            int[] pom = new int[s.Length];
            int[] seedTMP = new int[n];
            int k = 0, tmp = 0;
            for (int i = 0; i < x.Length; i++)
            {
                key[i] = seed[0];
                for (int j = 0; j < s.Length; j++)
                {
                    pom[j] = seed[j];
                }


                for (int j = 0; j < d.Length; j++)
                {
                    if (degree[j] == 1)
                    {
                        seedTMP[k] = seed[j];
                        k++;
                    }
                }

                for (int l = 1; l < n; l++)
                {

                    if ((seedTMP[l] == 1 && seedTMP[tmp] == 0) || (seedTMP[l] == 0 && seedTMP[tmp] == 1))
                    {
                        seedTMP[l] = 1;

                    }
                    else
                    {
                        seedTMP[l] = 0;
                    }
                    tmp++;
                }
                seed[0] = seedTMP[n - 1];
              //  Console.Write(seed[0]);
                k = 0;
                tmp = 0;
                for (int j = 1; j < s.Length; j++)
                {
                    seed[j] = pom[k];
                    k++;
                   // Console.Write(seed[j]);
                }
               // Console.WriteLine();
                k = 0;
                pom = new int[s.Length];
            }
            key[key.Length - 1] = seed[0];

           /* Console.WriteLine("KEY:");
            for (int i = 0; i < x.Length; i++)
            {
                Console.WriteLine(key[i]);
            }*/

           //xor x i key
          
            for(int i = 0; i < x.Length; i++)
            {
                if(int.Parse(x[i].ToString())==1 && key[i]==0 || int.Parse(x[i].ToString())== 0 && key[i] == 1)
                {
                    sb.Insert(i, '1');
                   
                }
                else
                {
                    sb.Insert(i, '0');
                   
                }
               
            }
           
            File.WriteAllText(file, sb.ToString());

            Console.WriteLine("Sprawdź plik res.txt");
            Console.ReadKey();
        }
    }
}
