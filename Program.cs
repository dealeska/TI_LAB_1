using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TI_Lab_1
{
    class Program
    {
        private static string RailFence_Encrypt(string plaintext, Int32 key)
        {
            string ciphertext = "";
            Int32 max_offset = 2 * (key - 1);
            Int32 row;

            for (Int32 i = 1; i <= key; i++)
            {
                for (Int32 j = 0; j < plaintext.Length; j++)
                {
                    row = (j + 1) % max_offset;

                    if (row == 0)
                        row = max_offset;
                    if (row > key)
                        row = key - (row - key);                   
                    if (row == i)
                        ciphertext += plaintext[j];
                }
            }
            return ciphertext;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(RailFence_Encrypt("SOME MESSAGE", 4));
        }
    }
}
