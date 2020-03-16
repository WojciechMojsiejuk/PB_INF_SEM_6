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
            int blocksNumber = (int)Math.Ceiling(message.Length / blockSize);
            int index;
            int messageIndex = 0;
            string encryptedMessage = "";
            for (int block = 0; block < blocksNumber; block++)
            {
                char?[,] transpositionMatrix = new char?[key.Length, key.Length];
                index = 0;

                //fill transpositionMatrix
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
            }
            return encryptedMessage;
        }

        public static string Decypher(string message, string key)
        {
            string decipheredMessage = "";
            message = message.ToUpper();
            key = key.ToUpper();
            double blockSize = ((1 + key.Length) * key.Length / 2);
            int blocksNumber = (int)Math.Ceiling(message.Length / blockSize);
            int lastBlockSize = message.Length % (int)blockSize;
            int temp = 0;
            int keyCount = 0;
            int depth = 0;
            bool depthFound = false;
            int[] order = new int[key.Length];

            for (int i = 0; i < alphabet.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (key[j] == alphabet[i])
                    {
                        order[j] = keyCount;
                        keyCount++;
                        temp += j + 1;
                        if (!depthFound)
                        {
                            depth++;
                        }
                        if (temp >= lastBlockSize)
                        {
                            depthFound = true;
                        }
                    }
                }
            }

            BitArray previousIndexes = new BitArray(key.Length);
            int pivot = 0;
            bool increaseTemp;
            for (int block = 0; block < blocksNumber; block++)
            {
                if (block == blocksNumber - 1 && lastBlockSize != 0)
                {
                    //Last block
                    char?[,] transpositionMatrix = new char?[depth, key.Length];
                    int temp2 = 0;

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
                                increaseTemp = true;
                                for (int l = 0; l < depth; l++)
                                {
                                    if (previousIndexes[l] == false && (temp2 + j + 1) <= lastBlockSize && pivot < message.Length)
                                    {
                                        if (increaseTemp)
                                        {
                                            temp2++;
                                            increaseTemp = false;
                                        }
                                        transpositionMatrix[l, j] = message[pivot];
                                        pivot++;
                                    }
                                }
                            }
                        }
                    }

                    //decipher
                    for (int i = 0; i < depth; i++)
                    {
                        for (int j = 0; j < key.Length; j++)
                        {
                            if (transpositionMatrix[i, j].HasValue)
                            {
                                decipheredMessage += transpositionMatrix[i, j];
                            }
                        }
                    }
                }
                else
                {
                    //Full blocks
                    char?[,] transpositionMatrix = new char?[key.Length, key.Length];

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

                                for (int l = 0; l < key.Length; l++)
                                {
                                    if (previousIndexes[l] == false)
                                    {
                                        transpositionMatrix[j, l] = message[pivot];
                                        pivot++;
                                    }
                                }
                            }
                        }
                    }

                    //decipher
                    for (int i = 0; i < key.Length; i++)
                    {
                        for (int j = 0; j < key.Length; j++)
                        {
                            if (transpositionMatrix[j, i].HasValue)
                            {
                                decipheredMessage += transpositionMatrix[j, i];
                            }
                        }
                    }
                }
            }
            return decipheredMessage;
        }

    }
}
