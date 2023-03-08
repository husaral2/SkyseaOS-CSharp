using Cosmos.Core;

namespace Sigma.Hardware
{
    internal unsafe class MainFunctions
    {
        public static byte inb(IOPort io, ushort port)
        {
            if(io != new IOPort(port))
                io = new IOPort(port);
            return io.Byte;
        }

        public static void outb(IOPort io, ushort port, byte data)
        {
            if (io != new IOPort(port))
                io = new IOPort(port);
            io.Byte = data;
        }

    }
}
