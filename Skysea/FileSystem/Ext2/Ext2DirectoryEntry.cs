using Cosmos.System.FileSystem.Listing;
using System;
using System.IO;

namespace Skysea.FileSystem.Ext2
{
    internal class Ext2DirectoryEntry : DirectoryEntry
    {
        private Ext2FileSystem fileSystem;
        private Inode InodeOfEntry;

        public Ext2DirectoryEntry(Cosmos.System.FileSystem.FileSystem aFileSystem, DirectoryEntry aParent, uint InodeNo, string aFullPath, string aName, long aSize, DirectoryEntryTypeEnum aEntryType) : base(aFileSystem, aParent, aFullPath, aName, aSize, aEntryType)
        {
            fileSystem = (Ext2FileSystem)mFileSystem;
            InodeOfEntry = fileSystem.ReadInode(InodeNo);
        }

        public override Stream GetFileStream()
        {
            throw new NotImplementedException();
        }

        public Ext2FileSystem GetFileSystem()
        {
            return fileSystem;
        }

        public Inode GetInode()
        {
            return InodeOfEntry;
        }

        public override long GetUsedSpace()
        {
            throw new NotImplementedException();
        }

        public override void SetName(string aName)
        {
            throw new NotImplementedException();
        }

        public override void SetSize(long aSize)
        {
            throw new NotImplementedException();
        }
    }
}
