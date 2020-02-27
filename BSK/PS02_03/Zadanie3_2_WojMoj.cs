using System;
using System.Linq;

namespace TranspositionCipher2
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
            // Assumption: message needs to be shorter than (1+key.Length)*key.Length/2
            if(message.Length>(1+key.Length)*key.Length/2)
            {
                throw new ArgumentException("Message is too large for the given key");
            }
            Cypher(trimmedMessage, key);            

        }

        static string Cypher(string message, string key)
        {
            string encryptedMessage = "";
            char?[,] transpositionMatrix = new char?[key.Length, key.Length];
            int index = 0;
            int messageIndex = 0;
            for (int i = 0; i < alphabet.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (key[j] == alphabet[i])
                    {
                        for(int k = 0; k<j; k++)
                        {
                            if(messageIndex<message.Length)
                            {
                                transpositionMatrix[index, k] = message[messageIndex];
                                messageIndex++;
                            }
                        }
                        index++;
                    }
                }
            }
            for (int i = 0; i < key.Length; i++)
            {
                for(int j = 0; j < key.Length; j++)
                {
                    if(transpositionMatrix[i,j].HasValue)
                    {
                        Console.Write(transpositionMatrix[i, j]);
                    }
                }
                Console.Write("\n");
            }
                
            return encryptedMessage;
        }
    }
}
