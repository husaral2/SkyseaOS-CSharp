using Cosmos.Core;
using Cosmos.HAL;
using Cosmos.HAL.BlockDevice;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.FAT;
using System;
using System.Collections.Generic;
using System.IO;

namespace Skysea.FileSystem
{
    internal class DiskManager
    {
        static ATA_PIO[] ataDevices = new ATA_PIO[4];
        public static BlockDevice[] devices = ATA_PIO.Devices.ToArray();

        public static bool SearchForDisks()
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

        public static void Format(Partition device)
        {
            MBR mbr = new MBR(device.Host);
            mbr.CreateMBR(device.Host);
            mbr.WritePartitionInformation(device, 0);
            FatFileSystemFactory fatFactory = new();
            Cosmos.System.FileSystem.FileSystem fatFs = fatFactory.Create(device, "0:\\", (long)(device.BlockCount * device.BlockSize / 1024));
            fatFs.Format("FAT32", true);
        }

    }
}
