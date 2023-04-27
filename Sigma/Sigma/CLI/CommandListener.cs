using Sigma.Usage;
using FS = Sigma.FileSystem;
using System;

namespace Sigma.CLI
{
    internal class CommandListener
    {    

        public static void Parse(string command)
        {
            string[] splitted = Helper.SpecialSplit(command);
            switch (command.Split(' ')[0])
            {
                case "echo":
                    if (command.Split(' ').Length > 1)
                        Console.WriteLine(command.Remove(0, 5));
                    break;
                case "gcd":
                    Console.WriteLine(FS.Controller.GetCurrentDirectory() + "\\");
                    break;
                case "format":
                    if (command.Split(' ').Length < 2)
                        break;
                    Console.WriteLine("Caution! All data in this drive will be gone forever! Do you want to continue? [(Y)es/(N)o]");
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        try
                        {
                            FS.DiskManager.Format(new Cosmos.HAL.BlockDevice.Partition(FS.DiskManager.devices[Convert.ToInt32(command.Split(' ')[1])], 511, FS.DiskManager.devices[Convert.ToInt32(command.Split(' ')[1])].BlockCount - 2048));
                        }
                        catch (Exception excp)
                        {
                            Console.WriteLine(excp.Message);
                        }
                    }
                    break;
                case "info":
                    Console.WriteLine("Codename Sigma 0.1.1 | 2023.4.19");
                    Console.WriteLine("Made by husaral, powered by COSMOS (www.gocosmos.org)");
                    break;
                case "shutdown":
                    splitted = command.Split(' ');
                    if (splitted.Length < 2)
                        break;
                    switch (splitted[1])
                    {
                        case "-s":
                            //UserManager.UserList[0].SavePreferences();
                            Cosmos.Core.ACPI.Shutdown();
                            break;
                        case "-r":
                            Cosmos.Core.CPU.Reboot();
                            break;
                        default:
                            Console.WriteLine("The action for this command isn't valid.\n (-s for a complete shutdown, -r for restart)");
                            break;
                    }
                    break;
                default:
                    CommandFunction.Search(command);
                    break;
            }
        }
    }
}
