//Enums that's used for identifying values in ELF executables
namespace Skysea.Executable.ELF
{

    #region Executable Header Enums
    internal enum ELFExecutableType : ushort                      
    {
        None,
        Relocatable,
        Executable
    }

    internal enum ELFClassType
    {
        Class32 = 1,
        Class64 = 2
    }

    internal enum ELFEndianType
    {
        Little = 1,
        Big = 2
    }

    internal enum ELFOSType : ushort
    {
        None,
        HPUX,
        NetBSD,
        Linux
    }

    internal enum ELFProgramType : uint
    {
        Null,
        Load,
        Dynamic
    }
    #endregion

    #region Section Header Enums
    enum SectionTableTypes : uint
    {
        Null = 0,
        Info = 1,
        SymbolTable = 2,
        StringTable= 3 ,
        RelocationWithAddend = 4,
        NotPresent = 8,
        Relocation = 9
    }
    #endregion
}
