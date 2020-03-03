using System;

namespace BSKPS01_02
{
    class Zadanie5_WM
    {
        public static string Cypher(string message, string key)
        {
            message = message.ToUpper();
            key = key.ToUpper();
            string encryptedMessage = "";
            for(int i=0; i<message.Length; i++)
            {
                encryptedMessage += Convert.ToChar( (((int)message[i]-(int)'A') + (key[i % key.Length] - (int)'A'))%((int)'Z' - (int)'A' + 1)+(int)'A');
            }
            return encryptedMessage;
        }

        public static string Decypher(string encryptedMessage, string key)
        {

            encryptedMessage = encryptedMessage.ToUpper();
            key = key.ToUpper();
            string revertedKey = "";
            for(int i=0; i<key.Length; i++)
            {
                revertedKey += Convert.ToChar((((int)'Z' - (int)'A' + 1) - (key[i] - (int)'A')) % ((int)'Z' - (int)'A' + 1)+(int)'A');
            }
            return Cypher(encryptedMessage, revertedKey);
        }
    }
}
