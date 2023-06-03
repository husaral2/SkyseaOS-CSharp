using System.Runtime.InteropServices;

namespace Skysea.Executable.ELF.Headers
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct ELFProgramHeader
    {
        public ELFProgramHeader(ELFProgramHeader* Header)
        {
            ProgramType = Header->ProgramType;
            ProgramOffset = Header->ProgramOffset;
            ProgramVAdress = Header->ProgramVAdress;
            ProgramPAdress = Header->ProgramPAdress;
            FileSize = Header->FileSize;
            MemorySize = Header->MemorySize;
            Flags = Header->Flags;
            Align = Header->Align;
        }

        public ELFProgramType ProgramType;
        public uint ProgramOffset;
        public uint ProgramVAdress; //Where the program should be located on the virtual memory
        public uint ProgramPAdress;
        public uint FileSize;
        public uint MemorySize;
        public uint Flags;
        public uint Align;
    }
}
