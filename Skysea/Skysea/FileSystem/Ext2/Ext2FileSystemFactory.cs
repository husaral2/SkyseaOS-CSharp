using Cosmos.HAL.BlockDevice;
using Cosmos.System.FileSystem;
using System;

namespace Skysea.FileSystem.Ext2
{
    internal class Ext2FileSystemFactory : FileSystemFactory
    {
        public override string Name => "Ext2";

        public override Cosmos.System.FileSystem.FileSystem Create(Partition aDevice, string aRootPath, long aSize)
        {
            Ext2FileSystem fs = new Ext2FileSystem(aDevice, aRootPath, aSize);
            return fs;
        }

        public override bool IsType(Partition aDevice)
        {
            byte[] xSB = aDevice.NewBlockArray(2);
            aDevice.ReadBlock(2, 2, ref xSB);
            return BitConverter.ToUInt16(xSB, 56) == 0xEF53;
        }
    }
}
