using Cosmos.HAL.BlockDevice;
using Cosmos.System.FileSystem.Listing;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Skysea.FileSystem.Ext2
{
    //The superblock struct, the superblock in Ext2 contains information about the filesystem.
    internal struct SuperBlock
    {
        public uint TotalNumberOfInodes;
        public uint TotalNumberOfBlocks;
        public uint SuperuserReservedBlocks;
        public uint UnallocatedBlocks;
        public uint UnallocatedInodes;
        public uint BlockNumber; //block number of the block containing the superblock
        public uint BlockSize;
        public uint FragmentSize;
        public uint BlocksInAGroup;
        public uint FragmentsInAGroup;
        public uint InodesInAGroup;
        public uint LastMountTime; //in POSIX
        public uint LastWrittenTime; //in POSIX
        public ushort MountCountSinceCheck;
        public ushort MountsAllowedBeforeCheck;
        public ushort Signature;
        public ushort State;
        public ushort BehaviorWhenError;
        public ushort MinorVersion;
        public uint LastConsistencyCheck; //in POSIX
        public uint IntervalBetweenChecks;
        public uint OSID; //OS ID from which the filesystem on a volume was created
        public uint MajorVersion;
        public ushort UserID; //that can use reserved blocks
        public ushort GroupID; //that can use reserved blocks
        public uint FirstNonReservedInode;
        public uint InodeSize;
    }

    //Contains information regarding where important data structures are located
    internal struct BlockGroupDescriptor
    {
        public uint BlockUsageAdress;
        public uint InodeUsageAdress;
        public uint InodeTableAdress;
        public uint UnallocatedBlocksInAGroup;
        public uint UnallocatedInodesInAGroup;
        public uint DirectoriesInAGroup;
    }

    internal struct Inode
    {
        public uint SizeLow32;
        public uint LastAccessTime;
        public uint CreationTime;
        public uint LastModificationTime;
        public uint DeletionTime;
        public ushort GroupID;
        public ushort CountOfHardLinks;
        public uint CountOfDiskSectors;
        public uint[] DirectBlockPointers;
    }

    internal class Ext2FileSystem : Cosmos.System.FileSystem.FileSystem
    {
        private Partition xDevice;
        private SuperBlock xSB = new SuperBlock();
        private uint SectorsPerBlock;
        List<BlockGroupDescriptor> xBlockGroups = new();

        public Ext2FileSystem(Partition aDevice, string aRootPath, long aSize) : base(aDevice, aRootPath, aSize)
        {
            xDevice = aDevice ?? throw new ArgumentNullException("Partition is null");

            ReadTheSuperblock();
            ReadBlockGroupDescriptorTable();
        }

        public override long AvailableFreeSpace => xSB.UnallocatedBlocks * (1024 << (int) xSB.BlockSize);

        public override long TotalFreeSpace => throw new NotImplementedException();

        public override string Type => "Ext2";

        public override string Label { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override DirectoryEntry CreateDirectory(DirectoryEntry aParentDirectory, string aNewDirectory)
        {
            throw new NotImplementedException();
        }

        public override DirectoryEntry CreateFile(DirectoryEntry aParentDirectory, string aNewFile)
        {
            throw new NotImplementedException();
        }

        public override void DeleteDirectory(DirectoryEntry aPath)
        {
            throw new NotImplementedException();
        }

        public override void DeleteFile(DirectoryEntry aPath)
        {
            throw new NotImplementedException();
        }

        public override void DisplayFileSystemInfo()
        {
            Console.WriteLine("Filesystem: ");
            Console.WriteLine("Total number of inodes: " + xSB.TotalNumberOfInodes);
            Console.WriteLine("Total number of blocks: " + xSB.TotalNumberOfBlocks);
            Console.WriteLine("Total number of unallocated inodes: " + xSB.UnallocatedInodes);
            Console.WriteLine("Total number of unallocated inodes: " + xSB.UnallocatedBlocks);
            Console.WriteLine("Block size: " + xSB.BlockSize);
            Console.WriteLine("Fragment size: " + xSB.FragmentSize);
            Console.WriteLine("Indoes per group: " + xSB.InodesInAGroup);
            Console.WriteLine("The number of first non-reserved inode: " + xSB.FirstNonReservedInode);
            Console.WriteLine("The block usage adress of the 1st block group: " + xBlockGroups[0].BlockUsageAdress);
            Console.WriteLine("The inode usage adress of the 1st block group: " + xBlockGroups[0].InodeUsageAdress);
            Console.WriteLine("The block table adress of the 1st block group: " + xBlockGroups[0].InodeTableAdress);
        }

        public override void Format(string aDriveFormat, bool aQuick)
        {
            throw new NotImplementedException();
        }

        public override List<DirectoryEntry> GetDirectoryListing(DirectoryEntry baseDirectory)
        {
            throw new NotImplementedException();
        }

        public override DirectoryEntry GetRootDirectory()
        {
            Ext2DirectoryEntry xRootDirectory = new Ext2DirectoryEntry(this, null, 2, RootPath, RootPath, Size, DirectoryEntryTypeEnum.Directory);
            return xRootDirectory;
        }

        private void ReadTheSuperblock()
        {
            byte[] superBlock = xDevice.NewBlockArray(2);
            xDevice.ReadBlock(2, 2, ref superBlock);
            xSB.TotalNumberOfInodes = BitConverter.ToUInt32(superBlock, 0);
            xSB.TotalNumberOfBlocks = BitConverter.ToUInt32(superBlock, 4);
            xSB.SuperuserReservedBlocks = BitConverter.ToUInt32(superBlock, 5);
            xSB.UnallocatedBlocks = BitConverter.ToUInt32(superBlock, 12);
            xSB.UnallocatedInodes = BitConverter.ToUInt32(superBlock, 16);
            xSB.BlockNumber = BitConverter.ToUInt32(superBlock, 20);
            xSB.BlockSize = (uint)1024 << BitConverter.ToInt32(superBlock, 24);
            xSB.FragmentSize = (uint)1024 << BitConverter.ToInt32(superBlock, 28); //we need to shift 1024 to left the number in this fields times
            xSB.BlocksInAGroup = BitConverter.ToUInt32(superBlock, 32);
            xSB.FragmentsInAGroup = BitConverter.ToUInt32(superBlock, 36);
            xSB.InodesInAGroup = BitConverter.ToUInt32(superBlock, 40);
            xSB.LastMountTime = BitConverter.ToUInt32(superBlock, 44);
            xSB.LastWrittenTime = BitConverter.ToUInt32(superBlock, 48);
            xSB.MountCountSinceCheck = BitConverter.ToUInt16(superBlock, 52);
            xSB.MountsAllowedBeforeCheck = BitConverter.ToUInt16(superBlock, 54);
            xSB.Signature = BitConverter.ToUInt16(superBlock, 56);
            xSB.State = BitConverter.ToUInt16(superBlock, 58);
            xSB.BehaviorWhenError = BitConverter.ToUInt16(superBlock, 60);
            xSB.MinorVersion = BitConverter.ToUInt16(superBlock, 62);
            xSB.LastConsistencyCheck = BitConverter.ToUInt32(superBlock, 64);
            xSB.IntervalBetweenChecks = BitConverter.ToUInt32(superBlock, 68);
            xSB.OSID = BitConverter.ToUInt32(superBlock, 72);
            xSB.MajorVersion = BitConverter.ToUInt32(superBlock, 76);
            xSB.UserID = BitConverter.ToUInt16(superBlock, 80);
            xSB.GroupID = BitConverter.ToUInt16(superBlock, 82);
            xSB.FirstNonReservedInode = BitConverter.ToUInt32(superBlock, 84);
            xSB.InodeSize = BitConverter.ToUInt16(superBlock, 88);
            SectorsPerBlock = (uint)(xSB.BlockSize / xDevice.BlockSize);
        }

        //The block group descriptor table because is cached because we will only read the FS
        public void ReadBlockGroupDescriptorTable()
        {
            byte[] Table = xDevice.NewBlockArray(SectorsPerBlock);
            xDevice.ReadBlock(SectorsPerBlock, SectorsPerBlock, ref Table);
            BlockGroupDescriptor bgd;

            for(int i = 0; i < 64; ++i)
            {
                bgd = new();
                bgd.BlockUsageAdress = BitConverter.ToUInt32(Table, i * 32 + 0); //just for clarity
                bgd.InodeUsageAdress = BitConverter.ToUInt32(Table, i * 32 + 4);
                bgd.InodeTableAdress = BitConverter.ToUInt32(Table, i * 32 + 8);
                bgd.UnallocatedBlocksInAGroup = BitConverter.ToUInt16(Table, i * 32 + 12);
                bgd.UnallocatedInodesInAGroup = BitConverter.ToUInt16(Table, i * 32 + 14);
                bgd.DirectoriesInAGroup = BitConverter.ToUInt16(Table, i * 32 + 16);
                xBlockGroups.Add(bgd);
            }
        }

        public Inode ReadInode(uint number)
        {
            byte[] Table = xDevice.NewBlockArray(SectorsPerBlock);

            int ContainingBGDsIndex = (int)((number - 1) / xSB.InodesInAGroup);
            BlockGroupDescriptor ContainingBGD = xBlockGroups[ContainingBGDsIndex];
            Console.WriteLine(ContainingBGD.InodeTableAdress);

            long index = (number - 1) % xSB.InodesInAGroup;
            int containingBlocksIndex = (int) (index * xSB.InodeSize / xSB.BlockSize);
            Console.WriteLine(xSB.InodeSize);

            xDevice.ReadBlock(ContainingBGD.InodeTableAdress * SectorsPerBlock, SectorsPerBlock, ref Table);

            Inode inode = new();
            inode.SizeLow32 = BitConverter.ToUInt32(Table, (int)(index * xSB.InodeSize + 4));
            inode.LastAccessTime = BitConverter.ToUInt32(Table, (int)(index * xSB.InodeSize + 8));
            inode.CreationTime = BitConverter.ToUInt32(Table, (int)(index * xSB.InodeSize + 12));
            inode.LastModificationTime = BitConverter.ToUInt32(Table, (int)(index * xSB.InodeSize + 16));
            inode.DeletionTime = BitConverter.ToUInt32(Table, (int)(index * xSB.InodeSize + 20));
            inode.GroupID = BitConverter.ToUInt16(Table, (int)(index * xSB.InodeSize + 24));
            inode.CountOfHardLinks = BitConverter.ToUInt16(Table, (int)(index * xSB.InodeSize + 26));
            inode.CountOfDiskSectors = BitConverter.ToUInt32(Table, (int)(index * xSB.InodeSize + 28));

            inode.DirectBlockPointers = new uint[12];

            for (int i = 0; i < 15; ++i)
            {
                inode.DirectBlockPointers[i] = BitConverter.ToUInt32(Table, (int)(index * xSB.InodeSize + 40) + i * 4);
            }

            return inode;
        }

        public void GetDirectoryEntrysLocation(int index, Inode parent)
        {
            uint[] BlockPointers = parent.DirectBlockPointers;
            byte[] TempBlock;
            uint DirectoryBlock = index / (xSB.BlockSize / dir);
        }

        public SuperBlock GetSuperBlock()
        {
            return xSB;
        }

    }
}
