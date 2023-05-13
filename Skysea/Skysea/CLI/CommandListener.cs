using Skysea.Usage;
using FS = Skysea.FileSystem;
using System;
using Cosmos.HAL.BlockDevice;
using Skysea.FileSystem.Ext2;

namespace Skysea.CLI
{
    internal class CommandListener
    {
        public static void Parse(string command) {
            
            string[] splitted = command.Split(' ');
            switch (command.Split(' ')[0])
            {
                case "echo":
                    if (splitted.Length > 1)
                        Console.WriteLine(command.Remove(0, 5));
                    break;
                case "clc":
                    Console.Clear();
                    break;
                case "gcd":
                    Console.WriteLine(FS.Controller.GetCurrentDirectory() + "\\");
                    break;
                case "sysinf":
                    Console.WriteLine("CPU: " + Cosmos.Core.CPU.GetCPUVendorName() + " " + Cosmos.Core.CPU.GetCPUBrandString());
                    Console.WriteLine("Uptime: " + Cosmos.Core.CPU.GetCPUUptime() / (ulong)Cosmos.Core.CPU.GetCPUCycleSpeed() + "s");
                    Console.WriteLine("RAM: " + Cosmos.Core.CPU.GetAmountOfRAM() + " Free: " + Cosmos.Core.GCImplementation.GetAvailableRAM());
                    break;
                /*case "isext2":
                    try
                    {
                        MBR mbr = new MBR(FS.DiskManager.devices[0]);
                        Ext2FileSystemFactory fs = new();
                        Console.WriteLine(fs.IsType(new Partition(FS.DiskManager.devices[0], mbr.Partitions[0].StartSector, mbr.Partitions[0].SectorCount)));
                        Ext2FileSystem ext2 = (Ext2FileSystem) fs.Create(new Partition(FS.DiskManager.devices[0], mbr.Partitions[0].StartSector, mbr.Partitions[0].SectorCount), "1:\\", 434);
                        ext2.DisplayFileSystemInfo();

                        for(uint i = 1; i < 256; ++i)
                        {
                            Console.WriteLine(i);
                            Inode inode = ext2.ReadInode(i);
                            for(int j = 0; j < 12; ++j)
                            {
                                Console.Write(inode.DirectBlockPointers[j] + " ");
                                if (j % 3 == 0)
                                    Console.WriteLine();
                            }
                            Console.WriteLine(inode.SingleIndirectBlockPointer + " " + inode.DoubleIndirectBlockPointer + " " + inode.TripleIndirectBlockPointer);
                            Console.ReadLine();
                        }

                    } catch(Exception excp)
                    {
                        Console.WriteLine(excp.Message);
                    }
                    break;
                */
                case "info":
                    Console.WriteLine("SkyseaOS 0.1.2 [Milestone 0] | 2023.5.13");
                    Console.WriteLine("Made by husaral, powered by COSMOS (www.gocosmos.org)");
                    break;
                case "shutdown":
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
