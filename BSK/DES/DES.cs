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
        //tablica permutacyjna wstępna
        private int[] IPtable = { 58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4, 62, 54, 46, 38, 
            30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8, 57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 
            11, 3, 61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7 };
        private int[] PC1table = { };
        byte[] bytes;
        private BitArray KEY;
        public DES(string sourceFile,string outputFile,string key)
        {
            byte[] text;
            text = System.Text.Encoding.UTF8.GetBytes(key);
            Array.Reverse(text);
            KEY = new BitArray(text);
            bytes = new byte[102400];
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
        public void readBIn()
        {         
            //readfile
            try
            {                
                bytes = binaryReader.ReadBytes(1024);
                //array 64-bitowy do Des-a
                BitArray bitArray;
                //arraye 32-bitowe do podzielenia
                BitArray LPT;
                BitArray RPT;
                for (int j=0;j< 1024; j += 8)
                {
                    byte[] tempByteArray = new byte[8];
                    
                    for (int i =0;i<8;i++)
                    {
                        tempByteArray[i] = bytes[j + i];
                    }
                    bitArray = new BitArray(tempByteArray);
                    //permutacja wstepna
                    bitArray = permutationIP(bitArray);
                    //podzial
                    LPT = new BitArray(split(bitArray, 0));
                    RPT = new BitArray(split(bitArray, 32));
                }
            }
            catch(IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot read from file.");
                return;
            }          
        }
        //funkcja permutujaca klucz PC-1
        public BitArray permutationPC1(BitArray bitArray64)
        {
            BitArray newBitArray = new BitArray(new byte[7]);
            return newBitArray;
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
