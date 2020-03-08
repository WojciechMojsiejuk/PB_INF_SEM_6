using System;
using System.Collections;
using System.Linq;

namespace  BSKPS01_02
{
    static class Zadanie3_2_WM
    {
        //alphabet used to find order of the keyword
        private static char[] alphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();

        public static string Cypher(string message, string keyword)
        {
            //deleting whitespaces
            message = string.Concat(message.Where(c => !char.IsWhiteSpace(c)));
            //changing string to uppercase
            message = message.ToUpper();
            //changing string to uppercase
            string key = keyword.ToUpper();
            double blockSize = ((1 + key.Length) * key.Length / 2);
            int blocksNumber = (int)Math.Ceiling(message.Length/ blockSize);
           
            string encryptedMessage = "";
            char?[,] transpositionMatrix = new char?[key.Length*blocksNumber, key.Length];
            int index = 0;
            int messageIndex = 0;
            for(int block = 0; block<blockSize; block++)
            {
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
            }
            for (int i = 0; i < alphabet.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (key[j] == alphabet[i])
                    {
                        for (int k = 0; k < key.Length*blocksNumber; k++)
                        {
                            if (transpositionMatrix[k, j].HasValue)
                            {
                                encryptedMessage += transpositionMatrix[k, j];
                            }
                        }
                    }
                }
            }
            for(int i = 0; i< key.Length*blocksNumber; i++)
            {
                for(int j = 0; j<key.Length; j++)
                {
                    Console.Write(transpositionMatrix[i, j]);
                }
                Console.Write("\n");
            }
            return encryptedMessage;
        }

        public static string Decypher(string message, string key)
        {
            message = message.ToUpper();
            key = key.ToUpper();
            string decryptedMessage = "";
            int temp = 0;
            int keyCount = 0;
            int depth = 0;
            int[] order = new int[key.Length];
            int pivot = 0;
            double blockSize = ((1 + key.Length) * key.Length / 2);
            int blocksNumber = (int)Math.Ceiling(message.Length / blockSize);
           
            for(int block = 0; block<blocksNumber; block++)
            {
                for (int i = 0; i < alphabet.Length; i++)
                {
                    for (int j = 0; j < key.Length; j++)
                    {
                        if (key[j] == alphabet[i])
                        {
                            if(block == 0)
                            { 
                                order[j] = keyCount;
                            }
                            keyCount++;
                            temp += j + 1;
                            if (temp < message.Length)
                            {
                                depth = keyCount + 1;
                            }
                        }
                    }
                }
            }
            
            char?[,] transpositionMatrix = new char?[key.Length, depth];

            int characterCount = 0;

            BitArray previousIndexes = new BitArray(key.Length);
            int depthCounter = 0;

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

                        depthCounter = 0;
                        for (int l = 0; l < key.Length*blocksNumber; l++)
                        {
                            if(l == key.Length*(blocksNumber-1))
                            {
                                depthCounter = (blocksNumber - 1) * (int)blockSize;
                            }
                            if (previousIndexes[l%key.Length] == false)
                            {
                                depthCounter += (j + 1);
                                if (pivot < message.Length && l < depth && depthCounter<message.Length)
                                {
                                    transpositionMatrix[j, l] = message[pivot];
                                    pivot++;

                                }
                            }
                        }
                        if (characterCount + j + 1 - message.Length > 0 && characterCount + j + 1 - message.Length < j + 1)
                        {
                            for (int m = 0; m < characterCount + j + 1 - message.Length; m++)
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
