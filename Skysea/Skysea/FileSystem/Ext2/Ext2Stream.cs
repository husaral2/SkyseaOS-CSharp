using System;
using System.Collections.Generic;
using System.IO;

namespace Skysea.FileSystem.Ext2
{
    internal class Ext2Stream : System.IO.Stream
    {
        protected byte[] mReadBuffer;

        protected long mPosition;

        protected long? mReadBufferPosition;

        protected long mSize;

        private readonly Ext2DirectoryEntry mDirectoryEntry;

        private readonly Ext2FileSystem mFileSystem;

        private Inode mInode;
        
        public Ext2Stream(Ext2DirectoryEntry xDirectoryEntry)
        {
            mDirectoryEntry = xDirectoryEntry ?? throw new ArgumentNullException();
            mFileSystem = xDirectoryEntry.GetFileSystem();
            mInode = xDirectoryEntry.GetInode();
            mSize = xDirectoryEntry.mSize;
        }

        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => false;

        public override long Length => throw new NotImplementedException();

        public override long Position { get => mSize; set => mPosition = value; }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if(offset < 0 || count < 0)
            {
                throw new ArgumentException();
            }

            long MaxReadableBytes = mDirectoryEntry.mSize - 1;
            long xCount = count;
            long xOffset = offset;

            int BlockPointersWillBeRead = count / (1024 << (int) mFileSystem.GetSuperBlock().BlockSize);
            
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
