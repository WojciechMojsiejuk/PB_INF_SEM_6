using System;
using System.Linq;

namespace TranspositionCipher2
{
        class Program
    {
        //alphabet used to find order of the keyword
        private static char[] alphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();

        static void Main(string[] args)
        {
            string message = "HERE IS A SECRET MESSAGE ENCIPHERED BY TRANSPOSITION";
            //deleting whitespaces
            string trimmedMessage = string.Concat(message.Where(c => !char.IsWhiteSpace(c)));
            //changing string to uppercase
            trimmedMessage = trimmedMessage.ToUpper();
            string key = "CONVENIENCE";
            //changing string to uppercase
            key = key.ToUpper();
            // Assumption: message needs to be shorter than (1+key.Length)*key.Length/2
            if (message.Length > (1 + key.Length) * key.Length / 2)
            {
                throw new ArgumentException("Message is too large for the given key");
            }
            string encryptedMessage = Cypher(trimmedMessage, key);
            Console.WriteLine(Decypher(encryptedMessage, key));

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
                        for (int k = 0; k <= j; k++)
                        {
                            if (messageIndex < message.Length)
                            {
                                transpositionMatrix[index, k] = message[messageIndex];
                                messageIndex++;
                            }
                        }
                        index++;
                    }
                }
            }
            for (int i = 0; i < alphabet.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (key[j] == alphabet[i])
                    {
                        for (int k = 0; k < key.Length; k++)
                        {
                            if (transpositionMatrix[k, j].HasValue)
                            {
                                encryptedMessage += transpositionMatrix[k, j];
                            }
                        }
                    }
                }
            }
            return encryptedMessage;
        }

        static string Decypher(string message, string key)
        {
            string decryptedMessage = "";
            int temp = 0;
            int keyCount = 0;
            int depth = 0;
            int[] order = new int[key.Length];
            int pivot = 0;
            bool endOfMessage = false;
            for (int i = 0; i < alphabet.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (key[j] == alphabet[i])
                    {
                        order[j] = keyCount;
                        keyCount++;
                        temp += j + 1;
                        if (temp < message.Length)
                        {
                            depth = keyCount+1;
                        }
                    }
                }
                if(endOfMessage)
                {
                    break;
                }
            }

            char?[,] transpositionMatrix = new char?[key.Length, depth];

            BitArray previousIndexes = new BitArray(key.Length);
            for (int i = 0; i < alphabet.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (key[j] == alphabet[i])
                    {
                        previousIndexes.SetAll(false);
                        for (int k = 0; k < j; k++)
                        {
                            previousIndexes[order[k]] = true;
                        }
                        for (int l = 0; l < depth; l++)
                        {
                            if (!previousIndexes[l])
                            {
                                if(pivot<message.Length)
                                {
                                    transpositionMatrix[j, l] = message[pivot];
                                    pivot++;
                                }
                                
                            }
                        }
                    }
                }

            }
            for(int i = 0; i<depth; i++)
            {
                for(int j = 0; j<key.Length; j++)
                {
                    if(transpositionMatrix[j, i].HasValue)
                    {
                        decryptedMessage += transpositionMatrix[j, i];
                    }
                }
            }
            return decryptedMessage;
        }

    }
}
