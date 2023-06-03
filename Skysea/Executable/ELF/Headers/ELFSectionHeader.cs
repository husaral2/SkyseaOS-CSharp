using System.Runtime.InteropServices;

namespace Skysea.Executable.ELF.Headers
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct ELFSectionHeader
    {
        public ELFSectionHeader(ELFSectionHeader* Header)
        {
            SectionHeaderName = Header->SectionHeaderName;
            SectionHeaderType = Header->SectionHeaderType;
            Flags = Header->Flags;
            SectionHeaderOffset = Header->SectionHeaderOffset;
            SectionHeaderSize = Header->SectionHeaderSize;
            SectionHeaderLink = Header->SectionHeaderLink;
            SectionHeaderInfo = Header->SectionHeaderInfo;
            SectionHeaderAlign = Header->SectionHeaderAlign;
            SectionHeaderEntrySize = Header->SectionHeaderEntrySize;

        }

        public uint SectionHeaderName;
        public SectionTableTypes SectionHeaderType;
        public uint Flags;
        public uint SectionHeaderOffset;
        public uint SectionHeaderSize;
        public uint SectionHeaderLink;
        public uint SectionHeaderInfo;
        public uint SectionHeaderAlign;
        public uint SectionHeaderEntrySize;
    }
}
