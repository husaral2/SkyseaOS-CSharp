using System;

namespace Skysea.FileSystem.RAMFS
{
    internal class RAMFSFileSystem
    {
        public Drivers.Ramdisk Disk;
        uint FileCount;
        ushort BlockSize;
        uint UsedBlocks;

        public RAMFSFileSystem(Drivers.Ramdisk aDisk, ushort blockSize = 256)
        {
            Disk = aDisk;
            BlockSize = blockSize;
        }

        public void CreateNextFile(uint numberofblocks) {
            if (numberofblocks < 1)
                throw new ArgumentOutOfRangeException("The number of blocks a file contain should be bigger than zero.");
            if (FileCount * 8 >= 512)
                return ;
            if ((UsedBlocks + numberofblocks) * BlockSize + 512 > Disk.GetSize())
                return ;

            //Offset of the file
            Disk.WriteInt32(511 + UsedBlocks * BlockSize, FileCount * 8);
            //Number of blocks a file spans
            Disk.WriteInt32(numberofblocks, FileCount * 8 + 4);
            UsedBlocks += numberofblocks;
            ++FileCount;
        }
        
        public byte[] ReadFile(uint fileIndex)
        {
            return Disk.ReadZone((int) Disk.ReadInt32(fileIndex * 8), (int) Disk.ReadInt32(fileIndex * 8 + 4) * BlockSize);
        }

        //<summary>
        //Writes data to a file
        //</summary>
        //<param name="data">The data to be written</param>
        //<param name="fileIndex">The index of file to be written on</param>
        public void WriteFile(byte[] data, uint fileIndex)
        {
            Disk.WriteZone(data, (int) Disk.ReadInt32(fileIndex * 8));
        }

        public void SaveFile(uint fileIndex)
        {
            System.IO.File.Create("0:\\" + fileIndex + ".ram");
            System.IO.File.WriteAllBytes("0:\\" + fileIndex + ".ram", ReadFile(fileIndex));
        }
    }
}
