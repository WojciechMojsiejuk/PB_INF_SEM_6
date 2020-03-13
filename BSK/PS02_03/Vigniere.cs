using System;
using System.Collections.Generic;
using System.Text;

namespace Vigenere
{
    public class Vigniere
    {
        public string multiply(string coder,int length)
        {
            bool temp = true;
            int i = -1;
            char[] wynik = new char[length];
            while(temp)
            {
                for(int j=0;j<coder.Length;j++)
                {
                    if (i == length - 1)
                    {
                        temp = false;
                        break;
                    }
                    else
                    {
                        i++;
                        wynik[i] = coder[j];
                    }                                   
                }
                
            }
            string str = new string(wynik);
            return str;
        }
        
        public char[] vignereCipher(string text, string coder)
        {
            char[] wynik = new char[text.Length];
            if(text.Length>coder.Length)
            {
                coder = multiply(coder,text.Length);
            }
            int temp = 0;

            for (int i = 0; i < text.Length; i++)
            {
                temp = (int)text[i] + ((int)coder[i] - 65);
                if (temp <= 90) wynik[i] = (char)temp;
                else
                {
                    temp = temp - 90;
                    wynik[i] = (char)(64 + temp);
                }
            }
            return wynik;
        }
        public char[] vignereDecipher(string text, string coder)
        {
            char[] wynik = new char[text.Length];
            char[] basic = text.ToCharArray(0, text.Length);
            if (text.Length > coder.Length)
            {
                coder = multiply(coder, text.Length);
            }
            int temp = 0;
            for (int i = 0; i < text.Length; i++)
            {
                temp = (int)basic[i] - ((int)coder[i] - 65);
                wynik[i] = (char)temp;
                if (temp < 65)
                {
                    temp = temp + 90;
                    wynik[i] = (char)(temp - 64);
                }
            }
            return wynik;
        }
    }
}
