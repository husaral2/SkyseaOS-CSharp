using System;
using System.Collections.Generic;
using System.IO;
using FS = Skysea.FileSystem;

namespace Skysea.CLI
{
    internal class CommandFunction
    {
        //All of the CLI functions listed in a dictionary - The word for invoking method | Invoked method
        static Dictionary<string, Func<string, bool>> functions = new Dictionary<string, Func<string, bool>>();
        static string[] splitted;

        public static void Initalize()
        {
            //Set the keyboard layout
            functions.Add("kbd", delegate (string input) { 
                return Usage.KeyboardSettings.ChangeKeyboard(input.Split(' ')[0]);
            });

            //Detailed disk information
            functions.Add("atainf", delegate (string input) {
                FS.DiskManager.GetDetailedInformation();
                return true;
            });

            //Read a file's content
            functions.Add("read", delegate (string input) {
                splitted = Helper.SpecialSplit(input);
                string file = Helper.FindPath(splitted[1], FS.Controller.GetCurrentDirectory());
                if (File.Exists(file))
                {
                    Console.WriteLine(File.ReadAllText(file));
                    return true;
                }
                return false;
            });

            functions.Add("cat", delegate (string input)
            {
                splitted = Helper.SpecialSplit(input);
                string file = Helper.FindPath(splitted[1], FS.Controller.GetCurrentDirectory());
                if (File.Exists(file))
                {
                    StreamWriter stream = new StreamWriter(new FileStream(file, FileMode.Append, FileAccess.Write));

                    //Nobody would want the path of the file at the file itself (and the command they used)
                    stream.WriteLine(input.Remove(0, splitted[1].Length + 5));
                    stream.Close();

                    return true;
                }
                return false;
            });

            //Create a new file
            functions.Add("new", delegate (string input) {
                string[] splitted = Helper.SpecialSplit(input);
                string file = Helper.FindPath(splitted[1], FS.Controller.GetCurrentDirectory());
                if (file.IndexOfAny(FS.Controller.invalidCharacters) >= 0)
                {
                    Console.WriteLine("File cannot contain invalid characters (* \\ / \" < > : | ?)");
                    return false;
                }
                FS.Controller.fs.CreateFile(file);
                return true;
            });

            //Create a new directory
            functions.Add("nwdir", delegate(string input)
            {
                splitted = Helper.SpecialSplit(input);
                string file = Helper.FindPath(splitted[1], FS.Controller.GetCurrentDirectory());
                if (file.IndexOfAny(FS.Controller.invalidCharacters) >= 0)
                {
                    Console.WriteLine("Directory cannot contain invalid characters (* \\ / \" < > : | ?)");
                    return false;
                }
                FS.Controller.fs.CreateDirectory(file);
                return true;
            });

            //Remove a file
            functions.Add("remove", delegate(string input) {
                splitted = Helper.SpecialSplit(input);
                string file = Helper.FindPath(splitted[1], FS.Controller.GetCurrentDirectory());
                if (File.Exists(file))
                {
                    FS.Controller.fs.DeleteFile(FS.Controller.fs.GetFile(file));
                    return true;
                }
                Console.WriteLine("The file you are trying to remove doesn't exist.");
                return false;
            });

            //Remove a directory
            functions.Add("rmdir", delegate (string input) {
                splitted = Helper.SpecialSplit(input);
                string file = Helper.FindPath(splitted[1], FS.Controller.GetCurrentDirectory());
                if (Directory.Exists(file))
                {
                    FS.Controller.fs.DeleteDirectory(FS.Controller.fs.GetDirectory(file));
                    return true;
                }
                Console.WriteLine("The directory you try to remove doesn't exist.");
                return false;
            });

            //Copy a file
            functions.Add("copy", delegate(string input) {
                string[] splitted = Helper.SpecialSplit(input);
                string src = Helper.FindPath(splitted[1], FS.Controller.GetCurrentDirectory());
                string dst = Helper.FindPath(splitted[2], FS.Controller.GetCurrentDirectory());
                if (File.Exists(src))
                {
                    File.Copy(src, dst);
                    return true;
                }
                Console.WriteLine("The file you are trying to copy doesn't exist.");
                return false;
            });

            //Move a file
            functions.Add("move", delegate(string input)
            {
                splitted = Helper.SpecialSplit(input);
                string src = Helper.FindPath(splitted[1], FS.Controller.GetCurrentDirectory());
                string dst = Helper.FindPath(splitted[2], FS.Controller.GetCurrentDirectory());
                if (File.Exists(src))
                {
                    File.Copy(src, dst);
                    FS.Controller.fs.DeleteFile(FS.Controller.fs.GetFile(src));
                    return true;
                }
                Console.WriteLine("The file you are trying to move doesn't exist.");
                return false;
            });

            //Formatting the drive in FAT32
            functions.Add("format", delegate (string input)
            {
                splitted = input.Split(' ');
                if (splitted.Length < 3)
                    return false;

                uint startsector = Convert.ToUInt32(splitted[2]);

                //If there is a 3rd argument, it will set the size of the of the partition to that argument, else the partition size will be block count minus 2048 by default
                uint size = (uint)(splitted.Length < 4 ? FS.DiskManager.devices[Convert.ToInt32(splitted[1])].BlockCount - 2048 : Convert.ToUInt32(splitted[3]));

                Console.WriteLine("Warning! All data in this drive will be gone forever! Do you want to continue? [(Y)es/(N)o]");
                if (Console.ReadLine().ToLower() == "y" && startsector > 511)
                {
                    try
                    {
                        FS.DiskManager.Format(new Cosmos.HAL.BlockDevice.Partition(FS.DiskManager.devices[Convert.ToInt32(splitted[1])], startsector - 1, size));
                        return true;
                    }
                    catch (Exception excp)
                    {
                        Console.WriteLine(excp.Message);
                        return false;
                    }
                }
                else if (startsector < 511)
                    Console.WriteLine("The partition can't start before the 512. sector");
                    return false;
            });

            functions.Add("cd", new Func<string, bool>(FS.FileManager.Navigate));
            functions.Add("dir", new Func<string, bool>(FS.FileManager.ListContents));
        }

        public static void Search(string input)
        {
            foreach (string key in functions.Keys)
            {
                if (input.Split(' ')[0] == key)
                {
                    functions[key](input);
                    return;
                }
            }
            Console.WriteLine("The command doesn't exist.");
        }
    }
}
