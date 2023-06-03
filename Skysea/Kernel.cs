using System;
using FS = Skysea.FileSystem;
using Sys = Cosmos.System;

namespace Skysea
{
    public class Kernel : Sys.Kernel
    {
        //SkyseaOS 0.1.2 [Milestone 0]

        protected override void BeforeRun()
        {
            try
            {
                FS.DiskManager.SearchForDisks();
                Console.Write("Initializing the filesystem: ");
                FS.Controller.InitializeFilesystem();
                Console.WriteLine("done!");
                Console.Beep(587, 500);
            }
            catch(Exception excp)
            {
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.Clear();
                Console.WriteLine("An exception has ocurred!\n");
                Console.WriteLine(excp.Message);
                Console.WriteLine("\nIf you are seeing this first time, you might try restarting.\n" +
                    "If that did not work, please check for any hardware problems.\n" +
                    "If you are sure you do not have any problems, please leave a bug report!");
                Console.ReadKey();
                Cosmos.Core.CPU.Reboot();
            }


            CLI.CommandFunction.Initalize();
            Console.ReadLine();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to SkyseaOS!");
            Console.ResetColor();
        }

        protected override void Run()
        {
            Console.Write("# ");
            string command = Console.ReadLine();
            CLI.CommandListener.Parse(command);
        }
    }
}
