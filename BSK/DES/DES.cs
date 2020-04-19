using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReusableConsoleApp
{
    public class DES
    {
        BinaryReader binaryReader;
        BinaryWriter binaryWriter;
        //tablica permutacyjne
        private int[] IPtable = { 58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4, 62, 54, 46, 38, 
            30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8, 57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 
            11, 3, 61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7 };
        private int[] PC1tableLeft = { 57,49,41,33,25,17,9,1,58,50,42,34,26,16,10,2,59,51,43,35,27,19,11,3,60,52,44,36 };
        private int[] PC1tableRight = { 63,55,47,39,31,23,15,7,62,54,46,38,30,22,14,6,61,53,45,37,29,21,13,5,28,20,12,4};
        private int[] PC2table = { 14,17,11,24,1,5,3,28,15,6,21,10,23,19,12,4,26,8,16,7,27,20,13,2,41,52,31,37,47,55,30,
            40,51,45,33,48,44,49,39,56,34,53,46,42,50,36,29,32};
        private int[] LeftShiftTable = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
        //end tablice permutacyjne

        //bitarraye do przechowywania kluczy
        BitArray left28bitKey;
        BitArray right28bitKey;
        BitArray feistel48bitkey;
        byte[] bytes;
        BitArray initialKEY;
        public DES(string sourceFile,string outputFile,string key)
        {
            byte[] text;
            text = System.Text.Encoding.UTF8.GetBytes(key);
            Array.Reverse(text);
            initialKEY = new BitArray(text);
            //podzielenie klucza na dwie czesci po 28- bitów
            permutationPC1();
            //inicjalizacja 48bitowego klucza
            feistel48bitkey = new BitArray(new byte[6]);
            bytes = new byte[1024];
            //open file
            try
            {
                binaryReader = new BinaryReader(new FileStream(sourceFile, FileMode.Open));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot open file.");
                return;
            }
            try
            {
                binaryWriter = new BinaryWriter(new FileStream(outputFile, FileMode.Create));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot create file.");
                return;
            }
        }
        
        /// <summary>
        /// Funkcja wypisująca do konsoli BitArray do konsoli w postaci 0 i 1
        /// </summary>
        /// <param name="bitArray"></param>
        public void WriteBits(BitArray bitArray)
        {
            for(int i=0;i<bitArray.Length;i++)
            {
                Console.Write(bitArray[i] ? "1 " : "0 ");
            }
            Console.WriteLine("end of bit-array");
        }
        public void Close()
        {
            binaryReader.Close();
            binaryWriter.Close();
        }
        //
        public void readBIn()
        {         
            //readfile
            try
            {                
                bytes = binaryReader.ReadBytes(1024);

                //array 64-bitowy do Des-a
                BitArray bitArray;

                //arraye 32-bitowe do podzielenia
                BitArray leftPlainText;
                BitArray rightPlainText;

                for (int j=0;j< 1024; j += 8)
                {
                    byte[] tempByteArray = new byte[8];
                    //przepisanie 8bajtów=64bitów z arraya głownego do tymczasowego który będzie obsłużony przez DES
                    for (int i =0;i<8;i++)
                    {
                        tempByteArray[i] = bytes[j + i];
                    }
                    //array 64bitowy bitowy z tekstem
                    bitArray = new BitArray(tempByteArray);

                    //permutacja wstepna
                    bitArray = permutationIP(bitArray);

                    //podzial
                    leftPlainText = new BitArray(split(bitArray, 0));
                    rightPlainText = new BitArray(split(bitArray, 32));                    
                    //FEISTELLLL !!!11ONEONE
                    for(int i = 0; i<16;i++)
                    {
                        //left shift 
                        left28bitKey.LeftShift(LeftShiftTable[i]);
                        right28bitKey.LeftShift(LeftShiftTable[i]);
                        //pc-2 permutacja
                        permutationPC2();
                        //FUNKCJE FEISTELA TUTAJ 

                    }
                }
            }
            catch(IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot read from file.");
                return;
            }          
        }

        //funkcja permutujaca PC-1 dla klucza 
        //z 64bitowego klucza na dwie połowy po 28bitów
        public void permutationPC1()
        {
            left28bitKey = new BitArray(new byte[4]);
            right28bitKey = new BitArray(new byte[4]);
            for(int i = 0;i<28; i++)
            {
                left28bitKey[i] = initialKEY[PC1tableLeft[i] - 1];
            }
            for (int i = 0; i < 28; i++)
            {
                right28bitKey[i] = initialKEY[PC1tableRight[i] - 1];
            }
        }
        public void permutationPC2()
        {
            BitArray temp56bitArray = new BitArray(new byte[7]);
            //złaczenie lewej i prawej części klucza w celu późniejszego przeprowadzenia permutacji
            try {
                int j = 0;
                for (int i = 0; i < 28; i++)
                {
                    temp56bitArray[j] = left28bitKey[i];
                    j++;
                }
                for (int i = 0; i < 28; i++)
                {
                    temp56bitArray[j] = right28bitKey[i];
                    j++;
                }
            }
            catch(IndexOutOfRangeException e)
            {
                Console.WriteLine("YIKES! MAMY PROBLEm " + e.Message);
            }
            
            //permutacja
            for(int i=0;i<48;i++)
            {
                feistel48bitkey[i] = temp56bitArray[PC2table[i] - 1];
            }
        }
        //funkcja dzielaca na LPT i RPT przed funkcja f
        //LPT start =0 gdy RPT start =32
        public BitArray split(BitArray bitArray64, int start)
        {
            BitArray bitArray32 = new BitArray(new byte[4]);
            for(int i=0;i<32;i++)
            {
                bitArray32[i] = bitArray64[start];
                start++;
            }
            return bitArray32;
        }

        //funkcja wykonująca wstępną permutację IP na bloku danych 64bitowym
        public BitArray permutationIP(BitArray bitArray64)
        {
            BitArray newBitArray= new BitArray(new byte[8]);
            for (int i = 0; i < 64; i++)
            {
                newBitArray[i] = bitArray64[(IPtable[i] - 1)];
            }
            return newBitArray;
        }
        //wypisanie do pliku
        public void writeBin()
        {          
            try
            {
                binaryWriter.Write(bytes);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot write to file.");
                return;
            }                   
        }
    }
}
