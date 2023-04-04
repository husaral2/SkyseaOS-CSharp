using Cosmos.HAL.BlockDevice;
using Cosmos.System.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sigma.FileSystem
{
    internal class DiskManager
    {
        static ATA_PIO[] ataDevices = new ATA_PIO[4];
        public static BlockDevice[] devices = ATA_PIO.Devices.ToArray();

        public static bool InitalizeDisks()
        {
            bool isFound = false;
            for(int i = 0; i < 4; ++i)
            {
                if(i > 1)
                {
                    ataDevices[i] = new ATA_PIO(new Cosmos.Core.IOGroup.ATA(true),Ata.ControllerIdEnum.Secondary, (Ata.BusPositionEnum) i - 2);
                    isFound = !(GetInformation(i) < 1) || isFound;
                    continue;
                }
                ataDevices[i] = new ATA_PIO(new Cosmos.Core.IOGroup.ATA(false), Ata.ControllerIdEnum.Primary, (Ata.BusPositionEnum) i);
                isFound = !(GetInformation(i) < 1) || isFound;
            }
            return isFound;
        }

        //Checks if the device exists or not, returns false if the device type is null
        public static sbyte GetInformation(int DeviceNo)
        {
            switch (ataDevices[DeviceNo].DiscoverDrive())
            {
                case ATA_PIO.SpecLevel.Null:
                    Console.WriteLine("This drive does not exist.");
                    return 0;
                case ATA_PIO.SpecLevel.ATA:
                    Console.WriteLine("Drive type: ATA");
                    return 1;
                case ATA_PIO.SpecLevel.ATAPI:
                    Console.WriteLine("Drive type: ATAPI");
                    return 2;
                default:
                    return -1;
            }
        }

        //Gets device type, block size, block type, LBA availablity
        public static void GetDetailedInformation()
        {
            List<Disk> a = Controller.fs.GetDisks();
            for(int i = 0; i < a.Count; ++i)
            {
                a[i].DisplayInformation();
                foreach (ManagedPartition mp in a[i].Partitions)
                    Console.WriteLine(mp.HasFileSystem);
                
            }
        }

        public static void FormatFAT(Partition device)
        {
            Cosmos.System.FileSystem.FAT.FatFileSystemFactory factory = new Cosmos.System.FileSystem.FAT.FatFileSystemFactory();
            Cosmos.System.FileSystem.VFS.FileSystemManager.Register(factory);
            Cosmos.System.FileSystem.FileSystem fs = factory.Create(device, "0:", (long)device.BlockSize);
            fs.Format("FAT32", false);
        }

        public static void CreateNewBootSector(BlockDevice device)
        {
            byte[] xBPB = device.NewBlockArray(1);
            
            device.WriteBlock(0, 1, ref xBPB);
        }
    }
}
