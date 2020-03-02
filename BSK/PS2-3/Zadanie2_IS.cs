using System;

//Izabela Siemaszko Zadanie 2
namespace Szyfrowanie_na_macierzy
{
    class Program
    {
        static void Main(string[] args)
        {
            //  Console.WriteLine("text:");
            //String text = Console.ReadLine();

            //            Console.WriteLine("d:");
            //          int d = int.Parse(Console.ReadLine());

            //            Console.WriteLine("key:");
            //          String key = Console.ReadLine();

            String text = "CRYPTOGRAPHYOSA";
            String key = "3-1-4-2";
            int d = 4;
            int j = 0;
            
            int[] tab_key = new int [d];

            for(int i = 0; i < key.Length; i++)
            {
                if (!key[i].Equals('-'))
                {
                    tab_key[j] = int.Parse(key[i].ToString());
                    j++;
                }
            }
        
            int resultMod = text.Length % d;

            String[] row = new String[d];
            j = 0;
            for(int i = 0; i < text.Length; i++)
            {
                row[j] = text[i].ToString();
                j++;
                if (j % d == 0)
                {

                    for(int k = 0; k < tab_key.Length; k++)
                    {
                        Console.Write(row[tab_key[k]-1]);
                        
                    }
                   
                    
                    j = 0;
                    row = new String[d];
                }

            }

            if (resultMod != 0)
            {
                row = new string[d];
                j = 0;
                for(int i = text.Length - resultMod; i < text.Length; i++)
                {
                    row[j] = text[i].ToString();
                    j++;
                }
                for(int k = 0; k < tab_key.Length; k++)
                {
                    if(row[tab_key[k] - 1] != null)
                    {
                       Console.Write(row[tab_key[k] - 1]);
                    }
                }
            }
            Console.WriteLine();


            Console.ReadKey();
        }
    }
}
