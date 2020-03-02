using System;
using System.Collections;
using System.Linq;

namespace TranspositionCipher
{
    class Program
    {
        private static char[] alphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();
        static void Main(string[] args)
        {
            string message = "HERE IS A SECRET MESSAGE ENCIPHERED BY TRANSPOSITION";
            string trimmedMessage = string.Concat(message.Where(c => !char.IsWhiteSpace(c)));
            trimmedMessage = trimmedMessage.ToUpper();
            string key = "CONVENIENCE";
            key = key.ToUpper();
            string[] transpositionMatrix = new string[key.Length];

            for(int i = 0; i<trimmedMessage.Length; i++)
            {
                transpositionMatrix[i % key.Length] += trimmedMessage[i];
            }
            string encryptedMessage = Cypher(transpositionMatrix, key);
            Console.WriteLine(Decypher(encryptedMessage,key));
        }
        
        static string Cypher(string[] message, string key)
        {
            char[] alphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();
            string encryptedMessage = "";
            for(int i = 0; i<alphabet.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if(key[j]==alphabet[i])
                    {
                        encryptedMessage += message[j];
                    }
                }
            }
            return encryptedMessage;
        }

        static string Decypher(string encryptedMessage, string key)
        {
            int mod = encryptedMessage.Length % key.Length;
            int lines = encryptedMessage.Length / key.Length;
            string[] transpositionMatrix = new string[key.Length];
            int start = 0;
            int length;
            string result = "";
            for (int i = 0; i < alphabet.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (key[j] == alphabet[i])
                    {
                        length = lines;
                        if(j<mod)
                        {
                            length++;
                        }
                        transpositionMatrix[j] = encryptedMessage.Substring(start, length);
                        start += length;
                    }
                }
            }
            for(int i=0; i<lines; i++)
            {
                for(int j=0; j<transpositionMatrix.Length; j++)
                {
                    result += transpositionMatrix[j][i];
                }
            }
            if(mod>0)
            {
                for(int i=0; i<mod; i++)
                {
                    result += transpositionMatrix[i][lines];
                }
            }
            return result;
        }
    }
}
