using Cosmos.Core;
using Cosmos.HAL.BlockDevice;
using System;

namespace Skysea.Drivers
{
    internal unsafe class Ramdisk : BlockDevice
    {
        ManagedMemoryBlock Disk;
        uint DiskSize;
        new ulong BlockSize = 256;

        public override BlockDeviceType Type => BlockDeviceType.HardDrive;

        public Ramdisk(uint size) {
            DiskSize = size;
            Disk = new(size);
        }

        public void Dispose()
        {
            GCImplementation.Free(Disk);
        }

        public void WriteByte(byte data, uint offset)
        {
            Disk.Write8(offset, data);
        }

        public void WriteInt16(ushort data, uint offset)
        {
            Disk.Write16(offset, data);
        }

        public void WriteInt32(uint data, uint offset)
        {
            Disk.Write32(offset, data);
        }
        
        public void WriteZone(byte[] data, int offset)
        {
            Buffer.BlockCopy(data, 0, Disk.memory, offset, data.Length);
        }

        public void WriteString(string data, int offset)
        {
            for(int i = 0; i < data.Length; ++i)
            {
                Disk.memory[offset + i] = (byte) data[i];
            }

            Disk.memory[offset + data.Length] = 0;
        }

        public byte ReadByte(int offset)
        {
            return Disk.memory[offset];
        }

        public byte[] ReadZone(int offset, int count)
        {
            byte[] output = new byte[count];
            Buffer.BlockCopy(Disk.memory, offset, output, 0, count);
            return output;
        }
    
        public uint ReadInt32(uint offset)
        {
            return Disk.Read32(offset);
        }

        public string ReadString(int offset, int length)
        {
            string value = "";
            for (int i = 0; i <= length; ++i)
                value += (char) Disk.memory[offset + i];
            return value;
        }

        public uint GetSize()
        {
            return DiskSize;
        }

        public override void ReadBlock(ulong aBlockNo, ulong aBlockCount, ref byte[] aData)
        {
            byte[] output = new byte[aBlockCount * BlockSize];
            Buffer.BlockCopy(Disk.memory, (int)(aBlockNo * BlockSize) , output, 0, (int)(aBlockCount * BlockSize));
            aData = output;
        }

        public override void WriteBlock(ulong aBlockNo, ulong aBlockCount, ref byte[] aData)
        {
            Buffer.BlockCopy(aData, 0, Disk.memory, (int) (aBlockNo * BlockSize), (int)aBlockCount);
        }
    }
}
