using System;

namespace Skysea
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

    }
}
