using Cosmos.HAL.BlockDevice;
using Cosmos.System.FileSystem;
using System;
using System.Collections.Generic;

namespace Sigma.FileSystem
{
    internal class DiskManager
    {
        static ATA_PIO[] ataDevices = new ATA_PIO[4];

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
            for(int i = 0; i < ataDevices.Length; ++i)
            {
                if(GetInformation(i) > 0)
                    Console.WriteLine(i + ": " + (ataDevices[i].BlockCount + " " + ataDevices[i].BlockSize) + " LBA: "+ ataDevices[i].LBA48Bit);
            }
        }

        //Will be removed?
        public static bool InitalizeISOFS(BlockDevice device)
        {
            if (device.Type != BlockDeviceType.RemovableCD)
                return false;
            if (device.BlockCount == 0)
                return false;
            throw new NotImplementedException();
        }
    }
}
