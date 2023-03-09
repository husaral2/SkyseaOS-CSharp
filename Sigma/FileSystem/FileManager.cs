using System.IO;

namespace Sigma.FileSystem
{
    internal class FileManager
    {
        public static void ListContents(string dir)
        {
            string[] directories = Directory.GetDirectories(dir);
            string[] files = Directory.GetFiles(dir);
            int x = directories.Length + files.Length;
            for(int i = 0; i < x; i++)
            {
                if (i >= directories.Length)
                {
                    FileInfo fi = new FileInfo(dir + "\\" + files[i - directories.Length]);
                    System.Console.WriteLine("{0,30} {1}B",fi.Name,fi.Length);
                }
                else
                {
                    System.Console.WriteLine(directories[i]);
                }
            }
        }
    }
}
