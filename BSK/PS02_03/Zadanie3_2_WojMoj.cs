using System;
using System.Collections;
using System.Linq;

namespace BSK_PS01_TranspositionMatrix
{
    class Program
    {
        //alphabet used to find order of the keyword
        private static char[] alphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();

        static void Main(string[] args)
        {
            string message = "HEREISASECRETMESSAGEENCIPHEREDBYTRANSPOSITION";
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
            Console.WriteLine(encryptedMessage);
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
            for(int i = 0; i< key.Length; i++)
            {
                for(int j = 0; j<key.Length; j++)
                {
                    Console.Write(transpositionMatrix[i, j]);
                }
                Console.Write("\n");
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

            int characterCount = 0;

            BitArray previousIndexes = new BitArray(key.Length);
            for (int i = 0; i < alphabet.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (key[j] == alphabet[i] && pivot < message.Length)
                    {
                        previousIndexes.SetAll(false);
                        for (int k = 0; k < j; k++)
                        {
                            previousIndexes[order[k]] = true;
                        }
                        for (int l = 0; l < key.Length; l++)
                        {
                            if (previousIndexes[l] == false)
                            {
                                if(pivot<message.Length && l<depth)
                                {
                                    transpositionMatrix[j, l] = message[pivot];
                                    pivot++;
                                    
                                }
                            }
                        }
                        if(characterCount + j+1 - message.Length > 0 && characterCount + j +1 - message.Length<j+1)
                        {
                            for(int m = 0; m < characterCount + j + 1 - message.Length; m++)
                            {
                                pivot--;
                                transpositionMatrix[j, depth - m - 1] = null;
                            }
                        }
                        characterCount += j + 1;
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
                        Console.Write(transpositionMatrix[j, i]);
                    }
                }
                Console.Write("\n");
            }
            return decryptedMessage;
        }

    }
}
