using System.Runtime.InteropServices;

namespace Skysea.Executable.ELF.Headers
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct ELFHeader
    {
        public ELFHeader(ELFHeader* Header)
        {
            MagicNumber = Header->MagicNumber;
            Class = Header->Class;
            EndianType = Header->EndianType;
            HeaderVersion = Header->HeaderVersion;
            OSABI = Header->OSABI;
            ABIVersion = Header->ABIVersion;
            Type = Header->Type;

            MachineType = Header->MachineType;
            ELFVersion = Header->ELFVersion;
            EntryPoint = Header->EntryPoint;
            ProgramHeaderTableOffset = Header->ProgramHeaderTableOffset;
            SectionHeaderTableOffset = Header->SectionHeaderTableOffset;
            Flags = Header->Flags;
            HeaderSize = Header->HeaderSize;
            ProgramHeaderEntrySize = Header->ProgramHeaderEntrySize;
            ProgramHeaderEntryCount = Header->ProgramHeaderEntryCount;
            SectionHeaderEntrySize = Header->SectionHeaderEntrySize;
            SectionHeaderEntryCount = Header->SectionHeaderEntryCount;
            SectionHeaderStringIndex = Header->SectionHeaderStringIndex;
        }

        public bool IsType()
        {
            return MagicNumber == System.BitConverter.ToUInt32(new byte[] {0x7F, 0x45, 0x4C, 0x46});
        }

        #region Identitation
        public uint MagicNumber;
        public ELFClassType Class;
        public ELFEndianType EndianType;
        readonly byte HeaderVersion;
        public ELFOSType OSABI;
        public byte ABIVersion;
        private fixed char Padding[7];
        #endregion

        public ELFExecutableType Type;
        public ushort MachineType;
        public uint ELFVersion;
        public uint EntryPoint;
        public uint ProgramHeaderTableOffset;
        public uint SectionHeaderTableOffset;
        public uint Flags;
        public ushort HeaderSize;
        public ushort ProgramHeaderEntrySize;
        public ushort ProgramHeaderEntryCount;
        public ushort SectionHeaderEntrySize;
        public ushort SectionHeaderEntryCount;
        public ushort SectionHeaderStringIndex;
    }
}
