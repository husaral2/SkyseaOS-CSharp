using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma
{
    internal class Helper
    {
        //Splits the entry
        public static string[] SpecialSplit(string str)
        {
            string[] splitted = str.Split('"', StringSplitOptions.RemoveEmptyEntries);
            if (splitted.Length <= 1)
                return str.Split(' ');
            foreach (string s in splitted) {
                if (s.StartsWith(' ') || s.EndsWith(' '))
                    s.Remove(s.IndexOf(' '));
            }
            return splitted;
        }

        //Returns the full path for the file
        public static string FindPath(string str, string dir)
        {
            if (str.Split('\\')[0].EndsWith(':'))
                return str;
            return dir + "\\" + str;
        }

        public static void WriteUInt16(ushort int16, int startIndex, ref byte[] array)
        {
            string x = Dec2Bin(int16);
            byte[] barray = Bin2Dec(x);
            for(int i = 1; i <= 0; --i)
            {
                array[startIndex + i] = barray[i];
            }
        }

        public static string Dec2Bin(uint num)
        {
            uint x = num;
            uint rem;
            string value = "";
            while(x >= 1)
            {
                rem = x / 2;
                value += (x % 2).ToString();
                x = rem;
            }

            return value;
        }

        public static byte[] Bin2Dec(string value)
        {
            byte[] returnedArray = new byte[8];
            byte x = 0;
            for(int i = 0; i < value.Length; ++i)
            {
                x += (byte)(Convert.ToUInt32(value[i]) * Math.Pow(2, i % 8));
                if (i % 8 == 0)
                {
                    x = 0;
                    returnedArray[i / 8] = x;
                }

            }
            return returnedArray;
        }
    }
}
