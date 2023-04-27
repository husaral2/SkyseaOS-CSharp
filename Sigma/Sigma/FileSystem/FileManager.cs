using System;
using System.IO;

namespace Sigma.FileSystem
{
    internal class FileManager
    {
        public static bool ListContents(string input)
        {
            string directory = input;

            if (!Directory.Exists(directory))
                directory = Controller.GetCurrentDirectory();

            string[] directories = Directory.GetDirectories(directory);
            string[] files = Directory.GetFiles(directory);
            int x = directories.Length + files.Length;
            for(int i = 0; i < x; i++)
            {
                if (i >= directories.Length)
                {
                    FileInfo fi = new FileInfo(directory + "\\" + files[i - directories.Length]);
                    Console.WriteLine("{0,30} {1}B",fi.Name,fi.Length);
                }
                else
                {
                    Console.WriteLine(directories[i]);
                }
            }
            return true;
        }

        public static bool Navigate(string command)
        {
            string[] splitted = Helper.SpecialSplit(command);
            if (splitted.Length > 1)
            {
                if (splitted[1].Contains(':'))
                {
                    Controller.SetCurrentDirectory(splitted[1]);
                    return true;
                }

                foreach (string s in splitted[1].Split('\\', StringSplitOptions.RemoveEmptyEntries))
                {
                    if (s == "..")
                        Controller.GoBack();
                    else
                    {
                        if (Directory.Exists(Controller.GetCurrentDirectory() + "\\" + s))
                        {
                            Controller.SetCurrentDirectory(Controller.GetCurrentDirectory() + "\\" + s);
                        }
                        else
                            Console.WriteLine("The directory you are trying to navigate doesn't exist.");
                    }
                }
            }
            return true;
        }

    }
}
