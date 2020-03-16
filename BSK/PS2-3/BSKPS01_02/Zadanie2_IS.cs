using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSKPS01_02
{
    class Zadanie2_IS
    {
        public static string Cypher(string text, string key, string n)
        {
            text = text.ToUpper();
            key = key.ToUpper();
            int d = int.Parse(n);
            int j = 0;

             int[] tab_key = new int[d];
            string ctext = "";

            for (int i = 0; i < key.Length; i++)
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
            for (int i = 0; i < text.Length; i++)
            {
                row[j] = text[i].ToString();
                j++;
                if (j % d == 0)
                {

                    for (int k = 0; k < tab_key.Length; k++)
                    {
                        ctext += row[tab_key[k] - 1];
                    }


                    j = 0;
                    row = new String[d];
                }

            }

            if (resultMod != 0)
            {
                row = new string[d];
                j = 0;
                for (int i = text.Length - resultMod; i < text.Length; i++)
                {
                    row[j] = text[i].ToString();
                    j++;
                }
                for (int k = 0; k < tab_key.Length; k++)
                {
                    if (row[tab_key[k] - 1] != null)
                    {
                        ctext += row[tab_key[k] - 1];
                    }
                }
            }

            return ctext;

        }

        public static string Decypher(string text, string key, string n)
        {
            text = text.ToUpper();
            key = key.ToUpper();
            int d = int.Parse(n);

            int k = 0;
            int[] tab_key = new int[d];
            int[] pom_key = new int[d];
            string dtext = "";

            for (int i = 0; i < key.Length; i++)
            {
                if (!key[i].Equals('-'))
                {
                    tab_key[k] = int.Parse(key[i].ToString());
                    pom_key[k] = int.Parse(key[i].ToString());
                    k++;
                }
            }
            
            int pivot = 0;
            int size = (int)Math.Ceiling((double)text.Length / (int)d);
            int rozmiarWierszaOstatniego = text.Length % d;
            char?[,] matrix = new char?[d, size];
             char[] tab = new char[rozmiarWierszaOstatniego];
            if (rozmiarWierszaOstatniego != 0)
            {
                for (int i = 0; i < size - 1; i++)
                {
                    for (int j = 0; j < d; j++)
                    {
                        try
                        {
                            matrix[(pom_key[j] - 1), i] = text[j + ((d) * pivot)];
                        }
                        catch (IndexOutOfRangeException)
                        {

                        }
                    }
                    pivot++;
                }
                pivot++;
                //ostatni wiersz obsluga
                tab = new char[rozmiarWierszaOstatniego];
                for(int x=text.Length-rozmiarWierszaOstatniego;x<text.Length;x++)
                {
                    char aktualnaLitera = text[x];
                    for(int y=0;y<d;y++)
                    {
                        if(pom_key[y] <= rozmiarWierszaOstatniego && tab[pom_key[y] - 1]=='\0')
                        {
                            try
                            {
                                
                                tab[pom_key[y] - 1] = text[x];
                                
                                break;
                            }
                            catch(IndexOutOfRangeException)
                            {

                            }
                            
                        }
                    }
                }
                System.Diagnostics.Debug.WriteLine(new string(tab));
               
            }
            else
            {
                //char?[,] matrix = new char?[d, size];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < d; j++)
                    {
                        try
                        {
                            matrix[(pom_key[j] - 1), i] = text[j + ((d) * pivot)];
                        }
                        catch (IndexOutOfRangeException)
                        {

                        }
                    }
                    pivot++;
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < d; j++)
                {
                    if(matrix[j,i].HasValue)
                    {
                        dtext += matrix[j,i];
                    }
                }
            }
            for (int r = 0; r < tab.Length; r++)
            {
                dtext += tab[r];
            }
               // System.Diagnostics.Debug.WriteLine(tab[r]);
            return dtext;
           
        }
        }
}
