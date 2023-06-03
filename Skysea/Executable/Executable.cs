using Skysea.Executable.ELF.Headers;
using System;

namespace Skysea.Executable
{
    internal unsafe class Executable
    {
        public delegate* unmanaged<void> Main;

        public Executable()
        {
            Main = (delegate* unmanaged<void>)0;
        }

        //Function for loading 32-bit ELF binaries
        public static Executable FromELF32(byte[] Binary)
        {
            Executable ELFExc = new Executable();
            fixed(byte* ptr = Binary)
            {

                ELFHeader Header = new((ELFHeader*) ptr);

                //Check if the file is a valid ELF file
                if (!Header.IsType())
                    return null;

                ELFExc.Main = (delegate* unmanaged<void>)Header.EntryPoint;

                ELFProgramHeader* ProgramHeader = (ELFProgramHeader*)(ptr + Header.ProgramHeaderTableOffset);

                for (int i = 0; i < Header.ProgramHeaderEntryCount; i++, ProgramHeader++) {
                    switch (ProgramHeader->ProgramType)
                    {

                        case ELF.ELFProgramType.Null:
                            return null;
                        case ELF.ELFProgramType.Load:
                            Buffer.MemoryCopy(ProgramHeader + ProgramHeader->ProgramOffset, (byte*)ProgramHeader->ProgramVAdress, ProgramHeader->FileSize, ProgramHeader->FileSize);
                            break;
                        default:
                            break;

                    }
                }

            }

            return ELFExc;
        }

    }
}
