using System;

namespace ReusableConsoleApp
{
    class Program
    {
       
        static void Main(string[] args)
        {

            DES des = new DES("test.bin", "test69.bin","bajtowxd");
            des.readBIn();
            des.writeBin();

        }
      

       
        
    }
}
