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
              int j = 0;

               int[] tab_key = new int[d];
              string dtext = "";

              for (int i = 0; i < key.Length; i++)
              {
                  if (!key[i].Equals('-'))
                  {
                      tab_key[j] = int.Parse(key[i].ToString());
                      j++;
                  }
              }

            Array.Reverse(tab_key);//odwrócenie tablicy  
            
            

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
                          dtext += row[tab_key[k] - 1];
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
                          dtext += row[tab_key[k] - 1];
                      }
                  }
              }

              return dtext;
          }

    }
}
