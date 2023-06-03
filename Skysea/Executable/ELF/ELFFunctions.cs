using Skysea.Executable.ELF.Headers;

namespace Skysea.Executable.ELF
{
    internal unsafe class ELFFunctions
    {
        public static ELFSectionHeader* GetSectionHeader(ELFHeader* header)
        {
            return (ELFSectionHeader*)((int)header + header->SectionHeaderTableOffset);
        }

        public static ELFSectionHeader* GetSection(ELFHeader* header, int index)
        {
            return &GetSectionHeader(header)[index];
        }

        public static char* GetStringTable(ELFHeader* header)
        {
            if (header->SectionHeaderStringIndex == 0) 
                return null;
            return (char*)header + GetSection(header, header->SectionHeaderStringIndex)->SectionHeaderOffset;

        }

        public static char* GetStringFromTable(ELFHeader* header, int offset)
        {
            char* StringTable = GetStringTable(header);

            if (StringTable == null)
                return null;

            return StringTable + offset;
        }
    }
}
