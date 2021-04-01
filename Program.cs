using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TI_Lab_1
{
    class Program
    {
        const string turningKey1 = "1010000001001100100001000";
        const string turningKey2 = "0000100000001010011100010";

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

        public static string Caesar_Encrypt(string plaintext, Int32 key)
        {
            string ciphertext = "";

            for (Int32 i = 0; i < plaintext.Length; i++)
            {
                if (plaintext[i] >= 'a' && plaintext[i] <= 'z')
                    ciphertext += (char)((plaintext[i] - 'a' + key) % 26 + 'a');
                else if (plaintext[i] >= 'A' && plaintext[i] <= 'Z')
                    ciphertext += (char)((plaintext[i] - 'A' + key) % 26 + 'A');
                else if (plaintext[i] >= 'а' && plaintext[i] <= 'ё')
                    ciphertext += (char)((plaintext[i] - 'а' + key) % 33 + 'а');
                else if (plaintext[i] >= 'А' && plaintext[i] <= 'Ё')
                    ciphertext += (char)((plaintext[i] - 'А' + key) % 33 + 'А');
                else ciphertext += (char)(plaintext[i] + key);
            }
            return ciphertext;
        }
        public static string Caesar_Decrypt(string ciphertext, Int32 key)
        {
            string plaintext = "";

            for (Int32 i = 0; i < ciphertext.Length; i++)
            {
                if (ciphertext[i] >= 'a' && ciphertext[i] <= 'z')
                    plaintext += (char)((ciphertext[i] - 'a' + 26 - key) % 26 + 'a');
                else if (ciphertext[i] >= 'A' && ciphertext[i] <= 'Z')
                    plaintext += (char)((ciphertext[i] - 'A' + 26 - key) % 26 + 'A');
                else if (ciphertext[i] >= 'а' && ciphertext[i] <= 'ё')
                    plaintext += (char)((ciphertext[i] - 'а' + 33 - key) % 33 + 'а');
                else if (ciphertext[i] >= 'А' && ciphertext[i] <= 'Ё')
                    plaintext += (char)((ciphertext[i] - 'А' + 33 - key) % 33 + 'А');
                else plaintext += (char)(ciphertext[i] - key);
            }
            return plaintext;
        }

        public static string Column_Encrypt(string plaintext, string key)
        {
            string ciphertext = "";
            Int32[] indexKey = new Int32[key.Length];           
            Int32 i = 0, ind = 0, j = 0, rowNum = 0;
            if (plaintext.Length % key.Length != 0)
                rowNum++;
            rowNum += (plaintext.Length / key.Length);
            char[,] columnText = new char[rowNum, key.Length];

            for (i = 0; i < key.Length - 1; i++)
            {
                for (j = i + 1; j < key.Length; j++)
                {
                    if (key[i] <= key[j])
                        indexKey[j]++;
                    else
                        indexKey[i]++;
                }
            }

            for (i = 0; i < rowNum; i++)
            {
                for (j = 0; j < key.Length && ind < plaintext.Length; j++)
                {
                    columnText[i, j] = (char)(plaintext[ind]);
                    ind++;
                }
            }

            ind = 0;
            for (i = 0; i < key.Length; i++)
            {
                j = 0;
                while (indexKey[j] != ind)
                    j++;

                for (Int32 k = 0; k < rowNum; k++)
                {
                    if (columnText[k, j] != '\0')
                        ciphertext += columnText[k, j];
                }
                ind++;
            }

            return ciphertext;
        }
        public static string Column_Decrypt(string ciphertext, string key)
        {
            string plaintext = "";
            Int32[] indexKey = new Int32[key.Length];
            Int32 i = 0, j = 0, ind = ciphertext.Length - 1, rowNum = 0;
            if (ciphertext.Length % key.Length != 0)
                rowNum++;
            rowNum += (ciphertext.Length / key.Length);
            char[,] columnText = new char[rowNum, key.Length];

            for (i = 0; i < key.Length - 1; i++)
            {
                for (j = i + 1; j < key.Length; j++)
                {
                    if (key[i] <= key[j])
                        indexKey[j]++;
                    else
                        indexKey[i]++;
                }
            }

            for (i = key.Length - 1; i >= 0; i--)
            {
                j = 0;
                while (indexKey[j] != i)
                    j++;
                if (indexKey[j] == i)
                {
                    Int32 row = rowNum;
                    if ((ciphertext.Length - j - 1) / key.Length != rowNum - 1)
                        row--;

                    for (Int32 k = row - 1; k >= 0 && ind >= 0; k--)
                    {
                        columnText[k, j] = (char)(ciphertext[ind]);
                        ind--;
                    }
                }
            }

            for (i = 0; i < rowNum; i++)
            {
                for (j = 0; j < key.Length; j++)
                {
                    if (columnText[i, j] != '\0')
                        plaintext += columnText[i, j];
                }
            }

            return plaintext;
        }

        public static string Turning_Encrypt(string plaintext, string key)
        {
            string ciphertext = "";
            Int32 index = 0;
            bool[,] punchingMask;
            Int32 sizeMatr = 5;
            punchingMask = new bool[sizeMatr, sizeMatr];
            char[,] cipherMatrix = new char[sizeMatr, sizeMatr];
            bool saveCenterCell;

            for (Int32 i = 0; i < sizeMatr; ++i)
                for (Int32 j = 0; j < sizeMatr; ++j)
                    if (key[index++] == '0')
                        punchingMask[i, j] = false;
                    else
                        punchingMask[i, j] = true;

            saveCenterCell = punchingMask[sizeMatr / 2, sizeMatr / 2];
            index = 0;

            while (plaintext.Length % (sizeMatr * sizeMatr) != 0)
                plaintext += ' ';                      

            while (index < plaintext.Length - 1)
            {
                for (Int32 i = 0; i < sizeMatr; ++i)
                {
                    for (Int32 j = 0; j < sizeMatr; ++j)
                    {
                        if (punchingMask[i, j])
                            cipherMatrix[i, j] = plaintext[index++];
                    }
                }

                if (punchingMask[sizeMatr / 2, sizeMatr / 2])
                    punchingMask[sizeMatr / 2, sizeMatr / 2] = false;
                
                for (Int32 i = 0; i < sizeMatr; i++)
                {
                    for (Int32 j = 0; j < sizeMatr; j++)
                    {
                        if (punchingMask[i, j])
                            cipherMatrix[j, sizeMatr - 1 - i] = plaintext[index++];
                    }
                }

                for (Int32 i = sizeMatr - 1; i >= 0; i--)
                {
                    for (Int32 j = sizeMatr - 1; j >= 0; j--)
                    {
                        if (punchingMask[sizeMatr - 1 - i, sizeMatr - 1 - j])
                            cipherMatrix[i, j] = plaintext[index++];
                    }
                }

                for (Int32 i = 0; i < sizeMatr - 1; i++)
                {
                    for (Int32 j = sizeMatr - 1; j >= 0; j--)
                    {
                        if (punchingMask[i, sizeMatr - 1 - j])
                            cipherMatrix[j, i] = plaintext[index++];
                    }
                }

                for (int i = 0; i < sizeMatr; ++i)
                {
                    for (int j = 0; j < sizeMatr; ++j)
                    {
                        ciphertext += cipherMatrix[i, j];
                    }
                }

                punchingMask[sizeMatr / 2, sizeMatr / 2] = saveCenterCell;
            }

            return ciphertext;
        }
        public static string Turning_Decrypt(string ciphertext, string key)
        {
            string plaintext = "";
            Int32 index = 0;
            bool[,] punchingMask;
            Int32 sizeMatr = 5;
            punchingMask = new bool[sizeMatr, sizeMatr];
            char[,] plainMatrix = new char[sizeMatr, sizeMatr];
            bool saveCenterCell;

            for (Int32 i = 0; i < sizeMatr; ++i)
                for (Int32 j = 0; j < sizeMatr; ++j)
                    if (key[index++] == '0')
                        punchingMask[i, j] = false;
                    else
                        punchingMask[i, j] = true;

            saveCenterCell = punchingMask[sizeMatr / 2, sizeMatr / 2];
            index = 0;

            while (ciphertext.Length % (sizeMatr * sizeMatr) != 0)
                ciphertext += ' ';

            for (Int32 i = 0; i < sizeMatr; i++)
            {
                for (Int32 j = 0; j < sizeMatr; j++)
                {
                    plainMatrix[i, j] = ciphertext[index++];
                }
            }

            for (Int32 i = 0; i < sizeMatr; ++i)
            {
                for (Int32 j = 0; j < sizeMatr; ++j)
                {
                    if (punchingMask[i, j])
                        plaintext += plainMatrix[i, j];
                }
            }

            if (punchingMask[sizeMatr / 2, sizeMatr / 2])
                punchingMask[sizeMatr / 2, sizeMatr / 2] = false;

            for (Int32 i = 0; i < sizeMatr; i++)
            {
                for (Int32 j = 0; j < sizeMatr; j++)
                {
                    if (punchingMask[i, j])
                        plaintext += plainMatrix[j, sizeMatr - 1 - i];
                }
            }

            for (Int32 i = sizeMatr - 1; i >= 0; i--)
            {
                for (Int32 j = sizeMatr - 1; j >= 0; j--)
                {
                    if (punchingMask[sizeMatr - 1 - i, sizeMatr - 1 - j])
                        plaintext += plainMatrix[i, j];
                }
            }

            for (Int32 i = 0; i < sizeMatr - 1; i++)
            {
                for (Int32 j = sizeMatr - 1; j >= 0; j--)
                {
                    if (punchingMask[i, sizeMatr - 1 - j])
                        plaintext += plainMatrix[j, i];
                }
            }
            punchingMask[sizeMatr / 2, sizeMatr / 2] = saveCenterCell;

            return plaintext;
        }


        static void Main(string[] args)
        {
            Int32 chipher, action;
            string plaintext, key;            
            
            string name = (Column_Encrypt("ALESYA VERY LUCK", "CRYPTOGRAPHY"));
            Console.WriteLine(name);
            Console.WriteLine(Column_Decrypt(name, "CRYPTOGRAPHY"));
            
            do
            {
                Console.WriteLine("Choose chipher:\n1. RailFence.\n2. Column.\n3. Turning.\n4. Caesar.\n");
                chipher = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Choose action:\n1. Encrypt.\n2. Decrypt.\n");
                action = Int32.Parse(Console.ReadLine());

                Console.Write("Enter your text to encrypt: ");
                plaintext = Console.ReadLine();

                Console.Write("Enter your key to encrypt: ");
                key = Console.ReadLine();

                if (action == 1)
                {
                    switch (chipher)
                    {
                        case 1:
                            Console.WriteLine(RailFence_Encrypt(plaintext, Convert.ToInt32(key)));
                            break;
                        case 2:
                            Console.WriteLine(Column_Encrypt(plaintext, key));
                            break;
                        case 3:
                            Console.WriteLine(Turning_Encrypt(plaintext, turningKey2));
                            break;
                        case 4:
                            Console.WriteLine(Caesar_Encrypt(plaintext, Convert.ToInt32(key)));
                            break;
                        default:
                            Console.WriteLine("Nothing choose...");
                            break;
                    }
                }
                else if (action == 2)
                {
                    switch (chipher)
                    {
                        case 1:
                            Console.WriteLine(RailFence_Decrypt(plaintext, Convert.ToInt32(key)));
                            break;
                        case 2:
                            Console.WriteLine(Column_Decrypt(plaintext, key));
                            break;
                        case 3:
                            Console.WriteLine(Turning_Decrypt(plaintext, turningKey2));
                            break;
                        case 4:
                            Console.WriteLine(Caesar_Decrypt(plaintext, Convert.ToInt32(key)));
                            break;
                        default:
                            Console.WriteLine("Nothing choose...");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Nothing choose...");
                }

                Console.WriteLine("\n");

            } while (true);
            
        }

    }
}
