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

        public static string RailFence_Decrypt(string ciphertext, Int32 key)
        {
            string plaintext = "";
            Int32 max_offset = 2 * (key - 1);
            string[] matrix = new string[key];
            Int32 row, charInRow, index = 0;

            for (Int32 i = 1; i <= key; i++)
            {
                charInRow = 0;

                for (Int32 j = 0; j < ciphertext.Length; j++)
                {
                    row = (j + 1) % max_offset;

                    if (row == 0)
                        row = max_offset;
                    if (row > key)
                        row = key - (row - key);
                    if (row == i)
                        charInRow++;
                }

                for (Int32 j = 0; j < charInRow; j++, index++)
                    matrix[i - 1] += ciphertext[index];
            }

            Int32 len = 0;
            Int32 offset = -1;
            index = 0;
            while (len < ciphertext.Length)
            {
                plaintext += matrix[index][0];
                matrix[index] = matrix[index].Substring(1);

                if (index + 1 == key || index + 1 == 1)
                    offset = -offset;

                index += offset;
                len++;
            }

            return plaintext;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(RailFence_Encrypt("SOME MESSAGE", 4));
            Console.WriteLine(RailFence_Decrypt(RailFence_Encrypt("SOME MESSAGE", 4), 4));
        }
    }
}
