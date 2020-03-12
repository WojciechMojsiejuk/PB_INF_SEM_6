using System;

namespace Vigenere
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = "CRYPTOGRAPHY";
            string key = "BREAKBREAKBR";
            string encyptedMessage = Cypher(message, key);
            Console.WriteLine(encyptedMessage);
            Console.WriteLine(Decypher(encyptedMessage, key));
        }

        static string Cypher(string message, string key)
        {
            string encryptedMessage = "";
            for(int i=0; i<message.Length; i++)
            {
                encryptedMessage += Convert.ToChar( (((int)message[i]-(int)'A') + (key[i % key.Length] - (int)'A'))%((int)'Z' - (int)'A' + 1)+(int)'A');
            }
            return encryptedMessage;
        }

        static string Decypher(string encryptedMessage, string key)
        {
            string revertedKey = "";
            for(int i=0; i<key.Length; i++)
            {
                revertedKey += Convert.ToChar((((int)'Z' - (int)'A' + 1) - (key[i] - (int)'A')) % ((int)'Z' - (int)'A' + 1)+(int)'A');
            }
            return Cypher(encryptedMessage, revertedKey);
        }
    }
}
